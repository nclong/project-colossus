using UnityEngine;
using System.Collections;

public class ElectricianAttack : PrimaryAbility {

    public int controller;
    public int firingRate;
    public float spread;
    public GameObject parentProjectile;

    private CharacterMovement characterMovement;
    private float tickLength;
    private float tickTimer = 0;
    private AngleInput angleInput;
    private PlayerInput playerInput;
	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( 0, 0, 0 );
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        playerInput = InputManager.Players[controller];
        tickLength = 1f / (float)firingRate;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        characterMovement.Moveable = true;
        characterMovement.Rotatable = true;
        if( state == AbilityState.Startup )
        {
            tickTimer = 0f;
            state = AbilityState.Active;
        }

        if( state == AbilityState.Active )
        {
            tickTimer += Time.deltaTime;
            if( tickTimer >= tickLength )
            {
                tickTimer = tickTimer - tickLength;
                GameObject projObj = (GameObject)Instantiate(parentProjectile,  transform.position, transform.rotation );
                Projectile proj = (Projectile)projObj.GetComponent<Projectile>();
                angleInput = characterMovement.GetRotationInput();
                proj.SetTargetByAngle_Deg( angleInput.Angle + Random.Range( -spread, spread ) );
                proj.Launch();
                tickTimer = 0f;
            }

            if( playerInput.PrimaryAbility.IsWithin(0.0f, InputManager.GeneralEpsilon))
            {
                AbilityEnd();
            }
        }
	
	}
}
