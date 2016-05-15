using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class Hero : MonoBehaviour
{
	private static int _FACING_LEFT = -1;
	private static int _FACING_RIGHT = 1;

	[SerializeField]
	private float maxWalkingSpeed = 5f;
	[SerializeField]
	private float walkMotorTorque = 40f;
	[SerializeField]
	private float maxClimbingSpeed = 5f;
	[SerializeField]
	private float horizontalFlyingForce = 0.5f;
	[SerializeField]
	private float jumpHeight = 1f;	
	[SerializeField]
	private float offsetCarryObjHero = 0.7f;
	
	[SerializeField]
	private LayerMask whatIsGround;
	[SerializeField]
	private LayerMask heroPlatformMask;
	[SerializeField]
	private LayerMask mapInteractiveObjectsMask;
	[SerializeField]
	private Collider2D headCollider = null;
	[SerializeField]
	private Collider2D heroPlatform = null;
	[SerializeField]
	private Collider2D bodyCollider = null;
	[SerializeField]
	private HingeJoint2D walkMotor = null;
	[SerializeField]
	private Collider2D footCollider = null;

	private HeroInteractor heroInterac;
	private float motorMaxAngularSpeed = 0f;
	private int facingDirection=_FACING_RIGHT;
	private Rigidbody2D rigidBody2D;
	private double jumpForce;
	private Animator animator;
	private bool isClimbing = false;
	public GameObject ladder = null;
	private float gravityOriginal;
	private bool Carrying = false;
	private bool Crouched = false;
	private bool CarryingByAction = false;
	private GameObject CarriedObject;
	public bool m_isActive = false;
    public bool changeHeroAllowed = true;
	
	public bool IsActive {
		get { return m_isActive; }
		protected set { m_isActive = value; }
	}
	
	private bool m_onAir = false;
	
	public bool OnAir {
		get { return m_onAir; }
	}
	
	public abstract bool IsStrong ();

	protected virtual void Awake ()
	{
		heroInterac = GetComponentInChildren<HeroInteractor> ();
		rigidBody2D = GetComponent<Rigidbody2D> ();
		
		//get the animator
		animator = GetComponentInChildren<Animator> ();
		
		gravityOriginal = rigidBody2D.gravityScale;
		
		CalculateJumpForce ();
		CalculateWalkingMotorAngularSpeed ();
	}
	
	void FixedUpdate ()
	{
		
	}
	
	public bool isGrounded ()
	{
		//TODO better method to check if grounded
		//old way:
		//bool grounded = Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround.value | heroPlatformMask.value | mapInteractiveObjectsMask.value);
		//new way:
		bool grounded = footCollider.IsTouchingLayers (whatIsGround.value | heroPlatformMask.value | mapInteractiveObjectsMask.value);
		return grounded;
	}
	
	public bool isPushingSomething ()
	{
		return bodyCollider.IsTouchingLayers (mapInteractiveObjectsMask.value) && heroInterac.carriableObject != null;
	}

	public void Move (float speed)
	{
		bool grounded = isGrounded ();
		if (isClimbing)
			isClimbing = (heroInterac.ladder != null && heroInterac.ladder.GetComponent<ILadder>()!=null && !grounded);

		if (grounded) {
			animator.SetBool ("jumpOnAir", false);
			if (speed == 0.0f) {
				animator.SetBool ("walk", false);
				StopWalk ();
				StopPush();
				if (Carrying)
					animator.SetBool ("carry", true);
			} else {
				if (Carrying)
					animator.SetBool ("carry", true);
				else if (isPushingSomething ())
					Push ();
				else
					StopPush();
				animator.SetBool ("walk", true);
				ChangeMotorSpeed (motorMaxAngularSpeed * speed);
			}
		} else {
			if (rigidBody2D.velocity.x * Mathf.Sign (speed) < maxWalkingSpeed)
				rigidBody2D.AddForce (new Vector2 (speed * horizontalFlyingForce, 0), ForceMode2D.Impulse);
			if (Carrying)
				animator.SetBool ("carry", true);
			else
				StopPush();
			if (isClimbing){
				if (speed != 0.0f) {
					int d = speed>0?1:-1;
					isClimbing = false;
					//ladder = null;
					animator.SetBool ("jumpOnAir", true);
				}
			}
			else
				animator.SetBool ("jumpOnAir", true);
			
		}
		flipAnimation (speed);
	}
	
	public void StopWalk ()
	{
		animator.SetBool ("walk", false);
		//This command + changing Linear Drag to 1, prevents the hero slide after the jump
		rigidBody2D.velocity = new Vector2 (0, rigidBody2D.velocity.y);
		if (Carrying) {
			Rigidbody2D rbCO= CarriedObject.GetComponent<Rigidbody2D> ();
			rbCO.velocity = new Vector2 (0, rbCO.velocity.y);
		}
		ChangeMotorSpeed (0f);
	}
	
	public void VerticalMove(float speed)
	{
		bool grounded = isGrounded ();

		if (isClimbing)
			isClimbing = (heroInterac.ladder != null && heroInterac.ladder.GetComponent<ILadder>()!=null && !grounded);
		else
			isClimbing = (heroInterac.ladder != null && heroInterac.ladder.GetComponent<ILadder>()!=null && !grounded && speed != 0f && !Carrying);

		if (isClimbing){
			GravityScale = 0f;
			Transform tGO = heroInterac.ladder.transform;
			
			transform.position = new Vector2 (tGO.position.x, transform.position.y);
			rigidBody2D.velocity = new Vector2 (0, -speed * maxClimbingSpeed);
			// The code comment bellow is to be undone when the animator animating climb and stop on ladder
			/*
			if (speed ==0f){
				animator.SetBool ("climb", false);
				animator.SetBool ("stopclimb", true);
			}
			else{
				animator.SetBool ("stopclimb", false);
				animator.SetBool ("climb", true);
			}
			*/			
		} 
		else{
			GravityScale = gravityOriginal;
			// The code comment bellow is to be undone when the animator animating climb and stop on ladder
			/*
			animator.SetBool ("climb", false);
			animator.SetBool ("stopclimb", false);
			*/
		}
		
	}
	
	public void Jump ()
	{
		Jump (1);
	}

	/// <summary>
	/// Makes the Hero jump to his/her maximum height multiplied by a factor.
	/// </summary>
	/// <param name="multiplier">Maximum height factor.</param>
	public void Jump (float multiplier)
	{
		//JUMP, IF GROUDED OR ON OTHER HERO PLATFORM
		bool grounded = isGrounded ();
		
		if (grounded) {
			animator.SetTrigger ("jumpOnAir");
			
			foreach (Rigidbody2D rg2d in transform.GetComponentsInChildren<Rigidbody2D>())
				rg2d.velocity = new Vector2 (rigidBody2D.velocity.x, 0);
			
			rigidBody2D.AddForce (new Vector2 (0f, (float)jumpForce) * Mathf.Sqrt(multiplier), ForceMode2D.Impulse);
			SoundManager.Instance.SendMessage ("PlaySFXJump");
		}
	}
	
	public void Crouch ()
	{
		heroPlatform.offset = headCollider.offset - new Vector2 (0, heroPlatform.bounds.size.y);
		Crouched = true;
		//Crouch Animation
		animator.SetBool ("crouch", true);
	}
	
	public void StandUp ()
	{
		if (!Physics2D.OverlapArea (headCollider.bounds.min, headCollider.bounds.max, whatIsGround.value)) { // Do not stand up inside a short area
			heroPlatform.offset = headCollider.offset;
			Crouched = false;
			//Crouch Animation
			animator.SetBool ("crouch", false);
		}
	}
	
	public void Push ()
	{
		animator.SetBool ("push", true);
	}
	
	public void StopPush ()
	{
		animator.SetBool ("push", false);
	}
	
	public void Carry ()
	{
		if (!Carrying) {
			CarryObject();
			CarryingByAction = false;
		}
		else
			animator.SetBool ("carry", true);
	}
	
	public void StopCarry ()
	{
		if (Carrying && !CarryingByAction) {
			ReleaseObject();
		}
		animator.SetBool ("carry", false);
	}
	
	private void flipAnimation (float horizontalMove)
	{
		//Flip the animation
		if ((horizontalMove > 0 && facingDirection==_FACING_LEFT) || (horizontalMove < 0 && facingDirection==_FACING_RIGHT)) {
			// Switch the way the player is labelled as facing.
			facingDirection*=-1;
			// Multiply the player's x local scale by -1.
			Transform rendererTransform = transform.Find ("Renderer").transform;
			Vector3 theScale = rendererTransform.localScale;
			theScale.x *= -1;
			rendererTransform.localScale = theScale;
		}
	}
	
	private void CalculateJumpForce ()
	{
		//Impulse to Jump that height
		//more info look at http://hyperphysics.phy-astr.gsu.edu/hbase/impulse.html and reverse http://hyperphysics.phy-astr.gsu.edu/hbase/flobj.html#c2
		double totalMass = 0;
		//get the total mass from the he
		foreach (Rigidbody2D rg2d in transform.GetComponentsInChildren<Rigidbody2D>())
			totalMass += rg2d.mass;
		
		//Force using Math instead of Mathf, to use double instead of float. (no big result changes)
		jumpForce = ((double)totalMass) * ((double)Math.Sqrt ((double)(2D * ((double)jumpHeight) * ((double)rigidBody2D.gravityScale) * ((double)Math.Abs (Physics2D.gravity.y)))));
		//Add a epsilon to compensate for an unknown error
		jumpForce *= 1.03;
	}
	
	private void CalculateWalkingMotorAngularSpeed ()
	{
		float footRadius = walkMotor.GetComponent<CircleCollider2D> ().radius;
		
		motorMaxAngularSpeed = Mathf.Rad2Deg * maxWalkingSpeed / footRadius;
		
	}


	public void ChangeHero ()
    {
        if (changeHeroAllowed) m_isActive = !m_isActive;
	}
	
	private void ChangeMotorSpeed (float speed)
	{
		JointMotor2D tMotor = walkMotor.motor; 
		tMotor.motorSpeed = speed;
		tMotor.maxMotorTorque = walkMotorTorque;
		walkMotor.motor = tMotor;
	}
	
	public void Action ()
	{
		this.DoAction ();
	}
	
	private void DoAction ()
	{
		if (!Carrying) {
			if (!isClimbing) {
				if (heroInterac.actionableObject != null) {
					IHeroActionable iHeroActionable = heroInterac.actionableObject.GetComponent<IHeroActionable> ();
					if (iHeroActionable != null) {
						iHeroActionable.OnHeroActivate ();
					} 
				} else if (heroInterac.carriableObject != null) {
					CarryObject ();
					CarryingByAction = true;
				}
			}

		}
		else{
			ReleaseObject();
		}
		
	}
	
	public void TouchedForceField ()
	{
		float speed = 2;
		rigidBody2D.AddForce (new Vector2 (facingDirection * maxWalkingSpeed * speed, 0), ForceMode2D.Impulse);
	}
	
	public float JumpHeight {
		get {
			return this.jumpHeight;
		}
		set {
			jumpHeight = value;
			CalculateJumpForce ();
		}
	}
	
	public float WalkMotorTorque {
		get {
			return this.walkMotorTorque;
		}
		set {
			walkMotorTorque = value;
			CalculateWalkingMotorAngularSpeed ();
		}
	}
	
	public float MaxWalkingSpeed {
		get {
			return this.maxWalkingSpeed;
		}
		set {
			maxWalkingSpeed = value;
			CalculateWalkingMotorAngularSpeed ();
		}
	}
	
	public float HorizontalFlyingForce {
		get {
			return this.horizontalFlyingForce;
		}
		set {
			horizontalFlyingForce = value;
		}
	}
	
	public float GravityScale {
		get {
			return rigidBody2D.gravityScale;
		}
		
		set {
			foreach (Rigidbody2D rg2d in transform.GetComponentsInChildren<Rigidbody2D>())
				rg2d.gravityScale = value;
			CalculateJumpForce ();
		}
		
	}
	private void CarryObject(){
		Carriable carriable = heroInterac.carriableObject.GetComponent<Carriable> ();
		bool objIsInFront = heroInterac.carriableObject.transform.position.x*facingDirection > transform.position.x*facingDirection;
		if (carriable != null && !carriable.isBeingCarried && objIsInFront && (!carriable.isHeavy () || this.IsStrong ())) {
			float fator = carriable.isHeavy()?1.5f:1f;
			Carrying = true;
			CarriedObject = heroInterac.carriableObject;
			SliderJoint2D sliderJoint = GetComponent<SliderJoint2D>();
			CarriedObject.transform.rotation = new Quaternion(0, 0, 0, CarriedObject.transform.localRotation.w);
			CarriedObject.transform.position = new Vector2 (transform.position.x, transform.position.y + transform.localScale.y + CarriedObject.transform.localScale.y + offsetCarryObjHero*fator);
			sliderJoint.connectedBody = CarriedObject.GetComponent<Rigidbody2D> ();
			sliderJoint.enabled = true;
			carriable.isBeingCarried = true;
			StopPush();
			animator.SetBool ("carry", true);
		}

	}

	public void ReleaseObject(){
		Carriable carriable = CarriedObject.GetComponent<Carriable> ();
		float fator = (carriable.isHeavy()?1.5f:1);
		SliderJoint2D sliderJoint = GetComponent<SliderJoint2D>();
		sliderJoint.connectedBody = null;
		sliderJoint.enabled = false;
		if (Crouched) {
			CarriedObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			CarriedObject.transform.rotation = new Quaternion(0, 0, 0, CarriedObject.transform.localRotation.w);
			CarriedObject.transform.position = new Vector2 (transform.position.x + facingDirection * transform.localScale.x/2 + facingDirection * CarriedObject.transform.localScale.x/2, transform.position.y - transform.localScale.y + CarriedObject.transform.localScale.y + offsetCarryObjHero*fator);
		}
		else
		{
			CarriedObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (rigidBody2D.velocity.x, 0);
			CarriedObject.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (fator * facingDirection * 5f, fator * 1f), ForceMode2D.Impulse);
		}
		Carrying = false;
		CarriedObject = null;
		carriable.isBeingCarried = false;
		animator.SetBool ("carry", false);
	}


}
