using UnityEngine;
using System.Collections;

public class ElectricianMine : MonoBehaviour {

    public int mine_damage;
    public int blast_radius;

    public GameObject player;

    private ElectricianPlaceMine place_mine_script;

	// Use this for initialization
	void Start () {
        place_mine_script = (ElectricianPlaceMine) player.GetComponent<ElectricianPlaceMine>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Enemy")
        {
            //Enemy stepped on the mine, explode in 1 second
            Invoke("MineExplode", 1);

            //Mine exploded, make the mine disappear
            renderer.enabled = false;

            //Delete from hierarchy
            Invoke("DestroyMine", 1);
        }

    }

    void MineExplode()
    {
        //Show Explosion Animation

        //Get all enemy in blast range
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Collider[] enemy_in_range = Physics.OverlapSphere(pos, blast_radius);

        foreach (Collider enemy in enemy_in_range)
        {
            if (enemy.gameObject.CompareTag("Enemy"))
            {
                //Deal damage to enemy in blast range
                CharacterAttributes enemy_attribute = enemy.GetComponentInParent<CharacterAttributes>();
                enemy_attribute.ModifyHealth(-mine_damage);

                Debug.Log(enemy.transform.root.name + " has taken " + mine_damage + " damage.");
            }
        }
    }

    void DestroyMine()
    {
        //Update Number of Mine in game
        place_mine_script.current_mine--;
        Destroy(gameObject);
    }
}
