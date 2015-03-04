using UnityEngine;
using System.Collections;

public class ElectricianPlaceTower : MonoBehaviour, IAbility {
	
	
	public GameObject tower;
	
	public int cost;
	public int maxTowers;
	private int numTowers;
	
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

	void Awake()
	{
		//put the button assignment and state assignment in here.
		button = m_button;
		numTowers = 0;
		timer = new AbilityTimer((int)m_startupTime, (int)m_activeTime, (int)m_cooldownTime);
	}
	
	// Use this for initialization
	void Start () {
		//all of this is getting overridden somewhere i don't know where please help
		//timer = new AbilityTimer((int)m_startupTime, (int)m_activeTime, (int)m_cooldownTime);
		stateController = (CharacterStateController)GetComponent<CharacterStateController>();
		char_attributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
		char_movement = (CharacterMovement)GetComponent<CharacterMovement>();
		//for example, even though the button here is what we set in the script, its gets overridden
		//so that when its called in GetButton() it gives 0. I had to hard code GetButton to 1 so that
		//i could test placeTower but since everything else is being overwritten, it barely matters.
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
		if(char_attributes.CurrentResource >= cost && numTowers < maxTowers){
			timer.Start ();
			char_attributes.ModifyResource(-cost);

			//Vector3 pos = transform.position + new Vector3(angle.Cos, angle.Sin, 0f) * 10;
			Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 5f);
			GameObject placedTower = (GameObject)Instantiate(tower, pos, Quaternion.identity);

			numTowers++;

			AbilityEnd();
		}
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
		//something is changing button to 0
		Debug.Log ("button @ GetButton() = " + button);
		//return button;
		//im forcing it to be 1
		return button;
	}
}
