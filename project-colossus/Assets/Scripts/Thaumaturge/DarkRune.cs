using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DarkRune : MonoBehaviour {

    public int chargeRequired;
    public Text statusText;
    public int currentCharge { get; private set; }

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = (SpriteRenderer)renderer;
        currentCharge = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        statusText.text = currentCharge.ToString() + " / " + chargeRequired.ToString();
        statusText.transform.position = Camera.main.WorldToScreenPoint( transform.position );
        statusText.transform.position = new Vector3( statusText.transform.position.x + 69, statusText.transform.position.y - 35, statusText.transform.position.z );
        if( currentCharge >= chargeRequired )
        {
            transform.gameObject.layer = LayerMask.NameToLayer( "Ignore Raycast" );
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        ThaumaturgeDark dark = (ThaumaturgeDark)collider.gameObject.GetComponent<ThaumaturgeDark>();
        if( dark != null )
        {
            dark.inRune = true;
            if( currentCharge >= chargeRequired )
            {
                dark.runeFull = true;
            }
        }

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        ThaumaturgeDark dark = (ThaumaturgeDark)collider.gameObject.GetComponent<ThaumaturgeDark>();
        if( dark != null )
        {
            dark.inRune = false;
            if( currentCharge >= chargeRequired )
            {
                dark.runeFull = true;
            }
        }
    }

    public void AddCharge(int x )
    {
        currentCharge += x;
    }

    public void SetText(Text txt)
    {
        statusText = txt;
        statusText.enabled = true;
    }
}
