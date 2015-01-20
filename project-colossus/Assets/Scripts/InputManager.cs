using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public static PlayerInput[] Players = new PlayerInput[4];
    public static float GeneralEpsilon = 0.01f;
	public bool[] A1 = new bool[4];
	public bool[] A2 = new bool[4];
	public bool[] A3 = new bool[4];
	public bool[] A4 = new bool[4];
	public float[] leftTrigger = new float[4];
	public float[] rightTrigger = new float[4];
	public string[] A1str = new string[4];
	public string[] A2str = new string[4];
	public string[] A3str = new string[4];
	public string[] A4str = new string[4];
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
			A1str[i] = "AbilityA" + i.ToString();
			Players[i].Abilities[0] = A1[i] = Input.GetButton( A1str[i] );
			A2str[i] = "AbilityX" + i.ToString();
			Players[i].Abilities[1] = A2[i] = Input.GetButton( A2str[i] );
			A3str[i] = "AbilityB" + i.ToString();
			Players[i].Abilities[2] = A3[i] = Input.GetButton( A3str[i] );
			A4str[i] = "AbilityY" + i.ToString();
			Players[i].Abilities[3] = A4[i] = Input.GetButton( A4str[i] );
			Players[i].PrimaryAbility = rightTrigger[i] =  Input.GetAxis( "Primary" + i.ToString() );
            Players[i].SecondaryAbility = leftTrigger[i] = Input.GetAxis( "Secondary" + i.ToString() );
        }

        string[] s = Input.GetJoystickNames();

	}
}
