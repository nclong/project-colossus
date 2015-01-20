using UnityEngine;
using System.Collections;

public class DeathFullSwing : MonoBehaviour {
    public float guarenteeThreshold;
    private float chance;
    public float chanceGrowthPerSecond;
    public GameObject swingObject;
    public float swingLength;

    private float currentCounter;
    private bool swinging;
    private float attackTimer;
    private EnemyMovement movement;
	// Use this for initialization
	void Start () {
        attackTimer = 0f;
        movement = (EnemyMovement)GetComponent<EnemyMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        swingObject.transform.localPosition = new Vector3( 0f, 0f, 0.26f );
        if( !swinging )
        {
            movement.moveable = true;
            attackTimer = 0f;
            if( Random.Range( chance, 101f ) >= 100f )
            {
                swinging = true;
            }
 
            if( chance >= guarenteeThreshold )
            {
                swinging = true;
            }

            chance += Time.deltaTime * chanceGrowthPerSecond;
        }
        else
        {
            movement.moveable = false;
            chance = 0f;
            if( attackTimer < swingLength )
            {
                swingObject.SetActive( true );
                swingObject.transform.eulerAngles = new Vector3( swingObject.transform.eulerAngles.x, swingObject.transform.eulerAngles.y, ( attackTimer * 360f ) / swingLength );
            }
            else
            {
                swingObject.transform.eulerAngles = new Vector3( swingObject.transform.eulerAngles.x, swingObject.transform.eulerAngles.y, 0f );
                swingObject.SetActive( false );
                swinging = false;
            }

            attackTimer += Time.deltaTime;
        }
	}
}
