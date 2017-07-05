using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayCycle : MonoBehaviour {

    public int dayLength;
    private float timeLeft = 0;
    private bool cycleRunning = false;
    private NpcCycle npcCycle = null;
    private StampableSurfaceController failStamp = null;

	void Start () {
        npcCycle = this.gameObject.GetComponent<NpcCycle>();
        npcCycle.dayCycle = this;
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

    public void SetGameOver(StampableSurfaceController stamp)
    {
        Debug.Log("Game over!");
        failStamp = stamp;
        cycleRunning = false;
        npcCycle.endNpcCycle();
    }

    public void CheckFail()
    {
        if (failStamp != null)
        {
            if (failStamp.HasBeenStamped)
            {
                if (failStamp.StampValue)
                    SceneManager.LoadScene("Main");
                else
                    Application.Quit();
            }
        }
        else
            Debug.Log("No fail object currently in play");
    }
}
