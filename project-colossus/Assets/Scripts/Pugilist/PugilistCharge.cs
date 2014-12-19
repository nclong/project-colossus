using UnityEngine;
using System.Collections;

public class PugilistCharge : SecondaryAbility {

    [Range(0.0f, 3.0f)]
    public int m_controller;
    public int chargeAmount;

    private CharacterMovement characterMovement;
    private CharacterAttributes characterAttributes;
    private PlayerInput playerInput;
    private float tickLength;
    private float tickTimer;
    bool startedUpdating = false;

	// Use this for initialization
	void Start () {
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        state = AbilityState.Inactive;
        playerInput = InputManager.Players[m_controller];
        tickLength = 1 / (float)chargeAmount;
	}
	
	// Update is called once per frame
	void Update () {
        switch( state )
        {
            case AbilityState.Startup:
                state = AbilityState.Active;
                break;
            case AbilityState.Active:
                if( !startedUpdating )
                {
                    tickTimer = 0f;
                    characterMovement.Moveable = false;
                    startedUpdating = true;
                }

                tickTimer += Time.deltaTime;
                if ( tickTimer >= tickLength )
                {
                    characterAttributes.ModifyResource( 1 );
                    tickTimer = tickTimer - tickLength;
                }
                if( playerInput.PrimaryAbility.IsWithin(0.0f, InputManager.GeneralEpsilon ))
                {
                    characterMovement.Moveable = true;
                    characterMovement.Rotatable = true;
                    startedUpdating = false;
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
