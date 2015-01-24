using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    public int placementCost;
    public int boltCost;
    public Text statusText;

    private CharacterAttributes characterAttributes;
    private CharacterStateController stateController;
    private ThaumaturgeDarkbolt darkbolt;
    private PlayerInput playerInput;
    private DarkRune rune;

	// Use this for initialization
	void Start () {
        runePlaced = false;
        runeFull = false;
        inRune = false;
        darkbolt = (ThaumaturgeDarkbolt)GetComponent<ThaumaturgeDarkbolt>();
        playerInput = InputManager.Players[controller];
        characterAttributes = (CharacterAttributes)GetComponent<CharacterAttributes>();
        stateController = (CharacterStateController)GetComponent<CharacterStateController>();
        rune = (DarkRune)runeObject.GetComponent<DarkRune>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
        if( state == AbilityState.Active)
        {
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
                    }
                }
                else
                {
                    AbilityEnd();
                }
            }
            if( !runePlaced )
            {

                if( characterAttributes.CurrentResource >= placementCost )
                {
                    GameObject runeInstance = ThaumaturgePlaceRuneCircle.PlaceRune( runeObject, this );
                    DarkRune runeData = (DarkRune)runeInstance.GetComponent<DarkRune>();
                    runeData.SetText( statusText );
                    characterAttributes.ModifyResource( -placementCost ); 
                }
                AbilityEnd();
            }
        }
	}

    public int GetButton()
    {
        return button;
    }

    public void AbilityStart()
    {
        state = AbilityState.Active;
    }

    public void AbilityEnd()
    {
        stateController.EndAbilities();
        state = AbilityState.Inactive;
    }
}
