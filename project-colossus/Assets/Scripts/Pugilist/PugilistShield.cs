using UnityEngine;
using System.Collections;
using System;

public class PugilistShield : MonoBehaviour, IAbility {
    [Range( 0.0f, 3.0f )]
    public int m_controller;
    public int m_startupTime;
    public int m_activeTime;
    public int m_cooldownTime;
    public int cost;
    public GameObject shieldObject;

    private int activeTimer;
    private AbilityTimer timer;
    public AbilityState state { get; set; }
    private CharacterStateController stateController;
    private CharacterMovement characterMovement;
    private CharacterAttributes characterAttributes;
    private PlayerInput playerInput;
    public int button;
    private int tickLength;
    private int tickTimer;

	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( m_startupTime, m_activeTime, m_cooldownTime );
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        state = AbilityState.Inactive;
        playerInput = InputManager.Players[m_controller];

        tickLength = 1f / (float)cost;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        switch( state )
        {
            case AbilityState.Startup:
                break;
            case AbilityState.Active:
                collider.enabled = false;
				tickTimer++;
                if ( tickTimer % tickLength == 0 )
                {
                    characterAttributes.ModifyResource( -1 );
                }
                if( !playerInput.Abilities[button] || characterAttributes.CurrentResource <= 0)
                {
					tickTimer = 0;
                    AbilityEnd();
                }
                break;
            case AbilityState.Cooldown:
                break;
            default:
                break;
        }
	}

    public int GetButton()
    {
        return button;
    }

    public void AbilityStart()
    {
        tickTimer = 0f;
        state = AbilityState.Active;
        characterMovement.Moveable = false;
        shieldObject.SetActive( true );
        rigidbody.velocity = Vector3.zero;
    }

    public void AbilityEnd()
    {
        if( !playerInput.Abilities[button] )
        {
            collider.enabled = true;
            stateController.EndAbilities();
            state = AbilityState.Inactive;
            characterMovement.Moveable = true;
            shieldObject.SetActive( false );
        }
    }
}
