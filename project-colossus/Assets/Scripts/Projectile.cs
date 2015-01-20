using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float speed;
    public float life;
    public bool absorbable;    

    private Vector3 velocity;
    private bool active;
    private float activeTimer = 0.0f;

	// Use this for initialization
	void Start () {
	
	}

    void FixedUpdate()
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
        velocity = ( dest - transform.position ).normalized * speed;
        rigidbody2D.velocity = velocity.In2D();
    }

    public void SetTargetByDirection(Vector3 dir)
    {
        velocity = dir.normalized * speed;
        rigidbody2D.velocity = velocity.In2D();
    }

    public void SetTargetByAngle_Deg(float deg)
    {
        velocity = new Vector2( Mathf.Cos( deg * Mathf.Deg2Rad ), Mathf.Sin( deg * Mathf.Deg2Rad )).normalized * speed;
        rigidbody2D.velocity = velocity;
    }

    public void SetTargetByAngle_Rad(float rad)
    {
        velocity = new Vector2( Mathf.Cos( rad ), Mathf.Sin( rad ) ).normalized * speed;
        rigidbody.velocity = velocity;
    }

    public void Launch()
    {
        active = true;
    }
}
