using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterStateController : MonoBehaviour {

    public string characterClass;
    public uint State;
    public int m_controller;

    private PlayerInput playerInput;
    private CharacterMovement characterMovement;
    private IAbility[] abilities;
    private SecondaryAbility secondaryAbility;
    private PrimaryAbility primaryAbility;
    private CharacterAttributes attributes;

	public bool primary;

	// Use this for initialization
	void Start () {
        playerInput = InputManager.Players[m_controller];
        //Get All Abilities on the Character Object
        abilities = new IAbility[4];
        List<MonoBehaviour> behaviourList = new List<MonoBehaviour>();
        GetComponents<MonoBehaviour>( behaviourList );
        List<IAbility> abilityList = new List<IAbility>();
        foreach( MonoBehaviour behaviour in behaviourList )
        {
            if( behaviour is IAbility )
            {
                abilityList.Add( (IAbility)behaviour );
            }
        }
        foreach( IAbility ability in abilityList )
        {
			//GetButton only returns 0 for some reason;
            abilities[ability.GetButton()] = ability;

        }


        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        attributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        secondaryAbility = (SecondaryAbility)GetComponent<SecondaryAbility>();
        primaryAbility = (PrimaryAbility)GetComponent<PrimaryAbility>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (characterClass == "electrician") {
						EndAbilities ();
		}
		primary = HasState (CharacterState.Primary);
        characterMovement.Moveable = false;
        characterMovement.Rotatable = false;
        if( !playerInput.LeftJoystickIsNull ) { AddState( CharacterState.Moving ); }
        if( !playerInput.RightJoystickIsNull ) { AddState( CharacterState.Rotating ); }

		//abilities[1].state is null;
		//Debug.Log (abilities [0]);
		//Debug.Log (abilities [1]);

        if( playerInput.Abilities[0] && (abilities[0].state == AbilityState.Inactive || abilities[0].state == AbilityState.Null))
        {
            AddState( CharacterState.Ability1 );
        }
        if( playerInput.Abilities[1] && ( abilities[1].state == AbilityState.Inactive || abilities[1].state == AbilityState.Null ) )
        {
            AddState( CharacterState.Ability2 );
        }
        if( playerInput.Abilities[2] && ( abilities[2].state == AbilityState.Inactive || abilities[2].state == AbilityState.Null ) )
        {
            AddState( CharacterState.Ability3 );
        }
        if( playerInput.Abilities[3] && ( abilities[3].state == AbilityState.Inactive || abilities[3].state == AbilityState.Null ) )
        {
            AddState( CharacterState.Ability4 );
        }

        if( playerInput.PrimaryAbility > InputManager.GeneralEpsilon )
        {
            AddState( CharacterState.Secondary );
        }
        else if( playerInput.PrimaryAbility < InputManager.GeneralEpsilon * -1 )
        {
            AddState( CharacterState.Primary );
        }
        else
        {
            RemoveState( CharacterState.Secondary );
            RemoveState( CharacterState.Primary );
        }

        if( attributes.CurrentHealth <= 0 )
        {
            AddState( CharacterState.KnockedOut );
            attributes.SetHealth( 0 );
        }


        if( HasState(CharacterState.Ability1) )
        {
            if( abilities[0].state == AbilityState.Inactive || abilities[0].state == AbilityState.Null )
            {
                abilities[0].AbilityStart();
            }
        }
        else if( HasState(CharacterState.Ability2))
        {
            if( abilities[1].state == AbilityState.Inactive || abilities[1].state == AbilityState.Null )
            {
                abilities[1].AbilityStart();
            }
        }
        else if( HasState(CharacterState.Ability3))
        {
            if( abilities[2].state == AbilityState.Inactive || abilities[2].state == AbilityState.Null )
            {
                abilities[2].AbilityStart();
            }
        }
        else if( HasState(CharacterState.Ability4))
        {
            if( abilities[3].state == AbilityState.Inactive || abilities[3].state == AbilityState.Null )
            {
                abilities[3].AbilityStart();
            }
        }
        else if( HasState(CharacterState.Primary))
        {
            if( primaryAbility.state == AbilityState.Inactive || primaryAbility.state == AbilityState.Null )
            {
                primaryAbility.AbilityStart();
            }
        }
        else if( HasState(CharacterState.Secondary))
        {
            if( secondaryAbility.state == AbilityState.Inactive || secondaryAbility.state == AbilityState.Null )
            {
                secondaryAbility.AbilityStart();
            }
        }
        else if( HasState(CharacterState.Moving) || HasState(CharacterState.Rotating))
        {
            if( HasState(CharacterState.Moving))
            {
                characterMovement.Moveable = true;
            }
            if (HasState( CharacterState.Rotating))
            {
                characterMovement.Rotatable = true;
            }
        }
	}

    public void OnCollisionEnter2D(Collision2D collider)
    {
        bool reflect;
        PugilistReflect possibleReflect;
        try
        {
            possibleReflect = (PugilistReflect)abilities[2];
        }
        catch( InvalidCastException )
        {
            possibleReflect = null;
        }

        reflect = 
        possibleReflect != null 
            ? possibleReflect.state == AbilityState.Active 
            : false;
            
        if( reflect )
        {
            Projectile proj = (Projectile)collider.gameObject.GetComponent<Projectile>();
            if( proj != null )
            {
                HarmfulHitbox oldBox = (HarmfulHitbox)collider.gameObject.GetComponent<HarmfulHitbox>();
                FriendlyHitbox newBox = (FriendlyHitbox)collider.gameObject.AddComponent<FriendlyHitbox>();
                newBox.damage = oldBox.damage * 2;
                oldBox.enabled = false;
                collider.gameObject.rigidbody.velocity *= -2;
            } 
        }
        else
        {
            HarmfulHitbox hitbox = collider.gameObject.GetComponent<HarmfulHitbox>();
            if( hitbox != null )
            {
                attributes.ModifyHealth( -hitbox.damage );
                if( collider.gameObject.tag == "Projectile" )
                {
                    if( characterClass == "thaumaturge" 
                        && secondaryAbility.state == AbilityState.Active 
                        && ((Projectile)collider.gameObject.GetComponent<Projectile>()).absorbable )
                    {
                        attributes.ModifyHealth( hitbox.damage );
                        attributes.ModifyResource( hitbox.damage );
                    }
                    Destroy( collider.gameObject );
                }
            } 
        }
    }

    public void EndAbilities()
    {
        RemoveState( CharacterState.Ability1 );
        RemoveState( CharacterState.Ability2 );
        RemoveState( CharacterState.Ability3 );
        RemoveState( CharacterState.Ability4 );

    }

    public bool HasState( CharacterState check )
    {
        return ( State & (uint)check ) == (uint)check;
    }

    public void AddState( CharacterState toAdd )
    {
        uint toAddInt = (uint)toAdd;
        State |= toAddInt;
    }

    public void RemoveState( CharacterState toRemove )
    {
        if( ( State & (uint)toRemove ) > 0 )
        {
            State ^= (uint)toRemove;
        }
    }
}
