using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    public float distanceFromGoal;
    public float topLimit;
    public float bottomLimit;
    public float leftLimit;
    public float rightLimit;
    public float speed;
    public bool moveable = true;

    public Vector3 targetDestination;
    private EnemyAttributes attributes;
    private bool targetSet = false;

	// Use this for initialization
	void Start () {
        attributes = (EnemyAttributes)GetComponent<EnemyAttributes>();
	}

    void FixedUpdate()
    {
        if( moveable )
        {
            float distance = Vector3.Distance( transform.position, targetDestination );
            if( targetSet && distance > distanceFromGoal )
            {
                rigidbody.velocity = ( targetDestination - transform.position ).normalized.PerspectiveAdjusted() * speed;
            }

            if( distance <= distanceFromGoal )
            {
                targetSet = false;
            }

        }
    }
	// Update is called once per frame
	void Update () {
        GameObject target = attributes.GetAggroLeader();
        if( target == null )
        {
            if( !targetSet )
            {
                targetDestination = new Vector3( Random.Range( leftLimit, rightLimit ), transform.position.y, Random.Range( bottomLimit, topLimit ) );
                targetSet = true; 
            }
        }
        else
        {
            targetDestination = target.transform.position;
        }

	}
}
