using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class CharacterMovement : MonoBehaviour {
    [Range(0f, 1000.0f)]
    public float m_movementSpeed;

    [Range( 0f, 1.0f )]
    public float m_rotationSpeed;

    [Range( 0f, 1.0f )]
    public float m_inputEpsilon;

    [Range( 0f, 4.0f )]
    public int m_controller;

    public IAbility[] m_abilities;
    
    private bool rightJoystickIsNull
    {
        get { return rightJoystickY.IsWithin( 0f, m_inputEpsilon ) && rightJoystickX.IsWithin( 0f, m_inputEpsilon ); }
    }

    public SpriteRotationState RotationState { get; private set; }
    public uint State { get; private set; }

    private float rightJoystickX;
    private float rightJoystickY;
    private float facingAngle;

    private string leftJoystickXStr;
    private string leftJoystickYStr;
    private string rightJoystickXStr;
    private string rightJoystickYStr;
    private string ability1Str;
    private string ability2Str;
    private string ability3Str;
    private string ability4Str;
    private PugilistJump jump;

    //NOT A FINAL SOLUTION AT ALL
    private bool jumpStarted = false;
    private bool jumpFinished = true;

	// Use this for initialization
	void Start () {
        jump = (PugilistJump)GetComponent<PugilistJump>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movementInput = new Vector3( Input.GetAxis( "LeftJoystickX" ), 0f, Input.GetAxis( "LeftJoystickY" ) );
        transform.position = Vector3.Lerp( transform.position, ( transform.position + movementInput * m_movementSpeed ), Time.deltaTime );
        rightJoystickX = Input.GetAxis( "RightJoystickX" );
        rightJoystickY = Input.GetAxis( "RightJoystickY" );
        if( !rightJoystickIsNull )
        {
            facingAngle = Mathf.Atan2( rightJoystickY, rightJoystickX ) * Mathf.Rad2Deg;
        }
        if( facingAngle >= 0 && facingAngle < 180)
        {
            RotationState = SpriteRotationState.Up;
        }
        else
        {
            RotationState = SpriteRotationState.Down;
        }

        //Not How This is Going To Work
        bool jumpInput = Input.GetButton("AbilityA0" );
        if( jumpInput && jump.state == AbilityState.Inactive && jumpFinished )
        {
            jumpStarted = true;
            jumpFinished = false;
            State |= (uint)CharacterState.Ability1;
            jump.AbilityStart();
            Debug.Log( "Jump" );
        }
        else if ( !jumpInput && !jumpStarted )
        {
            jumpFinished = true;
            removeState( (uint)CharacterState.Ability1 );
        }
	}

    public AngleInput GetRotationInput()
    {
        return new AngleInput()
        {
            Angle = facingAngle,
            X = rightJoystickX,
            Y = rightJoystickY,
            FromInput = rightJoystickIsNull,
            Cos = Mathf.Cos( facingAngle * Mathf.Deg2Rad ),
            Sin = Mathf.Sin( facingAngle * Mathf.Deg2Rad )
        };
    }

    public void EndAbilities()
    {
        jumpStarted = false;
        if( !Input.GetButton("AbilityA0"))
            removeState( (uint)CharacterState.Ability1 );
        removeState( (uint)CharacterState.Ability2 );
        removeState( (uint)CharacterState.Ability3 );
        removeState( (uint)CharacterState.Ability4 );

    }

    private void removeState(uint toRemove)
    {
        if( (State & toRemove) > 0 )
        {
            State ^= toRemove;
        }
    }
}
