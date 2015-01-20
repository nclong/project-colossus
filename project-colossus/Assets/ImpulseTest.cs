using UnityEngine;
using System.Collections;

public class ImpulseTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
        if( Input.GetKey(KeyCode.T))
        {
            rigidbody.AddForce( 1000f, 1000f, 1000f, ForceMode.Impulse );
        }
	}
}
