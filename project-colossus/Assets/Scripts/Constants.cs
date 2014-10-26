using UnityEngine;
using System.Collections;

public static class Constants {

	public static bool IsWithin( this float x, float target, float epsilon)
    {
        return Mathf.Abs( Mathf.Abs( x ) - target ) <= epsilon;
    }
}
