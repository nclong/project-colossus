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
	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( startup, active, cooldown );
        darkManager = GetComponent<ThaumaturgeDark>();
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer.Update();
        state = timer.State;

        switch( state )
        {
            case AbilityState.Startup:
                break;
            case AbilityState.Active:
                if( !projectileLaunched )
                {
                    Projectile proj = (Projectile)((GameObject)Instantiate( DarkBoltProjectile, transform.position, transform.rotation )).GetComponent<Projectile>();
                    AngleInput angle = characterMovement.GetRotationInput();
                    proj.SetTargetByDirection( new Vector3( angle.Cos, 0f, angle.Sin ) );
                    proj.Launch();
                    projectileLaunched = true;
                }
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
