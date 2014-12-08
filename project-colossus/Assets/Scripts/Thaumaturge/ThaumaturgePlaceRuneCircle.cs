using UnityEngine;
using System.Collections;

public class ThaumaturgePlaceRuneCircle : MonoBehaviour {

    private static Transform playerPos;
    public static void PlaceRune(GameObject rune, IThaumaturgeAbility ability)
    {
        Instantiate( rune, playerPos.position, playerPos.rotation );
        ability.runePlaced = true;
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
