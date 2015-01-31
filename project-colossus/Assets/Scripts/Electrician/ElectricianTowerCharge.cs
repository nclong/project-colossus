using UnityEngine;
using System.Collections;

public class TowerCharge : MonoBehaviour, IAbility {
	public bool chargeable {get; set;}
	[Range(0.0f, 3.0f)]
	public int m_controller;
	public int framesToTick;
	public int button;

	public AbilityState state { get; set; }
	
	private CharacterMovement characterMovement;
	private CharacterAttributes characterAttributes;
	private CharacterStateController stateController;
	private PlayerInput playerInput;
	private int framesCharging = 0;
	bool startedUpdating = false;
	
	// Use this for initialization
	void Start () {
		characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
		characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
		stateController = (CharacterStateController)GetComponent<CharacterStateController> ();
		state = AbilityState.Inactive;
		playerInput = InputManager.Players[m_controller];
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch (state) {
				case AbilityState.Startup:
						state = AbilityState.Active;
						break;
				case AbilityState.Active:
					if (chargeable) {
						if (!startedUpdating) {
								characterMovement.Moveable = false;
								startedUpdating = true;
								framesCharging = 0;
						} else {
								++framesCharging;
						}
						if (framesCharging % framesToTick == 0) {
								characterAttributes.ModifyResource (1);
						}
						if (playerInput.PrimaryAbility.IsWithin (0.0f, InputManager.GeneralEpsilon)) {
								characterMovement.Moveable = true;
								characterMovement.Rotatable = true;
								startedUpdating = false;
								framesCharging = 0;
								AbilityEnd ();
						}
					}
			break;
		case AbilityState.Cooldown:
			break;
		default:
			break;
		}
	}

	public int GetButton()
	{
		return button;
	}

	public void AbilityStart()
	{
		state = AbilityState.Startup; 
	}

	public void AbilityEnd()
	{
		state = AbilityState.Inactive;
		stateController.EndAbilities();
	}

}
