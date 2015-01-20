using UnityEngine;
using System.Collections;

public class CursorManager : MonoBehaviour {
    private GameObject parentObject;
    private CharacterMovement parentMovementComponent;

    public float m_cursorMovement;
    public float cursorDistance;

    public float xInput;
    public float yInput;
	// Use this for initialization
	void Start () {
        parentObject = transform.parent.gameObject;
        parentMovementComponent = parentObject.GetComponent<CharacterMovement>() as CharacterMovement;
	}
	
	// Update is called once per frame
	void LateUpdate () {        
        AngleInput angleInput = parentMovementComponent.GetRotationInput();
        xInput = angleInput.Cos;
        yInput = angleInput.Sin;
        if( angleInput.FromInput )
        {
            transform.localPosition = new Vector3( angleInput.Cos, angleInput.Sin, transform.localPosition.z) * cursorDistance; 
        }
	}
}
