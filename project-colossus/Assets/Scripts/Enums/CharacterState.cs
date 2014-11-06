using UnityEngine;
using System.Collections;

public enum CharacterState : uint {
    Null = 0,
    Moving = 1,
    Secondary = 2,
    Primary = 4,
    Ability1 = 8,
    Ability2 = 16,
    Ability3 = 32,
    Ability4 = 64,
    TakingDamage = 128,
}
