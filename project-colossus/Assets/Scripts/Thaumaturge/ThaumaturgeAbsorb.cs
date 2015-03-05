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
		playerInput = InputManager.Players[controller];
		characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
		spriteRenderer = (SpriteRenderer)GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if( state == AbilityState.Startup ) { state = AbilityState.Active;  }
		if( state == AbilityState.Active )
		{
			if( firstUpdate )
			{
                characterMovement.Moveable = false;
				firstUpdate = false;
				spriteRenderer.color = new Color( 0f, 0f, 0f );
			}


			if( playerInput.PrimaryAbility.IsWithin( 0.0f, InputManager.GeneralEpsilon ))
			{
				spriteRenderer.color = new Color( 255f, 255f, 255f );
				firstUpdate = true;
				characterMovement.Moveable = true;
                characterMovement.Rotatable = true;
				AbilityEnd();
			}
		}
	}
}
