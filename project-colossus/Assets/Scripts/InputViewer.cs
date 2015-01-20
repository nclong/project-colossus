using UnityEngine;
using System.Collections;

public class InputViewer : MonoBehaviour {

    public float leftX;
    public float leftY;
    public float rightX;
    public float rightY;
    public float triggers;
    public bool ability1;
    public bool ability2;
    public bool ability3;
    public bool ability4;

    public int playerNum = 0;
    private PlayerInput playerInput;
    
	// Use this for initialization
	void Start () {
        playerInput = InputManager.Players[playerNum];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        leftX = playerInput.LeftJoystickX;
        leftY = playerInput.LeftJoystickY;
        rightX = playerInput.RightJoystickX;
        rightY = playerInput.RightJoystickY;
        triggers = playerInput.PrimaryAbility;
        ability1 = playerInput.Abilities[0];
        ability2 = playerInput.Abilities[1];
        ability3 = playerInput.Abilities[2];
        ability4 = playerInput.Abilities[3];
	}
}
