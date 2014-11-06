using UnityEngine;
using System.Collections;

public enum AbilityState : byte {
    Null = 0,
    Inactive = 1,
    Startup = 63,
    Active = 127,
    Cooldown = 255
}
