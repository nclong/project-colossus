using UnityEngine;
using System.Collections;

public class ElectricianPlaceMine : MonoBehaviour, IAbility {
    public int m_controller;
    public int m_button;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;

    public int cost;

    private CharacterMovement char_movement;
    private CharacterAttributes char_attributes;
    private AngleInput angle;
    private float activeTimer;
    private AbilityTimer timer;

    public AbilityState state { get; set; }
    private CharacterStateController stateController;
    private PlayerInput playerInput;
    private int button;
    
    public GameObject player;
    public GameObject mine;

    public int max_mine;
    public int current_mine;

	// Use this for initialization
	void Start () {
        timer = new AbilityTimer(m_startupTime, m_activeTime, m_cooldownTime);
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        char_attributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        char_movement = (CharacterMovement)GetComponent<CharacterMovement>();

        state = AbilityState.Inactive;
        button = m_button;
        playerInput = InputManager.Players[m_controller];

        
        current_mine = 0;
	}
	
	// Update is called once per frame
	void Update () {
        state = timer.State;
        if( state != AbilityState.Inactive )
        {
            AbilityEnd();
        }
	}
    public void AbilityStart()
    {
        angle = char_movement.GetRotationInput();
        timer.Start();
        char_attributes.ModifyResource(-cost);

        Vector3 pos = transform.position + new Vector3( angle.Cos, 0f, angle.Sin).PerspectiveAdjusted() * 10;
        GameObject newMine = (GameObject)Instantiate(mine, pos, Quaternion.identity);
        ((ElectricianMine)newMine.GetComponent<ElectricianMine>()).player = transform.gameObject;

        current_mine++;
        AbilityEnd();
        Debug.Log("Placed a Mine");
    }

    public void AbilityEnd()
    {
        if (!playerInput.Abilities[button])
        {
            stateController.EndAbilities();
            state = AbilityState.Inactive;
        }
    }

    public int GetButton()
    {
        return button;
    }
}
