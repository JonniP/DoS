using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour {

    private int dayLength = 30;
    private float timeLeft = 0;
    private bool cycleRunning = false;
    private NpcCycle npcCycle = null;

	void Start () {
        npcCycle = this.gameObject.GetComponent<NpcCycle>();
        startDay();
	}

	void Update () {
        if (cycleRunning)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                endDay();
            }
        }
    }

    void startDay()
    {
        Debug.Log("starting day");
        cycleRunning = true;
        timeLeft = dayLength;
        npcCycle.startNpcCycle();
    }

    void endDay()
    {
        Debug.Log("ending day");
        cycleRunning = false;
        npcCycle.endNpcCycle();


    }
}
