using UnityEngine;
using System.Collections;

public class ThaumaturgeDarkbolt : MonoBehaviour {

    public AbilityState state { get; private set; }
    public GameObject DarkBoltProjectile;
    public float startup;
    public float active;
    public float cooldown;
    public int cost;

    private AbilityTimer timer;
    private ThaumaturgeDark darkManager;
    private CharacterAttributes characterAttributes;
    private CharacterMovement characterMovement;
    private bool projectileLaunched = false;
    private AngleInput angle;
    private RaycastHit hit;
	// Use this for initialization
	void Start () {
        darkManager = GetComponent<ThaumaturgeDark>();
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();

	}
	
	// Update is called once per frame
	void Update () {
        timer.Update();
        state = timer.State;

        switch( state )
        {
            case AbilityState.Startup:
                break;
            case AbilityState.Active:

                break;
            case AbilityState.Cooldown:
                break;
        }
	}

    public void AbilityStart()
    {
        timer.Start();
        characterAttributes.ModifyResource( -cost );

    }
    public void AbilityEnd()
    {
        darkManager.AbilityEnd();
    }
}
