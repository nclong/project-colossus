﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterAttributes : MonoBehaviour {

	public int MaxHealth;
	public int MaxResource;
	public int Defense;
    public Text healthLabel;
    public Text resourceLabel;
	public int CurrentHealth { get; private set; }
	public int CurrentResource { get; private set; }

	void Start()
	{
		CurrentHealth = MaxHealth;
        CurrentResource = MaxResource;
        healthLabel.text = "Health: " + CurrentHealth.ToString() + " / " + MaxHealth.ToString();
        resourceLabel.text = "Mana: " + CurrentResource.ToString() + " / " + MaxResource.ToString();
	}

    void FixedUpdate()
    {
        healthLabel.text = "Health: " + CurrentHealth.ToString() + " / " + MaxHealth.ToString();
        resourceLabel.text = "Mana: " + CurrentResource.ToString() + " / " + MaxResource.ToString();

        if( CurrentResource < 0 )
        {
            CurrentResource = 0;
        }

        if( CurrentResource > MaxResource )
        {
            CurrentResource = MaxResource;
        }

        if( CurrentHealth > MaxHealth )
        {
            CurrentHealth = MaxHealth;
        }

        if( CurrentHealth <= 0 )
        {
            Destroy( transform.gameObject );
            GameStateManager.playersDead++;
        }
    }

	public void SetHealth( int newHealth )
	{
		CurrentHealth = newHealth;
	}

	public void ModifyHealth( int offset )
	{
		if(CurrentHealth + offset >= MaxHealth)
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
		if(CurrentResource + offset >= MaxResource)
			CurrentResource = MaxResource;
		else
			CurrentResource += offset;
	}
}
