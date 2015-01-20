using UnityEngine;
using System.Collections;

public class AbilityTimer {
    public AbilityState State { get; private set; }
    private float currentTimer;
    private float timerMax;
    private float startupTime;
    private float activeTime;
    private float cooldownTime;

    public AbilityTimer( float startTimeIn, float activeTimeIn, float cooldownTimeIn )
    {
        startupTime = startTimeIn;
        activeTime = activeTimeIn;
        cooldownTime = cooldownTimeIn;

        timerMax = startupTime + activeTime + cooldownTime;
    }

	public void Update () {
        if( State != AbilityState.Inactive && State != AbilityState.Null )
        {
            currentTimer += Time.deltaTime;
            if( currentTimer <= startupTime )
            {
                State = AbilityState.Startup;
            }
            else if( currentTimer <= activeTime )
            {
                State = AbilityState.Active;
            }
            else
            {
                State = AbilityState.Cooldown;
            }
        }
	}

    public void Start()
    {
        State = AbilityState.Startup;
        currentTimer = 0.0f;
    }

    public bool IsOver
    {
        get { return currentTimer >= timerMax; }
    }
}
