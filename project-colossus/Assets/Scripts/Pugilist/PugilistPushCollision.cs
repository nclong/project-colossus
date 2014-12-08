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
	void Update () {
	
	}

    public void OnTriggerEnter(Collider collider)
    {
        CharacterMovement playerTest = collider.gameObject.GetComponent<CharacterMovement>();
        if( playerTest != null )
        {
            angle = movement.GetRotationInput();
            collider.rigidbody.AddForce( new Vector3( angle.Cos, 0f, angle.Sin ).PerspectiveAdjusted() * force, ForceMode.Impulse );
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        CharacterMovement playerTest = collision.gameObject.GetComponent<CharacterMovement>();
        if( playerTest != null )
        {
            angle = movement.GetRotationInput();
            collision.rigidbody.AddForce( new Vector3( angle.Cos, 0f, angle.Sin ).PerspectiveAdjusted() * force, ForceMode.Impulse );
        }
    }
}
