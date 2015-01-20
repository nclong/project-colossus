using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class CharacterMovement : MonoBehaviour {
    public string m_characterName;

    public float acceleration;
    public float maxSpeed;
    public float movementDecay;
    public float velocityZeroThreshold;
    public float m_rotationSpeed;

    public int m_controller;

    public bool Moveable = false;
    public bool Rotatable = false;

    public SpriteRotationState RotationState { get; private set; }
    private float facingAngle;
    private PlayerInput playerInput;
    private CharacterStateController stateController;
    private float actualMaxSpeed;
    private Vector2 leftStickVector;
    private Vector2 rightStickVector;

	// Use this for initialization
	void Start () {
        playerInput = InputManager.Players[m_controller];
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
	}

    void FixedUpdate()
    {
        if( Moveable )
        {
           if( playerInput.LeftJoystickIsNull )
           {
               if( rigidbody2D.velocity != Vector2.zero )
               {
                   float decayScale = Mathf.Pow( rigidbody2D.velocity.magnitude, movementDecay );
                   rigidbody2D.velocity = rigidbody2D.velocity / rigidbody2D.velocity.magnitude * decayScale;
                   if( rigidbody2D.velocity.magnitude <= velocityZeroThreshold )
                   {
                       rigidbody2D.velocity = Vector2.zero;
                   } 
               }
           }
           else
           {
               leftStickVector = new Vector2( playerInput.LeftJoystickX, playerInput.LeftJoystickY );
               actualMaxSpeed = maxSpeed * leftStickVector.magnitude;
               rigidbody2D.velocity += leftStickVector * acceleration;
               if( rigidbody2D.velocity.magnitude > actualMaxSpeed )
               {
                   rigidbody2D.velocity = rigidbody2D.velocity / rigidbody2D.velocity.magnitude * actualMaxSpeed;
               }
           }
        }

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
