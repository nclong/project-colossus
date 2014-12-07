using UnityEngine;
using System.Collections;

public class PugilistAttack : MonoBehaviour, IAbility {

    [Range(0.0f, 3.0f)]
    public int m_controller;
    public Button m_button;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;
    public int damage;
    public int aggroGenerated;
    public float radius;
    public float offset;
    public GameObject parentAttackObject;

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
            	//Some animation stuff will go here eventually
                break;
            case AbilityState.Active:
            	/*
            	Make Sure parent attack object is correct size and adjust
            	Make sure parent attack object is the correct locationm and ajust
				Set parent attack object to active

				Some Unity Functions to look up:
				MonoBehaviour.SetActive(bool)
				transform.localPosition

            	*/
                break;
            case AbilityState.Cooldown:

            	/*
            	Set parent attack object to inactive
            	*/
            	//Some animation stuff will also go here eventaully
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
