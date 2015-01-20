using UnityEngine;
using System.Collections;

public class DummyAbility : MonoBehaviour, IAbility {
	public int button;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	public int GetButton()
	{
		return button;
	}
	public AbilityState state { get; set; }
	public void AbilityStart()
	{
	}
	public void AbilityEnd(){
		}
}
