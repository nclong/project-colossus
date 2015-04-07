using UnityEngine;
using System.Collections;

public class ElectricianCharge : MonoBehaviour {

	public int m_controller;
	public int m_startupTime;
	public int m_activeTime;
	public int m_cooldownTime;

	public int energyGained;

	private bool absorbedEnergy;
	
	private AbilityTimer timer;
	public AbilityState state { get; set; }
	
	private CharacterStateController stateController;
	private CharacterMovement characterMovement;
	private CharacterAttributes characterAttributes;
	private PlayerInput playerInput;
	public int button = 3;
	private SpriteRenderer sr;
	
	// Use this for initialization
	void Start()
	{
		timer = new AbilityTimer( m_startupTime, m_activeTime, m_cooldownTime );
		stateController = (CharacterStateController)GetComponent<CharacterStateController>();
		characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
		characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
		state = AbilityState.Inactive;
		playerInput = InputManager.Players[m_controller];
		sr = (SpriteRenderer)GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer.Update();
		state = timer.State;
		switch (state) {
		case AbilityState.Startup:
			break;

		case AbilityState.Active:
			if (!absorbedEnergy) {
				ElectricianTowerResourceCounter tower;
				tower = (ElectricianTowerResourceCounter)GetComponent<ElectricianTowerResourceCounter> ();
				if (tower.resource >= energyGained) {
					tower.resource -= energyGained;
					characterAttributes.ModifyResource (energyGained);

					absorbedEnergy = true;
				}
			}
			break;

		case AbilityState.Cooldown:		
			break;

		default:
			break;
		}
		if( timer.IsOver )
		{
			AbilityEnd();
		}
	}
	
	public int GetButton()
	{
		return button;
	}

	public void AbilityStart() {
		timer.Start ();
	}

	public void AbilityEnd() {
		if (!playerInput.Abilities [button]) {
			stateController.EndAbilities();
			state = AbilityState.Inactive;
		}
	}
}
