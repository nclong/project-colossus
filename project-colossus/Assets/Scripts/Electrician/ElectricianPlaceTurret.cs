using UnityEngine;
using System.Collections;

public class ElectricianPlaceTurret : MonoBehaviour {


    public GameObject turret;

    public int cost;

    public int m_controller;
    public int m_button;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;

    private CharacterMovement char_movement;
    private CharacterAttributes char_attributes;
    private AngleInput angle;
    private float activeTimer;
    private AbilityTimer timer;

    public AbilityState state { get; set; }
    private CharacterStateController stateController;
    private PlayerInput playerInput;
    private int button;

	// Use this for initialization
	void Start () {
        timer = new AbilityTimer((int)m_startupTime, (int)m_activeTime, (int)m_cooldownTime);
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        char_attributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        char_movement = (CharacterMovement)GetComponent<CharacterMovement>();
        button = m_button;

        state = AbilityState.Inactive;
        playerInput = InputManager.Players[m_controller];

	}

    // Used in place of Update()
    void FixedUpdate()
    {
        state = timer.State;
        if (state != AbilityState.Inactive)
        {
            AbilityEnd();
        }
    }

    public void AbilityStart()
    {
        angle = char_movement.GetRotationInput();
        timer.Start();
        char_attributes.ModifyResource(-cost);

        Vector3 pos = transform.position + new Vector3(angle.Cos, angle.Sin, 0f) * 10;
        GameObject placedTurret = (GameObject)Instantiate(turret, pos, Quaternion.identity);

        AbilityEnd();
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
