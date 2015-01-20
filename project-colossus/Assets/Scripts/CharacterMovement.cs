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
    public float rotationMaxSpeed;
    public float rotationAcceleration;
    public float rotationZeroThreshold;

    public int m_controller;

    public bool Moveable = false;
    public bool Rotatable = false;

    public SpriteRotationState RotationState { get; private set; }
    private float facingAngle;
    private PlayerInput playerInput;
    private CharacterStateController stateController;
    private float actualMaxSpeed;
    private float actualMaxRotationSpeed;
    private float rotationSpeed;
    private bool rotateClockwise;

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
               Vector2 leftStickVector = new Vector2( playerInput.LeftJoystickX, playerInput.LeftJoystickY );
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
            float stickAngle = Mathf.Atan2( playerInput.RightJoystickY, playerInput.RightJoystickX ) * Mathf.Rad2Deg;
            actualMaxRotationSpeed = rotationMaxSpeed * new Vector2( playerInput.RightJoystickX, playerInput.RightJoystickY ).magnitude;
            rotationSpeed += rotationAcceleration * new Vector2( playerInput.RightJoystickX, playerInput.RightJoystickY ).magnitude;
            if( rotationSpeed > actualMaxRotationSpeed )
            {
                rotationSpeed = actualMaxRotationSpeed;
            }

            if( stickAngle == 0f )
            {
                stickAngle = 360f;
            }

            if( facingAngle == 0f )
            {
                facingAngle = 360f;
            }

            if( stickAngle > facingAngle )
            {
                if( stickAngle - facingAngle >= 180 )
                {
                    rotateClockwise = true;
                }
                else
                {
                    rotateClockwise = false;
                }
            }
            else
            {
                if( facingAngle - stickAngle >= 180 )
                {
                    rotateClockwise = false;
                }
                else
                {
                    rotateClockwise = true;
                }
            }

            if( facingAngle > 270 && ((360 - facingAngle) + stickAngle) < rotationSpeed )
            {
                facingAngle = stickAngle;
            }
            else if( stickAngle > 270 && ((360 - stickAngle) + facingAngle) < rotationSpeed )
            {
                facingAngle = stickAngle;
            }
            else
            {
                if( rotateClockwise )
                {
                    float targetAngle = facingAngle - rotationSpeed;
                    if( rotationSpeed < 0 )
                    {
                        rotationSpeed += 360f;
                    }
                    if( targetAngle < stickAngle)
                    {
                        facingAngle = stickAngle;
                    }
                    else
                    {
                        facingAngle = targetAngle;
                    }
                }
                else
                {
                    float targetAngle = facingAngle + rotationSpeed;
                    if( rotationSpeed > 360 )
                    {
                        rotationSpeed -= 360f;
                    }
                    if( targetAngle > stickAngle )
                    {
                        facingAngle = stickAngle;
                    }
                    else
                    {
                        facingAngle = targetAngle;
                    }
                }
            }

            if( Mathf.Abs(stickAngle - facingAngle) <= rotationZeroThreshold )
            {
                facingAngle = stickAngle;
            }

        }
        else
        {
            actualMaxRotationSpeed = 0f;
            rotationSpeed = 0f;
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
