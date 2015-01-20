using UnityEngine;
using System.Collections;

public class PugilistPushCollision : MonoBehaviour {
    public GameObject parent;

    private CharacterMovement movement;
    private PugilistPush push;
    private float force;
    private AngleInput angle;

	// Use this for initialization
	void Start () {
        movement = parent.GetComponent<CharacterMovement>();
        push = parent.GetComponent<PugilistPush>();
        force = push.force;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

    public void OnTriggerEnter2D(Collider2D coll)
    {
        CharacterMovement playerTest = collider.gameObject.GetComponent<CharacterMovement>();
        if( playerTest != null )
        {
            angle = movement.GetRotationInput();
            coll.rigidbody2D.AddForce( new Vector2( angle.Cos, angle.Sin ) * force, ForceMode2D.Impulse );
        }
    }

    public void OnCollisionEnter2D( Collision2D coll )
    {
        CharacterMovement playerTest = collider.gameObject.GetComponent<CharacterMovement>();
        if( playerTest != null )
        {
            angle = movement.GetRotationInput();
            coll.rigidbody.AddForce( new Vector2( angle.Cos, angle.Sin ) * force, ForceMode2D.Impulse );
        }
    }
}
