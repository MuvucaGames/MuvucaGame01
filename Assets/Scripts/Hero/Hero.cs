using UnityEngine;
using System.Collections;

public abstract class Hero : MonoBehaviour {

	[SerializeField] private float MaxWalkingSpeed = 5f;
	[SerializeField] private float WalkForce = 40f;
	[SerializeField] private float JumpHeight = 1f;
	[SerializeField] private LayerMask whatIsGround;


	private bool m_FacingRight = true;
	private Rigidbody2D rigidBody2D;

	void Awake(){
		rigidBody2D = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){





	}

	public void Move(float horizontalMove, bool crouch, bool jump){
		//TEST
		rigidBody2D.AddForce(new Vector2(Input.GetAxis("Horizontal")*WalkForce,0), ForceMode2D.Impulse);
		
		//LIMIT WWALKING SPEED
		if (Mathf.Abs (rigidBody2D.velocity.x) > MaxWalkingSpeed) {
			rigidBody2D.velocity = new Vector2(Mathf.Sign(rigidBody2D.velocity.x) * MaxWalkingSpeed , rigidBody2D.velocity.y);
		}

		if(jump && Physics2D.OverlapCircle(transform.position, 0.2f, whatIsGround.value)){
			//Impulse to Jump that height
			//more info look at http://hyperphysics.phy-astr.gsu.edu/hbase/impulse.html and reverse http://hyperphysics.phy-astr.gsu.edu/hbase/flobj.html#c2
			rigidBody2D.AddForce(new Vector2(0f, rigidBody2D.mass*Mathf.Sqrt(2f*JumpHeight*rigidBody2D.gravityScale*Mathf.Abs(Physics2D.gravity.y))), ForceMode2D.Impulse);
		
		}

	}

}
