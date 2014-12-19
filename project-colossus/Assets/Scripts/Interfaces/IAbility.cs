using UnityEngine;
using System.Collections;

public interface IAbility
{
    int GetButton();
    AbilityState state { get; set; }
    void AbilityStart();
    void AbilityEnd();
}

