using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static PlayerInput[] Players = new PlayerInput[4];
    public static float GeneralEpsilon = 0.01f;
	// Use this for initialization
	void Start () {
        for( int i = 0; i < Players.Length; ++i )
        {
            Players[i] = new PlayerInput();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //for( int i = 0; i < Players.Length; ++i )
	    for( int i = 0; i < 2; ++i )
        {
            Players[i].LeftJoystickX = Input.GetAxis( "LeftJoystickX" + i.ToString() );
            Players[i].LeftJoystickY = Input.GetAxis( "LeftJoystickY" + i.ToString() );
            Players[i].RightJoystickX = Input.GetAxis( "RightJoystickX" + i.ToString() );
            Players[i].RightJoystickY = Input.GetAxis( "RightJoystickY" + i.ToString() );
			Players[i].Abilities[0] = Input.GetButton( "AbilityA" + i.ToString() );
			Players[i].Abilities[1] = Input.GetButton( "AbilityX" + i.ToString() );
			Players[i].Abilities[2] = Input.GetButton( "AbilityB" + i.ToString() );
			Players[i].Abilities[3] = Input.GetButton( "AbilityY" + i.ToString() );
			Players[i].PrimaryAbility = Input.GetAxis( "Primary" + i.ToString() );
            Players[i].SecondaryAbility = Input.GetAxis( "Secondary" + i.ToString() );
        }
	}
}
