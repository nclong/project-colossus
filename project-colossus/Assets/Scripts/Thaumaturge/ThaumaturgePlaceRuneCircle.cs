using UnityEngine;
using System.Collections;

public class ThaumaturgePlaceRuneCircle : MonoBehaviour {

    private static Transform playerPos;
    public static GameObject PlaceRune(GameObject rune, IThaumaturgeAbility ability)
    {
        ability.runePlaced = true;
        return (GameObject)Instantiate( rune, playerPos.position, playerPos.rotation );
        
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPos = transform;
    }
}
