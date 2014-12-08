using UnityEngine;
using System.Collections;

public class ThaumaturgeDark : MonoBehaviour, IAbility, IThaumaturgeAbility
{

    public AbilityState state {get; set;}
    [Range( 0f, 3f )]
    public int button;
    [Range( 0f, 3f )]
    public int controller;
    public bool runePlaced { get; set; }
    public bool runeFull { get; set; }
    public bool inRune { get; set; }
    public GameObject runeObject;
    public int chargePerSecond;
    public int placementCost;

    private CharacterAttributes characterAttributes;
    private ThaumaturgeDarkbolt darkbolt;
    private PlayerInput playerInput;
    private DarkRune rune;
    private float tickLength;
    private float tickTimer;

	// Use this for initialization
	void Start () {
        runePlaced = false;
        runeFull = false;
        inRune = false;
        darkbolt = (ThaumaturgeDarkbolt)GetComponent<ThaumaturgeDarkbolt>();
        playerInput = InputManager.Players[controller];
        tickLength = 1f / (float)chargePerSecond;
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        rune = (DarkRune)runeObject.GetComponent<DarkRune>();
	}
	
	// Update is called once per frame
	void Update () {
	
        if( state == AbilityState.Active)
        {
            if( !runePlaced && characterAttributes.CurrentResource >= placementCost)
            {
                ThaumaturgePlaceRuneCircle.PlaceRune( runeObject, this );
                characterAttributes.ModifyResource( -placementCost );
            }
            if( runePlaced )
            {
                if( inRune )
                {
                    if( runeFull )
                    {
                        if( darkbolt.state == AbilityState.Inactive || darkbolt.state == AbilityState.Null )
                        {
                            darkbolt.AbilityStart();
                        }
                    }
                    else
                    {
                        if( !playerInput.Abilities[button] )
                        {
                            AbilityEnd();
                        }
                        else
                        {
                            tickTimer += Time.deltaTime;
                            if( tickTimer >= tickLength )
                            {
                                if( characterAttributes.CurrentResource >= 1 )
                                {
                                    characterAttributes.ModifyResource( -1 );
                                    rune.AddCharge( 1 );
                                    tickTimer = tickLength - tickTimer;
                                }
                            }
                        }
                    }
                }
            }
        }
	}

    public int GetButton()
    {
        return 0;
    }

    public void AbilityStart()
    {
        tickTimer = 0.0f;
        state = AbilityState.Active;
    }

    public void AbilityEnd()
    {

    }
}
