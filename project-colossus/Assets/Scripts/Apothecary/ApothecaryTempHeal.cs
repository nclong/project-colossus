﻿using UnityEngine;
using System.Collections;

public class ApothecaryTempHeal : PrimaryAbility
{

    public int healRate;
    public int controller;

    private CharacterAttributes characterAttributes;
    private CharacterMovement characterMovement;
    private LineRenderer lineRenderer;
    private float tickLength;
    private float tickTimer;
    private bool tick;
    private RaycastHit hit;
    private AngleInput angleInput;
    private PlayerInput playerInput;
    // Use this for initialization
    void Start()
    {
        timer = new AbilityTimer( 0, 0, 0 );
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
        lineRenderer = (LineRenderer)GetComponent<LineRenderer>();
        playerInput = InputManager.Players[controller];
        tickLength = 1f / (float)healRate;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        characterMovement.Moveable = true;
        characterMovement.Rotatable = true;
        if( state == AbilityState.Startup )
        {
            state = AbilityState.Active;
        }
        if( state == AbilityState.Active )
        {
            tickTimer += Time.deltaTime;
            if( tickTimer >= tickLength )
            {
                tick = true;
                tickTimer = tickTimer - tickLength;
                tickTimer = 0f;
            }
            else
            {
                tick = false;
            }

            if( characterAttributes.CurrentResource > 0 )
            {
                lineRenderer.enabled = true;
                FindAndSetTarget( transform.position );
            }
        }

        if( playerInput.PrimaryAbility.IsWithin( 0.0f, InputManager.GeneralEpsilon ) )
        {
            lineRenderer.enabled = false;
            AbilityEnd();
        }
    }

    private void FindAndSetTarget( Vector3 source )
    {
        angleInput = characterMovement.GetRotationInput();
        if( Physics.Raycast( source, new Vector3( angleInput.Cos, 0f, angleInput.Sin ), out hit, 200 ) )
        {
            GameObject hitObject = hit.collider.gameObject;
            if( hitObject != null )
            {
                if( hitObject.tag == "PlayerObject" )
                {
                    CharacterAttributes attributes = (CharacterAttributes)hitObject.GetComponent<CharacterAttributes>();
                    if( tick )
                    {
                        attributes.ModifyHealth( 1 ); 
                    }

                    lineRenderer.SetPosition( 0, transform.position );
                    lineRenderer.SetPosition( 1, hitObject.transform.position );
                }
                else
                {
                    lineRenderer.SetPosition( 0, transform.position );
                    lineRenderer.SetPosition( 1, transform.position + new Vector3( angleInput.Cos, 0f, angleInput.Sin ).normalized * 200 );
                }
            }
            else
            {
                lineRenderer.SetPosition(0, transform.position); 
                lineRenderer.SetPosition(1, transform.position + new Vector3( angleInput.Cos, 0f, angleInput.Sin ).normalized * 200 );
            }
        }
        else
        {
            lineRenderer.SetPosition( 0, transform.position );
            lineRenderer.SetPosition( 1, transform.position + new Vector3( angleInput.Cos, 0f, angleInput.Sin ).normalized * 200 );
        }
    }
}
