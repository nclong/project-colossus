using UnityEngine;
using System.Collections;

public class PugilistCharge : SecondaryAbility {

    [Range(0.0f, 3.0f)]
    public int m_controller;
    public int chargeAmount;

    private CharacterMovement characterMovement;
    private CharacterAttributes characterAttributes;
    private PlayerInput playerInput;
    private int framesToTick;
	private int framesCharging = 0;
    bool startedUpdating = false;

	// Use this for initialization
	void Start () {
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        state = AbilityState.Inactive;
        playerInput = InputManager.Players[m_controller];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        switch( state )
        {
            case AbilityState.Startup:
                state = AbilityState.Active;
                break;
            case AbilityState.Active:
                if( !startedUpdating )
                {
                    characterMovement.Moveable = false;
                    startedUpdating = true;
					framesCharging = 0;
                }
				else
				{
					++framesCharging;
				}
                if ( framesCharging % framesToTick == 0 )
                {
                    characterAttributes.ModifyResource( 1 );
                }
                if( playerInput.PrimaryAbility.IsWithin(0.0f, InputManager.GeneralEpsilon ))
                {
                    characterMovement.Moveable = true;
                    characterMovement.Rotatable = true;
                    startedUpdating = false;
					framesCharging = 0;
                    AbilityEnd();
                }
                break;
            case AbilityState.Cooldown:
                break;
            default:
                break;
        }
	}
}
