using UnityEngine;
using System.Collections;

public class ThaumaturgeRedirectRenderer : MonoBehaviour {

    public float kinkLength;
    public float kinkIntensity;

    private int vertexCount;
    private Vector3 source;
    private Vector3 destination;
    private Vector3 source2Dest;
    private Vector3 kinkOffset;
    private Vector3 kinkPos;
    private Vector3 randomKink;
    private float radAngle;
    private AngleInput angleInput;
    private LineRenderer lineRenderer;
    private CharacterMovement characterMovement;

	// Use this for initialization
	void Start () {
        lineRenderer = (LineRenderer)GetComponent<LineRenderer>();
        characterMovement = (CharacterMovement)GetComponent<CharacterMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if( lineRenderer.enabled )
        {
            vertexCount = (int)( source2Dest.magnitude / kinkLength );
            lineRenderer.SetVertexCount( vertexCount );
            angleInput = characterMovement.GetRotationInput();
            radAngle = Mathf.Atan2( angleInput.Sin, angleInput.Cos );
            kinkOffset = new Vector3( Mathf.Cos( radAngle ), 0f, Mathf.Sin( radAngle ) ).normalized * kinkLength;

            for( int i = 0; i < vertexCount; ++i )
            {
                randomKink = new Vector3( Random.Range( -kinkIntensity, kinkIntensity ), 0f, Random.Range( -kinkIntensity, kinkIntensity ) ).PerspectiveAdjusted();
                kinkPos = source + kinkOffset * i + randomKink;
                lineRenderer.SetPosition( i, kinkPos );
            }

            if( vertexCount > 1 )
            {
                lineRenderer.SetPosition( 0, source );
                lineRenderer.SetPosition( vertexCount - 1, new Vector3(destination.x, source.y, destination.z));
            } 
        }
	}

    public void SetVectors(Vector3 src, Vector3 dst)
    {
        source = src;
        destination = dst;
        source2Dest = destination - source;
    }
}
