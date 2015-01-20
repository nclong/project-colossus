using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public static class Constants {

	public static bool IsWithin( this float x, float target, float epsilon)
    {
        return Mathf.Abs( Mathf.Abs( x ) - target ) <= epsilon;
    }

    public static bool IsLessThanMinusEpsilon( this float x, float target, float epsilon)
    {
        return Mathf.Abs(x - target) < Mathf.Abs(epsilon - target) && x <= target;
    }

    public static bool IsGreaterThanPlusEpsilon( this float x, float target, float epsilon)
    {
        return x > ( target + epsilon );
    }

    public static bool IsGreaterThanEqualPlusEpsilon( this float x, float target, float epsilon )
    {
        return x >= ( target + epsilon );
    }

    public static Vector3 XZDistorition 
    {
    	get 
    	{
    		return new Vector3( 1.0f, 0.0f, 1.0f);
    	}
    }

    public static Vector3 PerspectiveAdjusted( this Vector3 u )
    {
        float distortion = ( 1 / Mathf.Tan(Camera.main.transform.eulerAngles.x * Mathf.Deg2Rad ) ) ;
        return new Vector3( u.x, u.y, u.z * distortion );
    }

    public static Dictionary<string, Type> SecondaryAttacks
    {
        get
        {
            Dictionary<string, Type> dict = new Dictionary<string, Type>();
            dict["pugilist"] = typeof( PugilistCharge );
            dict["apothecary"] = typeof( ApothecaryDrink );
            dict["electrician"] = typeof( ElectricianCharge );
            dict["thaumaturge"] = typeof( ThaumaturgeAbsorb );

            return new Dictionary<string, Type>( dict );
        }
    }

    public static Vector2 In2D( this Vector3 u )
    {
        return new Vector2( u.x, u.y );
    }
    
    public static Vector2 ToVector2( this float angle )
    {
        return new Vector2( Mathf.Cos(angle), Mathf.Sin(angle) ).normalized;
    }

    public static float ToAngle( this Vector2 u )
    {
        return Mathf.Atan2( u.y, u.x );
    }
}
