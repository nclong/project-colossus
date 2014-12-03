using UnityEngine;
using System.Collections;

public class ElectricianMine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Enemy")
        {
            Debug.Log("Enemy Stepped on the Mine");
            //Show Explosion Animation

            //Do Damage to the enemy
            //coll.gameObject.

            //Make the mine disappear
            renderer.enabled = false;

            //Delete from hierarchy
            Invoke("DestroyMine", 1);
        }
        else if (coll.tag == "Player")
        {
            Debug.Log("Player Stepped on the Mine");
        }
    }

    void DestroyMine()
    {
        //Update Number of Mine in game
        Destroy(gameObject);
    }
}
