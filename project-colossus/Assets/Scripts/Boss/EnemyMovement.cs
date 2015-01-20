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

    public Vector2 targetDestination;
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
            float distance = Vector2.Distance( transform.position, targetDestination );
            if( targetSet && distance > distanceFromGoal )
            {
                rigidbody2D.velocity = ( targetDestination - transform.position.In2D() ).normalized * speed;
            }

            if( distance <= distanceFromGoal )
            {
                targetSet = false;
            }
            GameObject target = attributes.GetAggroLeader();
            if( target == null )
            {
                if( !targetSet )
                {
                    targetDestination = new Vector2( Random.Range( leftLimit, rightLimit ), Random.Range( bottomLimit, topLimit ) );
                    targetSet = true;
                }
            }
            else
            {
                targetDestination = target.transform.position;
            }
        }
    }
}
