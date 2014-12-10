using UnityEngine;

public class SecondaryAbility : MonoBehaviour {
    public AbilityState state;
    private CharacterStateController stateController;
    
    public void AbilityStart() 
    { 
        state = AbilityState.Startup; 
    }
    public void AbilityEnd() 
    { 
        state = AbilityState.Inactive;
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        stateController.RemoveState( CharacterState.Secondary );
        stateController.EndAbilities();
    }
}