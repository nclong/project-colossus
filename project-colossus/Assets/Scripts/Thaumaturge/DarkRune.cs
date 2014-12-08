using UnityEngine;
using System.Collections;

public class DarkRune : MonoBehaviour {

    public int chargeRequired;
    private int currentCharge = 0;

    private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
        spriteRenderer = (SpriteRenderer)renderer;
	}
	
	// Update is called once per frame
	void Update () {

        if( chargeRequired >= currentCharge )
        {
            spriteRenderer.color = new Color( spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b );
        }
        else
        {
            spriteRenderer.color = new Color( spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, (float)currentCharge * 255f / (float)chargeRequired ); 
        }
	}

    void OnTriggerEnter(Collider collider)
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

    void OnTriggerExit(Collider collider)
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
}
