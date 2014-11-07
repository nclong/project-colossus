using UnityEngine;
using System.Collections;

public class CharacterAnimationCollector : MonoBehaviour {

    private Animator animator;
    private RuntimeAnimatorController animationController;
    private CharacterMovement characterMovement;
    private SpriteRotationState rotationState;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>() as Animator;
        characterMovement = GetComponent<CharacterMovement>() as CharacterMovement;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        //Get States
        rotationState = characterMovement.RotationState;

        //Apply Logic To States

        //Send States to Animator
        switch( rotationState )
        {
            case SpriteRotationState.Up:
                animator.SetInteger( "RotationMovement", 0 );
                break;
            case SpriteRotationState.Down:
                animator.SetInteger( "RotationMovement", 1 );
                break;
            default:
                animator.SetInteger( "RotationMovement", 0 );
                break;
        }
	    
	}
}
