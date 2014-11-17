using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterStateController : MonoBehaviour {

    public uint State;
    public int m_controller;

    private PlayerInput playerInput;
    private CharacterMovement characterMovement;
    private IAbility[] abilities;

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
            abilities[ability.GetButton()] = ability;
        }

        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        characterMovement.Moveable = false;
        characterMovement.Rotatable = false;
        if( !playerInput.LeftJoystickIsNull ) { AddState( CharacterState.Moving ); }
        if( !playerInput.RightJoystickIsNull ) { AddState( CharacterState.Rotating ); }
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
        /* NOT YET IMPLIMENTED
         * if( playerInput.PrimaryAbility > 0.025f ) { AddState( CharacterState.Primary ); }
         * if( playerInput.SecondaryAbility > 0.025f ) { AddState( CharacterState.Secondary ); }
         */

        if( HasState(CharacterState.Ability1) )
        {
            if( abilities[0].state == AbilityState.Inactive || abilities[0].state == AbilityState.Null )
            {
                abilities[0].AbilityStart();
            }
        }
        else if( HasState(CharacterState.Ability2))
        {

        }
        else if( HasState(CharacterState.Ability3))
        {

        }
        else if( HasState(CharacterState.Ability4))
        {

        }
        else if( HasState(CharacterState.Primary))
        {

        }
        else if( HasState(CharacterState.Secondary))
        {

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
        State |= (uint)toAdd;
    }

    public void RemoveState( CharacterState toRemove )
    {
        if( ( State & (uint)toRemove ) > 0 )
        {
            State ^= (uint)toRemove;
        }
    }
}
