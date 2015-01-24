using UnityEngine;

public class PrimaryAbility : MonoBehaviour
{
    public AbilityState state;
    public AbilityTimer timer;
    private CharacterStateController stateController;
    public void AbilityStart() 
    { 
        state = AbilityState.Startup;
        timer.Start();
    }
    public void AbilityEnd() 
    { 
        state = AbilityState.Inactive;
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        stateController.RemoveState( CharacterState.Primary );
        stateController.EndAbilities();
    }
}