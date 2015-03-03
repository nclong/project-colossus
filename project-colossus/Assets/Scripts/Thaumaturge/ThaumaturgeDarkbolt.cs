using UnityEngine;
using System.Collections;

public class ThaumaturgeDarkbolt : MonoBehaviour {
	
    private static Transform playerPos;
    public KeyCode key;
    public GameObject projectile;
    private GameObject boss;

    // Use this for initialization
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("EnemyObject");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(key))
        {
            GameObject proj = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
            Projectile projComponent = proj.GetComponent<Projectile>();
            projComponent.SetTargetByDest(boss.transform.position);
            projComponent.Launch();
        }
        playerPos = transform;
    }
}

