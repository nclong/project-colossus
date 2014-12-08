using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed;
    public float life;

    private Vector3 velocity;
    private bool active;
    private float activeTimer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}

    void FixedUpdate()
    {
        //rigidbody.velocity = velocity;
    }

    void Update()
    {
        if( activeTimer >= life)
        {
            active = false;
        }

        if( active )
        {
            activeTimer += Time.deltaTime;
        }
        else
        {
            Destroy( transform.gameObject );
        }
    }

    public void SetTargetByDest(Vector3 dest)
    {
        velocity = ( dest - transform.position ).normalized.PerspectiveAdjusted() * speed;
        rigidbody.velocity = velocity;
    }

    public void SetTargetByDirection(Vector3 dir)
    {
        velocity = dir.normalized.PerspectiveAdjusted() * speed;
        rigidbody.velocity = velocity;
    }

    public void Launch()
    {
        active = true;
    }
}
