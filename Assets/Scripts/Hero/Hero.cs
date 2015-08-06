using UnityEngine;
using System.Collections;
using System;

public abstract class Hero : MonoBehaviour {

	[SerializeField] private float MaxWalkingSpeed = 5f;
	[SerializeField] private float WalkForce = 40f;
	[SerializeField] private float JumpHeight = 1f;
	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] private LayerMask heroPlatformMask;
	[SerializeField] private LayerMask mapInteractiveObjectsMask;
	[SerializeField] private Collider2D headCollider;
	[SerializeField] private Collider2D heroPlatform;

	protected bool m_isActive = false;
	public bool IsActive{
		get { return m_isActive; }
		protected set { m_isActive = value; }
	}
	private bool m_onAir = false;
	public bool OnAir{
		get { return m_onAir; }
	}

	private bool m_FacingRight = true;
	private Rigidbody2D rigidBody2D;

	private double jumpForce;
	private Animator animator;

	protected virtual void Awake(){
		rigidBody2D = GetComponent<Rigidbody2D> ();

		//get the animator
		animator = GetComponentInChildren<Animator> ();

		//Force using Math instead of Mathf, to use double instead of float. (no big result changes)
		jumpForce = ((double)rigidBody2D.mass) * ((double)Math.Sqrt ((double)(2D * ((double)JumpHeight) * ((double)rigidBody2D.gravityScale) * ((double)Math.Abs (Physics2D.gravity.y)))));
	}

	void FixedUpdate(){

	}

	public void Move(float horizontalMove, bool crouch, bool jump){

		//WALK HORIZONTALY
		rigidBody2D.AddForce (new Vector2 (horizontalMove * WalkForce, 0), ForceMode2D.Impulse);
	
		//LIMIT WWALKING SPEED
		if (Mathf.Abs (rigidBody2D.velocity.x) > MaxWalkingSpeed) {
			rigidBody2D.velocity = new Vector2 (Mathf.Sign (rigidBody2D.velocity.x) * MaxWalkingSpeed, rigidBody2D.velocity.y);
		}

		//SET WALKING ANIMATION
		animator.SetBool ("walk", Mathf.Abs (horizontalMove) > 0);

		//CROUCH
		if (crouch && !headCollider.isTrigger) {
			//Transfor head in trigger
			headCollider.isTrigger = crouch;
				heroPlatform.offset = heroPlatform.offset - new Vector2 (0f, headCollider.bounds.extents.y * 2);
			//Crouch Animation
			animator.SetBool ("crouch", true);
		} else if (!crouch && headCollider.isTrigger && !Physics2D.OverlapArea (headCollider.bounds.min, headCollider.bounds.max, whatIsGround.value)) {
			headCollider.isTrigger = crouch;
			heroPlatform.offset = headCollider.offset;
			//Crouch Animatio
			animator.SetBool ("crouch", false);
		}

		//JUMP, IF GROUDED OR ON OTHER HERO PLATFORM
		if (jump && Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround.value | heroPlatformMask.value | mapInteractiveObjectsMask.value)) {
			//Impulse to Jump that height
			//more info look at http://hyperphysics.phy-astr.gsu.edu/hbase/impulse.html and reverse http://hyperphysics.phy-astr.gsu.edu/hbase/flobj.html#c2
			rigidBody2D.AddForce (new Vector2 (0f, (float)jumpForce), ForceMode2D.Impulse);
		}

		if (Physics2D.OverlapCircle (transform.position, 0.2f, whatIsGround.value | heroPlatformMask.value | mapInteractiveObjectsMask.value))
			m_onAir = false;
		else
			m_onAir = true;

		if ((horizontalMove > 0 && !m_FacingRight) || (horizontalMove < 0 && m_FacingRight)) {
			//Flip the animation
			// Switch the way the player is labelled as facing.
			m_FacingRight = !m_FacingRight;
		
			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

	}

	public void ChangeHero(){
		m_isActive = !m_isActive;
	}
}
