using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static PlayerInput[] Players = new PlayerInput[4];
	// Use this for initialization
	void Start () {
        for( int i = 0; i < Players.Length; ++i )
        {
            Players[i] = new PlayerInput();
        }
	}
	
	// Update is called once per frame
	void Update () {
        //for( int i = 0; i < Players.Length; ++i )
	    for( int i = 0; i < 1; ++i )
        {
            Players[0].LeftJoystickX = Input.GetAxis( "LeftJoystickX" + i.ToString() );
            Players[0].LeftJoystickY = Input.GetAxis( "LeftJoystickY" + i.ToString() );
            Players[0].RightJoystickX = Input.GetAxis( "RightJoystickX" + i.ToString() );
            Players[0].RightJoystickY = Input.GetAxis( "RightJoystickY" + i.ToString() );
            Players[0].Abilities[0] = Input.GetButton( "AbilityA" + i.ToString() );
            Players[0].Abilities[1] = Input.GetButton( "AbilityX" + i.ToString() );
            Players[0].Abilities[2] = Input.GetButton( "AbilityB" + i.ToString() );
            Players[0].Abilities[3] = Input.GetButton( "AbilityY" + i.ToString() );
            //Still Need to Add Trigger Abilities
        }
	}
}
