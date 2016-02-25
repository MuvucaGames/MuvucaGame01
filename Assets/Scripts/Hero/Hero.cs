using UnityEngine;
using System.Collections;
using System;

public abstract class Hero : MonoBehaviour {

	[SerializeField] private float maxWalkingSpeed = 5f;
	[SerializeField] private float walkMotorTorque = 40f;
	[SerializeField] private float horizontalFlyingForce = 0.5f;
	[SerializeField] private float jumpHeight = 1f;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private LayerMask heroPlatformMask;
	[SerializeField] private LayerMask mapInteractiveObjectsMask;
	[SerializeField] private Collider2D headCollider = null;
	[SerializeField] private Collider2D heroPlatform = null;
	[SerializeField] private Collider2D bodyCollider = null;
	[SerializeField] private HingeJoint2D walkMotor = null;
	[SerializeField] private Collider2D footCollider = null;

	private float motorMaxAngularSpeed = 0f;
	private bool m_FacingRight = true;
	private Rigidbody2D rigidBody2D;
	private double jumpForce;
	private Animator animator;


	public bool m_isActive = false;
	public bool IsActive{
		get { return m_isActive; }
		protected set { m_isActive = value; }
	}

	private bool m_onAir = false;
	public bool OnAir{
		get { return m_onAir; }
	}


	protected virtual void Awake(){
		rigidBody2D = GetComponent<Rigidbody2D> ();

		//get the animator
		animator = GetComponentInChildren<Animator> ();

		CalculateJumpForce ();
		CalculateWalkingMotorAngularSpeed ();
	}

	void FixedUpdate(){

	}

	public bool isGrounded() {
		//TODO better method to check if grounded
		//old way:
		//bool grounded = Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround.value | heroPlatformMask.value | mapInteractiveObjectsMask.value);
		//new way:
		bool grounded = footCollider.IsTouchingLayers (whatIsGround.value | heroPlatformMask.value | mapInteractiveObjectsMask.value);
		return grounded;
	}

	public void Move(float speed) {
		bool grounded = isGrounded ();
		if (grounded) {
			animator.SetBool ("jumpOnAir", false);
			if (speed == 0.0f) {
				animator.SetBool ("walk", false);
				StopWalk ();
			}
			else {
				animator.SetBool ("walk", true);
				ChangeMotorSpeed (motorMaxAngularSpeed * speed);
			}
		} else {
			if (rigidBody2D.velocity.x * Mathf.Sign(speed) < maxWalkingSpeed)
				rigidBody2D.AddForce (new Vector2 (speed * horizontalFlyingForce, 0), ForceMode2D.Impulse);
			animator.SetBool ("jumpOnAir", true);
		}
		flipAnimation(speed);
	}

	public void Jump() {
		//JUMP, IF GROUDED OR ON OTHER HERO PLATFORM
		bool grounded = isGrounded ();
		if (grounded) {
			animator.SetTrigger("jumpStart");
			foreach (Rigidbody2D rg2d in transform.GetComponentsInChildren<Rigidbody2D>())
				rg2d.velocity = new Vector2(rigidBody2D.velocity.x, 0);
			rigidBody2D.AddForce (new Vector2 (0f, (float)jumpForce), ForceMode2D.Impulse);
			SoundManager.Instance.SendMessage("PlaySFXJump");
		}
	}

	public void Crouch() {
		heroPlatform.offset = headCollider.offset -  new Vector2(0, heroPlatform.bounds.size.y);
		//Crouch Animation
		animator.SetBool ("crouch", true);
	}

	public void StandUp() {
		if (!Physics2D.OverlapArea (headCollider.bounds.min, headCollider.bounds.max, whatIsGround.value)) { // Do not stand up inside a short area
			heroPlatform.offset = headCollider.offset;
			//Crouch Animation
			animator.SetBool ("crouch", false);
		}
	}

	public void Push() {
		animator.SetBool ("push", true);
	}

	public void StopPush() {
		animator.SetBool ("push", false);
	}

	public void Carry() {
		animator.SetBool ("carry", true);
	}

	public void StopCarry() {
		animator.SetBool ("carry", false);
	}

	private void flipAnimation(float horizontalMove)	{
		//Flip the animation
		if ((horizontalMove > 0 && !m_FacingRight) || (horizontalMove < 0 && m_FacingRight)) {
			// Switch the way the player is labelled as facing.
			m_FacingRight = !m_FacingRight;
			// Multiply the player's x local scale by -1.
			Transform rendererTransform = transform.Find ("Renderer").transform;
			Vector3 theScale = rendererTransform.localScale;
			theScale.x *= -1;
			rendererTransform.localScale = theScale;
		}
	}

	private void CalculateJumpForce(){
		//Impulse to Jump that height
		//more info look at http://hyperphysics.phy-astr.gsu.edu/hbase/impulse.html and reverse http://hyperphysics.phy-astr.gsu.edu/hbase/flobj.html#c2
		double totalMass = 0;
		//get the total mass from the hero
		foreach (Rigidbody2D rg2d in transform.GetComponentsInChildren<Rigidbody2D>())
			totalMass += rg2d.mass;

		//Force using Math instead of Mathf, to use double instead of float. (no big result changes)
		jumpForce = ((double)totalMass) * ((double)Math.Sqrt ((double)(2D * ((double)jumpHeight) * ((double)rigidBody2D.gravityScale) * ((double)Math.Abs (Physics2D.gravity.y)))));
		//Add a epsilon to compensate for an unknown error
		jumpForce *= 1.03;
	}

	private void CalculateWalkingMotorAngularSpeed(){
		float footRadius = walkMotor.GetComponent<CircleCollider2D>().radius;

		motorMaxAngularSpeed = Mathf.Rad2Deg * maxWalkingSpeed/footRadius;

	}

	public void ChangeHero(){
		m_isActive = !m_isActive;
	}

	public void StopWalk(){
		animator.SetBool ("walk", false);
		//This command + changing Linear Drag to 1, prevents the hero slide after the jump
		rigidBody2D.velocity = new Vector2 (0, rigidBody2D.velocity.y);
		ChangeMotorSpeed (0f);
	}

	private void ChangeMotorSpeed(float speed){
		JointMotor2D tMotor = walkMotor.motor; 
		tMotor.motorSpeed = speed;
		tMotor.maxMotorTorque = walkMotorTorque;
		walkMotor.motor = tMotor;
	}

	public void Action() {
		this.DoAction ();
	}

    private void DoAction()
    {
        Renderer r = GetComponentInChildren<Renderer>();
        Vector2 a = new Vector2(transform.position.x - r.bounds.extents.x, transform.position.y - r.bounds.extents.x);
        Vector2 b = new Vector2(transform.position.x + r.bounds.extents.x, transform.position.y + r.bounds.extents.x);
        Collider2D coll = Physics2D.OverlapArea(a, b, 1 << 11);

        if (coll != null)
        {
            switch (coll.tag)
            {
                case "Lever":
                    {

                        coll.SendMessage("ChangeState");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }

    private void TouchedForceField()
    {
        float speed = 2;

        if (m_FacingRight)
        {
            rigidBody2D.AddForce(new Vector2(maxWalkingSpeed * -speed, 0), ForceMode2D.Impulse);
        }
        else
        {
            rigidBody2D.AddForce(new Vector2(maxWalkingSpeed * speed, 0), ForceMode2D.Impulse);
        }
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
			CalculateWalkingMotorAngularSpeed();
		}
	}

	public float MaxWalkingSpeed {
		get {
			return this.maxWalkingSpeed;
		}
		set {
			maxWalkingSpeed = value;
			CalculateWalkingMotorAngularSpeed();
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

	public float GravityScale{
		get {
			return rigidBody2D.gravityScale;
		}

		set {
			foreach (Rigidbody2D rg2d in transform.GetComponentsInChildren<Rigidbody2D>())
				rg2d.gravityScale = value;

			CalculateJumpForce();
		}

	}

}
