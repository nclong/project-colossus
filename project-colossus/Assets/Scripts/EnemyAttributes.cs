using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttributes : MonoBehaviour {

    public int MaxHealth;
    public int MaxResource;
    public int Defense;
    public Text healthLabel;
    public Text resourceLabel;
    public int CurrentHealth { get; private set; }
    public int CurrentResource { get; private set; }

    private Dictionary<GameObject, int> aggroDict;

	// Use this for initialization
	void Start () {
        CurrentHealth = MaxHealth;
        CurrentResource = MaxResource;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void AddAggro(GameObject go, int aggro)
    {
        if( aggroDict.ContainsKey(go) )
        {
            aggroDict[go] += aggro;
        }
        else
        {
            aggroDict[go] = aggro;
        }
    }

    public void SetHealth( int newHealth )
    {
        CurrentHealth = newHealth;
    }

    public void ModifyHealth( int offset )
    {
        if( CurrentHealth + offset >= MaxHealth )
            CurrentHealth = MaxHealth;
        else
            CurrentHealth += offset;
    }

    public void SetResource( int newResource )
    {
        CurrentResource = newResource;
    }

    public void ModifyResource( int offset )
    {
        if( CurrentResource + offset >= MaxResource )
            CurrentResource = MaxResource;
        else
            CurrentResource += offset;
    }
}
