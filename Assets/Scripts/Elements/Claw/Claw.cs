using UnityEngine;
using System.Collections;

public class Claw : MonoBehaviour {

    public Vector3 clawInitialPos;
    protected Rigidbody2D heroStrongRigidBody = null;
    protected Rigidbody2D heroFastRigidBody = null;
    protected HeroStrong heroStrong = null;
    protected HeroFast heroFast = null;
    protected Collider2D coll;
    protected ClawController controller = null;


    // Use this for initialization
    void Awake () {
        controller = GetComponentInParent<ClawController>();
        coll = GetComponent<Collider2D>();
        clawInitialPos = coll.bounds.center;
        heroStrong = FindObjectOfType<HeroStrong> ();
        heroFast = FindObjectOfType<HeroFast> ();
        heroStrongRigidBody = heroStrong.GetComponent<Rigidbody2D>();
        heroFastRigidBody = heroFast.GetComponent<Rigidbody2D>();
    }
}
