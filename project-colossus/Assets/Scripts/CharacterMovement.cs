using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class CharacterMovement : MonoBehaviour {
    public string m_characterName;

    [Range(0f, 1000.0f)]
    public float m_movementSpeed;

    [Range( 0f, 1.0f )]
    public float m_rotationSpeed;

    [Range( 0f, 3.0f )]
    public int m_controller;

    public bool Moveable = false;
    public bool Rotatable = false;

    public SpriteRotationState RotationState { get; private set; }
    private float facingAngle;
    private PlayerInput playerInput;
    private CharacterStateController stateController;

	// Use this for initialization
	void Start () {
        playerInput = InputManager.Players[m_controller];
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
	}

    void FixedUpdate()
    {
        if( Moveable )
        {
            rigidbody.velocity = new Vector3( playerInput.LeftJoystickX, 0f, playerInput.LeftJoystickY ).PerspectiveAdjusted() * m_movementSpeed; 
        }
    }
	
	// Update is called once per frame
	void Update () {

        if( Rotatable && !playerInput.RightJoystickIsNull )
        {
            facingAngle = Mathf.Atan2( playerInput.RightJoystickY, playerInput.RightJoystickX ) * Mathf.Rad2Deg;
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

    public AngleInput GetRotationInput()
    {
        return new AngleInput()
        {
            Angle = facingAngle,
            X = playerInput.RightJoystickX,
            Y = playerInput.RightJoystickY,
            FromInput = !playerInput.RightJoystickIsNull,
            Cos = Mathf.Cos( facingAngle * Mathf.Deg2Rad ),
            Sin = Mathf.Sin( facingAngle * Mathf.Deg2Rad )
        };
    }
}
