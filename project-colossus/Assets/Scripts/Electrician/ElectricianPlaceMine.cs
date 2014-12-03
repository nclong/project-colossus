using UnityEngine;
using System.Collections;

public class ElectricianPlaceMine : MonoBehaviour {

    public GameObject player;
    public GameObject mine;

    public int max_mine = 2;
    public int current_mine;

	// Use this for initialization
	void Start () {
        current_mine = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1) && current_mine < 2)
        {
            Vector3 pos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5);
            Instantiate(mine, pos, Quaternion.identity);

            current_mine++;
            Debug.Log("Placed a Mine");
        }
	}
}
