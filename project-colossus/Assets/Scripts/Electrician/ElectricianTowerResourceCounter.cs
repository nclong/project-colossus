using UnityEngine;
using System.Collections;

public class ElectricianTowerResourceCounter : MonoBehaviour {

	private int resource = 0;
	private double elapsedTime = 0;

	// Update is called once per frame
	void FixedUpdate () {
		elapsedTime += Time.deltaTime;

		if (elapsedTime > 1.0) {
			resource += 2;
			elapsedTime = 1;
		}
	}
}
