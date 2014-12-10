using UnityEngine;
using System.Collections;

public class PugilistAttack : PrimaryAbility {

    [Range(0.0f, 3.0f)]
    public int m_controller;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;
    public int damage;
    public int aggroGenerated;
    public float radius;
    public float offset;
    public GameObject parentAttackObject;

    private CharacterStateController stateController;
    private CharacterMovement characterMovement;
    private PlayerInput playerInput;
    private AngleInput angle;

    private bool startedUpdated = false;
    private bool activeUpdated = false;
	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( m_startupTime, m_activeTime, m_cooldownTime );
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        state = AbilityState.Inactive;
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
                if( !startedUpdated )
                {
                    angle = characterMovement.GetRotationInput();
                    startedUpdated = true;
                    characterMovement.Moveable = false;
                    characterMovement.Rotatable = false;
                    parentAttackObject.transform.localPosition = new Vector3( angle.Cos, parentAttackObject.transform.localPosition.y, angle.Sin ).PerspectiveAdjusted() * offset;
                    parentAttackObject.transform.localScale = new Vector3( radius, radius, radius );
                }
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
                if( !activeUpdated )
                {
                    parentAttackObject.SetActive( true );
                    activeUpdated = true;
                }
                break;
            case AbilityState.Cooldown:
                parentAttackObject.SetActive( false );
            	/*
            	Set parent attack object to inactive
            	*/
            	//Some animation stuff will also go here eventaully
                break;
            case AbilityState.Null:
                state = AbilityState.Inactive;
                break;
            default:
                break;
        }
        if( timer.IsOver )
        {
            if( playerInput.PrimaryAbility.IsWithin( 0.0f, InputManager.GeneralEpsilon ) )
            {
                startedUpdated = false;
                activeUpdated = false;
                characterMovement.Moveable = true;
                characterMovement.Rotatable = true;
                stateController.EndAbilities();
                stateController.RemoveState( CharacterState.Primary );
                AbilityEnd();
            }
        }

	}

}
