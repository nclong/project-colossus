using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathProjectile : MonoBehaviour {
    public float guarenteeThreshold;
    private float chance;
    public float chanceGrowthPerSecond;
    public GameObject projectilePrefab;
    private GameObject[] players;

	// Use this for initialization
	void Start () {
        players = GameObject.FindGameObjectsWithTag( "PlayerObject" );
	}
	
	// Update is called once per frame
	void Update () {
        if( Random.Range( chance, 101f ) >= 100f )
        {
            GameObject proj = (GameObject)Instantiate( projectilePrefab, transform.position, transform.rotation );
            Projectile projComponent = proj.GetComponent<Projectile>();
            projComponent.SetTargetByDest( players[Random.Range( 0, 3 )].transform.position );
            projComponent.Launch();
            chance = 0f;
        }

        if( chance >= guarenteeThreshold )
        {
            GameObject proj = (GameObject)Instantiate( projectilePrefab, transform.position, transform.rotation );
            Projectile projComponent = proj.GetComponent<Projectile>();
            projComponent.SetTargetByDest( players[Random.Range( 0, 3 )].transform.position );
            projComponent.Launch();

            chance = 0f;
        }

        chance += Time.deltaTime * chanceGrowthPerSecond;
	}
}
