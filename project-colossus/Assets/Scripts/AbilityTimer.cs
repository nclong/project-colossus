using UnityEngine;
using System.Collections;

public class AbilityTimer {
    public AbilityState State { get; private set; }
	public int currentTimer { get; private set; }
    private int timerMax;
    private int startupTime;
    private int activeTime;
    private int cooldownTime;

    public AbilityTimer( int startTimeIn, int activeTimeIn, int cooldownTimeIn )
    {
        startupTime = startTimeIn;
        activeTime = activeTimeIn;
        cooldownTime = cooldownTimeIn;

        timerMax = startupTime + activeTime + cooldownTime;
    }

	public void Update () {
        if( State != AbilityState.Inactive && State != AbilityState.Null )
        {
			currentTimer++;
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
        currentTimer = 0;
    }

    public bool IsOver
    {
        get { return currentTimer >= timerMax; }
    }
}
