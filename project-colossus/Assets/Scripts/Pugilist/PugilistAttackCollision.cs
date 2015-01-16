using UnityEngine;
using System.Collections;

public class PugilistAttackCollision : MonoBehaviour {
    public GameObject parent;

    private PugilistAttack attackComp;
    private int damage;
    private int aggro;

	// Use this for initialization
	void Start () {
        attackComp = (PugilistAttack)parent.GetComponent<PugilistAttack>();
        damage = attackComp.damage;
        aggro = attackComp.aggroGenerated;
	}
	
	void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyAttributes enemy = (EnemyAttributes)collider.gameObject.GetComponent<EnemyAttributes>();
        if( enemy != null )
        {
            enemy.ModifyHealth( -damage );
            enemy.AddAggro(parent, aggro );
        }
    }
}
