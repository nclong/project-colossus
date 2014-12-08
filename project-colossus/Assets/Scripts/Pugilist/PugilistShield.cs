using UnityEngine;
using System.Collections;

public class PugilistShield : MonoBehaviour, IAbility {
    [Range( 0.0f, 3.0f )]
    public int m_controller;
    public Button m_button;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;
    public int cost;
    public GameObject shieldObject;

    private float activeTimer;
    private AbilityTimer timer;
    public AbilityState state { get; set; }
    private CharacterStateController stateController;
    private CharacterMovement characterMovement;
    private CharacterAttributes characterAttributes;
    private PlayerInput playerInput;
    public int button;
    private int integerSeconds = 0;
    private float tickLength;
    private float tickTimer;

	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( m_startupTime, m_activeTime, m_cooldownTime );
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        state = AbilityState.Inactive;
        //button = (int)m_button;
        playerInput = InputManager.Players[m_controller];

        tickLength = 1f / (float)cost;
	}
	
	// Update is called once per frame
	void Update () {

        switch( state )
        {
            case AbilityState.Startup:
                break;
            case AbilityState.Active:
                activeTimer += Time.deltaTime;
                tickTimer += Time.deltaTime;
                if ( tickTimer >= tickLength )
                {
                    characterAttributes.ModifyResource( -1 );
                    tickTimer = tickTimer - tickLength;
                }
                if( !playerInput.Abilities[button] || characterAttributes.CurrentResource <= 0)
                {
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
    }

    public void AbilityEnd()
    {
        if( !playerInput.Abilities[button] )
        {

            stateController.EndAbilities();
            state = AbilityState.Inactive;
            characterMovement.Moveable = true;
            shieldObject.SetActive( false );
        }
    }
}
