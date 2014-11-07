using UnityEngine;
using System.Collections;

public interface IAbility
{
    AbilityState state { get; set; }
    void AbilityStart();
    void AbilityEnd();
}

