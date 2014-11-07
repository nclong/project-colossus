using UnityEngine;
using System.Collections;

public class PugilistJump : MonoBehaviour, IAbility {

    [Range(1.0f, 4.0f)]
    public int m_controller;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;
    public float m_jumpDistance;


    private float activeTimer;
    private AbilityTimer timer;
    public AbilityState state { get; set; }
    private Vector3 target;
    private Vector3 start;
    private Vector3 startScale;
    private Vector3 targetScale;
    private CharacterMovement characterMovement;
	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( m_startupTime, m_activeTime, m_cooldownTime );
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        startScale = transform.localScale;
        state = AbilityState.Inactive;
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

    public void AbilityStart()
    {
        AngleInput angle = characterMovement.GetRotationInput();
        start = transform.position;
        target = transform.position + (new Vector3( angle.Cos, 0f, angle.Sin)).normalized * m_jumpDistance;
        startScale = transform.localScale;
        targetScale = transform.localScale * 1.5f;
        activeTimer = 0.0f;
        timer.Start();
    }

    public void AbilityEnd()
    {
        Debug.Log( state.ToString() );
        transform.localScale = startScale;
        if( !characterMovement.GetAbilityInput( m_controller ) )
        {            
            characterMovement.EndAbilities();
            state = AbilityState.Inactive; 
        }
    }
}
