using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class CharacterMovement : MonoBehaviour {
    public string m_characterName;

    [Range(0f, 1000.0f)]
    public float m_movementSpeed;

    [Range( 0f, 1.0f )]
    public float m_rotationSpeed;

    [Range( 0f, 1.0f )]
    public float m_inputEpsilon;

    [Range( 0f, 3.0f )]
    public int m_controller;

    public IAbility[] m_abilities;
    
    private bool rightJoystickIsNull
    {
        get { return rightJoystickY.IsWithin( 0f, m_inputEpsilon ) && rightJoystickX.IsWithin( 0f, m_inputEpsilon ); }
    }

    private bool leftJoystickIsNull
    {
        get { return leftJoystickY.IsWithin( 0f, m_inputEpsilon ) && leftJoystickX.IsWithin( 0f, m_inputEpsilon ); }
    }

    public SpriteRotationState RotationState { get; private set; }
    public uint State { get; private set; }

    private float leftJoystickX;
    private float leftJoystickY;
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
    private IAbility jump;

    //NOT A FINAL SOLUTION AT ALL
    private bool jumpStarted = false;
    private bool jumpFinished = true;

	// Use this for initialization
	void Start () {
        m_characterName = m_characterName.ToLower();
        jump = (IAbility)GetComponent<PugilistJump>();

        leftJoystickXStr = "LeftJoystickX" + m_controller.ToString();
        leftJoystickYStr = "LeftJoystickY" + m_controller.ToString();
        rightJoystickXStr = "RightJoystickX" + m_controller.ToString();
        rightJoystickYStr = "RightJoystickY" + m_controller.ToString();
        ability1Str = "AbilityA" + m_controller.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        
        //Collect Inputs
        leftJoystickX = Input.GetAxis( leftJoystickXStr );
        leftJoystickY = Input.GetAxis( leftJoystickYStr );
        rightJoystickX = Input.GetAxis( rightJoystickXStr );
        rightJoystickY = Input.GetAxis( rightJoystickYStr );
        bool jumpInput = Input.GetButton( ability1Str );

        //Set states from inputs
        if( !leftJoystickIsNull || !rightJoystickIsNull )
        {
            State |= (uint)CharacterState.Moving;
        }
        if( jumpInput && ( jump.state == AbilityState.Inactive || jump.state == AbilityState.Null) )
        {
            State |= (uint)CharacterState.Ability1;
        }

        //This may be it's own class eventually
        //Right now it uses the ability order of operations
        //To process effects
        if( ( State & (uint)CharacterState.Ability1 ) == (uint)CharacterState.Ability1 )
        {
            if( jump.state == AbilityState.Inactive || jump.state == AbilityState.Null )
            {
                jump.AbilityStart(); 
            }
        }
        else if( ( State & (uint)CharacterState.Ability2 ) == (uint)CharacterState.Ability2 )
        {

        }
        else if( ( State & (uint)CharacterState.Ability3 ) == (uint)CharacterState.Ability3 )
        {

        }
        else if( ( State & (uint)CharacterState.Ability4 ) == (uint)CharacterState.Ability4 )
        {

        }
        else if( ( State & (uint)CharacterState.Primary ) == (uint)CharacterState.Primary )
        {

        }
        else if( ( State & (uint)CharacterState.Secondary ) == (uint)CharacterState.Secondary )
        {

        }
        else if( ( State & (uint)CharacterState.Moving ) == (uint)CharacterState.Moving )
        {
            Vector3 movementInput = new Vector3( leftJoystickX, 0f, leftJoystickY );
            transform.position = Vector3.Lerp( transform.position, ( transform.position + movementInput * m_movementSpeed ), Time.deltaTime );
            if( !rightJoystickIsNull )
            {
                facingAngle = Mathf.Atan2( rightJoystickY, rightJoystickX ) * Mathf.Rad2Deg;
            }
            if( facingAngle >= 0 && facingAngle < 180 )
            {
                RotationState = SpriteRotationState.Up;
            }
            else
            {
                RotationState = SpriteRotationState.Down;
            }
        }
	}

    public bool GetAbilityInput( int index )
    {
        switch( index )
        {
            case 1:
                return Input.GetButton( ability1Str );
            case 2:
                return Input.GetButton( ability2Str );
            case 3:
                return Input.GetButton( ability3Str );
            case 4:
                return Input.GetButton( ability4Str );
            default:
                return false;
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
