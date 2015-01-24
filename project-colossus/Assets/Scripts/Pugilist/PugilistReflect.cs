using UnityEngine;
using System;
using System.Collections;

public class PugilistReflect : MonoBehaviour, IAbility
{

    public int m_controller;
    public int m_startupTime;
    public int m_activeTime;
    public int m_cooldownTime;

    public int damage;              // Not sure about this
    public int aggroGenerated;
    public int cost;                //Mana Pool

    private AbilityTimer timer;
    public AbilityState state { get; set; }

    private CharacterStateController stateController;
    private CharacterMovement characterMovement;
    private CharacterAttributes characterAttributes;
    private PlayerInput playerInput;
    public int button;
    private SpriteRenderer sr;

    /* Other variables to consider?
     * speed of projectile
     */



    // Use this for initialization
    void Start()
    {
        timer = new AbilityTimer( m_startupTime, m_activeTime, m_cooldownTime );
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        state = AbilityState.Inactive;
        playerInput = InputManager.Players[m_controller];
        sr = (SpriteRenderer)renderer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        timer.Update();
        state = timer.State;
        switch( state )
        {
            case AbilityState.Startup:
                characterAttributes.ModifyResource( -cost );
                // Animation startup for 0.3 sec and consume 10 mana

                //	BREAKDOWN:
                //	Show animation for 0.3 seconds
                // 	Consume 10 mana from mana pool

                // Consider all projectiles 90 degrees in front of player

                //	BREAKDOWN:
                //	If projectile is 90 degrees in front of player, go to 

                break;
            case AbilityState.Active:
                sr.color = new Color( 255f, 0f, 237f );
                // Ability will be up for 0.25 sec

                //	BREAKDOWN:
                //	While time is between 0 to 0.25 sec


                // If projectile contacts pugilist, then reverse back with 2x speed

                //	BREAKDOWN:
                //	First if projectile contacts
                // 	Then reverse direction
                // 	Change speed to 2x

                break;
            case AbilityState.Cooldown:
                sr.color = new Color( 255f, 255f, 255f );
                //Ability cooldown is 0.25 sec

                // BREAKDOWN:
                // Show animation returning to original state

                break;
            default:
                break;
        }
        if( timer.IsOver )
        {
            AbilityEnd();
        }

    }

    public int GetButton()
    {
        return button;
    }

    public void AbilityStart()
    {
        AngleInput angle = characterMovement.GetRotationInput();

        timer.Start();
    }

    public void AbilityEnd()
    {

        if( !playerInput.Abilities[button] )
        {
            stateController.EndAbilities();
            state = AbilityState.Inactive;
        }
    }
}
