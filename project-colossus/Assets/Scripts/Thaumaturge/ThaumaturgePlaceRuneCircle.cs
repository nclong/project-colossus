using UnityEngine;
using System.Collections;

public class ThaumaturgePlaceRuneCircle : MonoBehaviour {

    private static Transform playerPos;
    public KeyCode key;
    public GameObject rune;
    public bool runePlaced = false;
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
    void FixedUpdate()
    {
        if (Input.GetKeyDown(key))
            if (!runePlaced)
            {
                runePlaced = true;
                Instantiate(rune, transform.position, Quaternion.identity);
            }
        playerPos = transform;
    }
}
