using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {
    [Range(0f, 1.0f)]
    public float m_movementSpeed;

    [Range( 0f, 1.0f )]
    public float m_rotationSpeed;

    [Range( 0f, 1.0f )]
    public float m_inputEpsilon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 movementInput = new Vector3( Input.GetAxis( "LeftJoystickX" ), Input.GetAxis( "LeftJoystickY" ), 0f );
        Vector3 rotationInput = new Vector3( 0f, 0f, Mathf.Atan2(Input.GetAxis( "RightJoystickY" ), Input.GetAxis( "RightJoystickX" )) * Mathf.Rad2Deg - 90.0f);
        transform.position = Vector3.Lerp( transform.position, transform.position + movementInput, m_movementSpeed );
        if( !rightJoystickIsNull )
        {
            transform.eulerAngles = new Vector3( 0f, 0f, Mathf.LerpAngle( transform.eulerAngles.z, -rotationInput.z, m_rotationSpeed ) );
        }
	}

    private bool rightJoystickIsNull
    {
        get { return Input.GetAxis( "RightJoystickX" ).IsWithin( 0f, m_inputEpsilon ) && Input.GetAxis( "RightJoystickY" ).IsWithin( 0f, m_inputEpsilon ); }
    }
}
