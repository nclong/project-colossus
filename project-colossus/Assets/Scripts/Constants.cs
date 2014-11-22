using UnityEngine;
using System.Collections;

public static class Constants {

	public static bool IsWithin( this float x, float target, float epsilon)
    {
        return Mathf.Abs( Mathf.Abs( x ) - target ) <= epsilon;
    }

    public static Vector3 XZDistorition 
    {
    	get 
    	{
    		return new Vector3( 1.0f, 0.0f, 1.0f);
    	}
    }
}
