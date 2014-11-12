using UnityEngine;
using System.Collections;

public enum CharacterState : uint {
    Null = 0,
    Moving = 1,
    Rotating = 2,
    Secondary = 4,
    Primary = 8,
    Ability1 = 16,
    Ability2 = 32,
    Ability3 = 64,
    Ability4 = 128,
    TakingDamage = 256,
}
