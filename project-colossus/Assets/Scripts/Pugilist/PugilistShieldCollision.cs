﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PugilistShieldCollision : MonoBehaviour {

    public int hitCost;
    public GameObject parent;

    private CharacterAttributes characterAttributes;
    private List<HarmfulHitbox> harmfulsBlocked;

	// Use this for initialization
	void Start () {
        harmfulsBlocked = new List<HarmfulHitbox>();
        transform.localScale = transform.localScale.PerspectiveAdjusted();
        characterAttributes = (CharacterAttributes)parent.GetComponent<CharacterAttributes>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        GameObject collisionObject = collider.gameObject;
        HarmfulHitbox hitbox = collisionObject.GetComponent<HarmfulHitbox>() as HarmfulHitbox;
        if ( hitbox != null )
        {

            if( collisionObject.tag != "Projectile" )
            {
                characterAttributes.ModifyResource( -hitCost );
                harmfulsBlocked.Add( hitbox );
            }

            if( collisionObject.tag == "Projectile" )
            {
                characterAttributes.ModifyHealth( -hitCost );
                Destroy( collisionObject );
            }
        }
    }
}