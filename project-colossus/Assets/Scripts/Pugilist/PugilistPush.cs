using UnityEngine;
using System.Collections;

public class PugilistPush : MonoBehaviour, IAbility
{

    public int m_controller;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;

    public float force;
    public float radius;
    public float offset;
    public GameObject pushObject;

    public int cost;

    private AbilityTimer timer;
    public AbilityState state { get; set; }

    private CharacterStateController stateController;
    private CharacterMovement characterMovement;
    private CharacterAttributes characterAttributes;
    private PlayerInput playerInput;
    public int button;

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        timer.Update();
        state = timer.State;
        switch( state )
        {
            case AbilityState.Startup:
                // Animation startup for 0.3 sec and consume 10 mana

                //	BREAKDOWN:
                //	Show animation for 0.3 seconds
                // 	Consume 10 mana from mana pool

                // Consider all projectiles 90 degrees in front of player

                //	BREAKDOWN:
                //	If projectile is 90 degrees in front of player, go to 

                break;
            case AbilityState.Active:
                pushObject.SetActive( true );
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
                pushObject.SetActive( false );
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
        timer.Start();
        characterAttributes.ModifyResource( -cost );
        state = AbilityState.Startup;
        AngleInput angle = characterMovement.GetRotationInput();
        angle = characterMovement.GetRotationInput();
        //characterMovement.Moveable = false;
        characterMovement.Rotatable = false;
        pushObject.transform.localPosition = new Vector3( angle.Cos, pushObject.transform.localPosition.y, angle.Sin ).PerspectiveAdjusted() * offset;
        pushObject.transform.localScale = new Vector3( radius, radius, radius );
    }

    public void AbilityEnd()
    {
        pushObject.SetActive( false );

        if( !playerInput.Abilities[button] )
        {
            stateController.EndAbilities();
            state = AbilityState.Inactive;
            characterMovement.Moveable = true;
            characterMovement.Rotatable = true;
        }
    }
}
