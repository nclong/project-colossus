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
	void FixedUpdate () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "EnemyObject")
        {
            //Enemy stepped on the mine, explode in 1 second
            MineExplode();
            renderer.enabled = false;
            DestroyMine();
        }

    }

    void MineExplode()
    {
        //Show Explosion Animation

        //Get all enemy in blast range
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        //Collider[] enemy_in_range = Physics.OverlapSphere(pos, blast_radius);
        Collider2D[] enemy_in_range = Physics2D.OverlapCircleAll( pos.In2D(), blast_radius );

        foreach (Collider2D enemy in enemy_in_range)
        {
            if (enemy.gameObject.CompareTag("EnemyObject"))
            {
                //Deal damage to enemy in blast range
                EnemyAttributes enemy_attribute = enemy.GetComponent<EnemyAttributes>();
                enemy_attribute.ModifyHealth(-mine_damage);
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
