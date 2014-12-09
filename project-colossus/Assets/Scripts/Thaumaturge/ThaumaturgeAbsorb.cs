using UnityEngine;
using System.Collections;

public class ThaumaturgeAbsorb : SecondaryAbility {

	public int controller;

	private PlayerInput playerInput;
	private CharacterMovement characterMovement;
	private bool firstUpdate = true;
	private SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		playerInput = InputManager.Players[contoller];
		characterMovement = (CharacterMovement)GetComponenet<CharacterMovement>();
		spriteRenderer = (SpriteRenderer)renderer;
	}
	
	// Update is called once per frame
	void Update () {
		if( state == AbilityState.Active )
		{
			if( firstUpdate )
			{
				characterMovement.Moveable = false;
				firstUpdate = false;
				spriteRenderer.color = new Color( 0f, 0f, 0f );
			}


			if( playerInput.Secondary.IsWithin( 0.0f, InputManager.GeneralEpsilon ))
			{
				spriteRenderer.color = new Color( 255f, 255f, 255f );
				firstUpdate = true;
				characterMovement = true;
				AbilityEnd();
			}
		}
	}
}
