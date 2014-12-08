using UnityEngine;

public class PrimaryAbility : MonoBehaviour
{
    public AbilityState state;
    public AbilityTimer timer;
    public void AbilityStart() { state = AbilityState.Startup; timer.Start(); }
    public void AbilityEnd() { state = AbilityState.Inactive; }
}