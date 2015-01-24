using UnityEngine;
using System.Collections;

public class ThaumaturgeRedirect : PrimaryAbility {

    public int chargePerSecond;
    public int controller;

    private CharacterAttributes characterAttributes;
    private CharacterMovement characterMovement;
    private ThaumaturgeRedirectRenderer redirectRenderer;
    private LineRenderer lineRenderer;
    private int tickLength;
    private int tickTimer;
    private bool tick;
    private RaycastHit hit;
    private AngleInput angleInput;
    private PlayerInput playerInput;
	// Use this for initialization
	void Start () {
        timer = new AbilityTimer( 0, 0, 0 );
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        redirectRenderer = (ThaumaturgeRedirectRenderer)GetComponent<ThaumaturgeRedirectRenderer>();
        lineRenderer = (LineRenderer)GetComponent<LineRenderer>();
        playerInput = InputManager.Players[controller];
        tickLength = 1f / (float)chargePerSecond;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        characterMovement.Moveable = true;
        characterMovement.Rotatable = true;
	    if( state == AbilityState.Startup )
        {
            state = AbilityState.Active;
        }
        if( state == AbilityState.Active) 
        {
            tickTimer++;
            if( tickTimer % tickLength == 0 )
            {
                tick = true;
                characterAttributes.ModifyResource( -1 );
            }
            else
            {
                tick = false;
				tickTimer = 0;
            }

            if( characterAttributes.CurrentResource > 0 )
            {
                lineRenderer.enabled = true;
                FindAndSetTarget( transform.position );
            }
        }

        if( playerInput.PrimaryAbility.IsWithin( 0.0f, InputManager.GeneralEpsilon ))
        {
            lineRenderer.enabled = false;
            AbilityEnd();
        }
	}

    private void FindAndSetTarget(Vector3 source)
    {
        angleInput = characterMovement.GetRotationInput();
        if( Physics.Raycast(source, new Vector3( angleInput.Cos, 0f, angleInput.Sin ), out hit, 200 ))
        {
            GameObject hitObject = hit.collider.gameObject;
            if( hitObject != null )
            {
                if( hitObject.tag == "PlayerObject" )
                {
                    redirectRenderer.SetVectors( transform.position, hitObject.transform.position );
                }

                else if( hitObject.tag == "EnemyObject" )
                {
                    redirectRenderer.SetVectors( transform.position, hitObject.transform.position );
                    if( tick )
                    {
                        EnemyAttributes enemyInfo = (EnemyAttributes)hitObject.GetComponent<EnemyAttributes>();
                        enemyInfo.ModifyHealth( -6 );
                    }
                }

                else if( hitObject.tag == "Rune" )
                {
                    //JUST HACK WILL HAVE TO DO THIS REGARDLESS OF RUNE LATER
                    DarkRune runeInfo = (DarkRune)hitObject.GetComponent<DarkRune>();
                    if( runeInfo.currentCharge < runeInfo.chargeRequired )
                    {
                        redirectRenderer.SetVectors( transform.position, hitObject.transform.position );
                        if( tick )
                        {
                            runeInfo.AddCharge( 1 );
                        }
                    }
                }
                else
                {
                    redirectRenderer.SetVectors( transform.position, transform.position + new Vector3( angleInput.Cos, 0f, angleInput.Sin ).normalized * 200 );
                }
            }
            else
            {
                redirectRenderer.SetVectors( transform.position, transform.position + new Vector3( angleInput.Cos, 0f, angleInput.Sin ).normalized * 200 );
            }
        }
        else
        {
            redirectRenderer.SetVectors( transform.position, transform.position + new Vector3( angleInput.Cos, 0f, angleInput.Sin ).normalized * 200 );
        }
    }
}
