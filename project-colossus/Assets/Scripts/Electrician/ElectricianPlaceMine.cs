using UnityEngine;
using System.Collections;

public class ElectricianPlaceMine : MonoBehaviour {
    public int m_controller;
    public Button m_button;
    public float m_startupTime;
    public float m_activeTime;
    public float m_cooldownTime;

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
        state = AbilityState.Inactive;
        button = (int)m_button;
        playerInput = InputManager.Players[m_controller];
        
        current_mine = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1) && current_mine < max_mine)
        {
            Vector3 pos = new Vector3(player.transform.position.x + 10, player.transform.position.y, player.transform.position.z);
            GameObject newMine  = (GameObject)Instantiate(mine, pos, Quaternion.identity);
            ((ElectricianMine)newMine.GetComponent<ElectricianMine>()).player = transform.gameObject;

            current_mine++;
            Debug.Log("Placed a Mine");
        }
	}
}
