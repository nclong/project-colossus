using UnityEngine;
using System.Collections;

public class PugilistShield : MonoBehaviour {
    [Range( 0.0f, 3.0f )]
    public int m_controller;
    public Button m_button;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;

    private float activeTimer;
    private AbilityTimer timer;
    public AbilityState state { get; set; }
    private CharacterStateController stateController;
    private CharacterMovement characterMovement;
    private PlayerInput playerInput;
    private int button;

	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( m_startupTime, m_activeTime, m_cooldownTime );
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        state = AbilityState.Inactive;
        button = (int)m_button;
        playerInput = InputManager.Players[m_controller];
	}
	
	// Update is called once per frame
	void Update () {
        timer.Update();
        state = timer.State;
        switch( state )
        {
            case AbilityState.Startup:
                break;
            case AbilityState.Active:
                activeTimer += Time.deltaTime;
                break;
            case AbilityState.Cooldown:
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
        activeTimer = 0.0f;
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
