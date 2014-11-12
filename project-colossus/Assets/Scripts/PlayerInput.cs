using UnityEngine;
using System.Collections;

public class PlayerInput {
    public float LeftJoystickX;
    public float LeftJoystickY;
    public float RightJoystickX;
    public float RightJoystickY;
    public float PrimaryAbility;
    public float SecondaryAbility;
    public bool[] Abilities;
    public bool LeftJoystickIsNull
    {
        get
        {
            return LeftJoystickX.IsWithin( 0.0f, 0.025f ) && LeftJoystickY.IsWithin( 0.0f, 0.025f );
        }
    }

    public bool RightJoystickIsNull
    {
        get
        {
            return RightJoystickX.IsWithin( 0.0f, 0.025f ) && RightJoystickY.IsWithin( 0.0f, 0.025f );
        }
    }

    public PlayerInput()
    {
        Abilities = new bool[4];
    }
}
