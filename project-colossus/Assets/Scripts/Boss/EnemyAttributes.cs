﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class EnemyAttributes : MonoBehaviour {

    public int MaxHealth;
    public int Defense;
    public Text healthLabel;
    public int CurrentHealth { get; private set; }

    private Dictionary<GameObject, int> aggroDict;

	// Use this for initialization
	void Start () {
        aggroDict = new Dictionary<GameObject, int>();
        CurrentHealth = MaxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	    if( CurrentHealth <= 0 )
        {
            CurrentHealth = 0;
            Destroy( transform.gameObject );
            GameStateManager.PlayersWin();
        }

        healthLabel.text = CurrentHealth.ToString() + " / " + MaxHealth.ToString();
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

    public GameObject GetAggroLeader()
    {
        if( aggroDict.Count == 0 )
        {
            return null;
        }

        int max = 0;
        GameObject goMax = null;
        foreach( GameObject go in aggroDict.Keys)
        {
            if( aggroDict[go] > max)
            {
                max = aggroDict[go];
                goMax = go;
            }
        }

        return goMax;
    }
}