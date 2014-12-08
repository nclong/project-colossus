using UnityEngine;

public class SecondaryAbility : MonoBehaviour {
    public AbilityState state;
    public void AbilityStart() { state = AbilityState.Startup; }
    public void AbilityEnd() { state = AbilityState.Inactive; }
}