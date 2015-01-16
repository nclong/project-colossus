using UnityEngine;
using System.Collections;

public class PugilistJump : MonoBehaviour, IAbility {

    [Range(0.0f, 3.0f)]
    public int m_controller;
    public Button m_button;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;
    public float m_jumpDistance;
    public int cost;


    private float activeTimer;
    private AbilityTimer timer;
    public AbilityState state { get; set; }
    private Vector3 target;
    private Vector3 start;
    private Vector3 startScale;
    private Vector3 targetScale;
    private CharacterStateController stateController;
    private CharacterMovement characterMovement;
    private CharacterAttributes characterAttributes;
    private PlayerInput playerInput;
    private int button;
	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( m_startupTime, m_activeTime, m_cooldownTime );
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        startScale = transform.localScale;
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
                if( activeTimer < m_activeTime )
                {
                    transform.position = Vector3.Lerp( start, target, activeTimer / m_activeTime );
                    transform.localScale = Vector3.Lerp( startScale, targetScale, -Mathf.Abs( ( activeTimer - m_activeTime / 2 ) / ( m_activeTime / 2 ) ) + 1 ); 
                }
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
        AngleInput angle = characterMovement.GetRotationInput();
        start = transform.position;
        target = transform.position + (new Vector3( angle.Cos, angle.Sin, transform.position.z).normalized) * m_jumpDistance;
        startScale = transform.localScale;
        targetScale = transform.localScale * 1.5f;
        activeTimer = 0.0f;
        timer.Start();
        characterAttributes.ModifyResource( -cost );
        collider2D.isTrigger = true;
    }

    public void AbilityEnd()
    {
        collider2D.isTrigger = false;
        transform.localScale = startScale;
        if( !playerInput.Abilities[button] )
        {            
            stateController.EndAbilities();
            state = AbilityState.Inactive; 
        }
    }
}
