using UnityEngine;
using System.Collections;

public class ElectricianShot : MonoBehaviour, IAbility {

    public GameObject parentMissile;
    public AbilityState state {get; set; }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //FOR EDUCATIOAL PURPOSES ONLY. NOT GAME CODE
        if( Input.GetMouseButton(0) )
        {
            GameObject missile = (GameObject)Instantiate( (Object)parentMissile );
            missile.SetActive( true );
            missile.transform.position = Vector3.Lerp( missile.transform.position, missile.transform.position + Vector3.right, 0.5f );
        }
	
	}

    public int GetButton()
    {
        return 0;
    }

    public void AbilityStart()
    {
        return;
    }

    public void AbilityEnd()
    {
        return;
    }
}
