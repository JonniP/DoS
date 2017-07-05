using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCycle : MonoBehaviour {

    public Transform spawnPos;
    public Transform waitPos;
    public Transform endPos;
    public Transform passportSpawnPoint;
    public Transform permitSpawnPoint;
    public PassportReceiverController passportReceiver;
    public GameObject[] npcPrefabs;

    private float speed = 3.0f;

    private bool movingToWaitPos = false;
    private bool movingToEndPos = false;
    private bool movingBackToStart = false;

    private bool cycleRunning = false;
    private GameObject npc = null;
    private CharacterClass npcClass = null;
    public StampableSurfaceController failStamp = null;
    public DayCycle dayCycle;
    private int failCounter;

    public int ErrorProbability = 40;
    public int MistakesToLose = 3;

    private void Update()
    {
        if(movingToWaitPos)
        {
            npc.transform.position = Vector3.MoveTowards(
                npc.transform.position, waitPos.position, speed * Time.deltaTime);

            if (npc.transform.position == waitPos.position)
            {
                npc.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
                onReachedWaitPos();
            }
        }
        else if (movingToEndPos)
        {
            npc.transform.position = Vector3.MoveTowards(
                npc.transform.position, endPos.position, speed * Time.deltaTime);

            if (npc.transform.position == endPos.position)
            {
                onReachedEndPos();
            }
        }
        else if (movingBackToStart)
        {
            npc.transform.position = Vector3.MoveTowards(
                npc.transform.position, spawnPos.position, speed * Time.deltaTime);

            if (npc.transform.position == spawnPos.position)
            {
                onReachedEndPos();
            }
        }
    }

    public void startNpcCycle()
    {
        Debug.Log("starting npc cycle...");
        cycleRunning = true;
        spawnNPC();
    }

    public void endNpcCycle()
    {
        Debug.Log("ending npc cycle...");
        cycleRunning = false;
    }

    public void onReachedWaitPos()
    {
        Debug.Log("reached wait pos");

        movingToWaitPos = false;

        npcClass.GenerateDocuments();
    }

    public void onReachedEndPos()
    {
        Debug.Log("reached end pos");
        movingToEndPos = false;
        movingBackToStart = false;

        GameObject.Destroy(npc);
        spawnNPC();
    }

    private void spawnNPC()
    {
        Debug.Log("spawning npc...");
        if (!cycleRunning) return;

        npc = Instantiate(npcPrefabs[0], new Vector3(spawnPos.position.x, 
            spawnPos.position.y, spawnPos.position.z), Quaternion.identity);
        npcClass = npc.GetComponent<CharacterClass>();
        npcClass.npcCycle = this;
        npcClass.passportSpawnPoint = this.passportSpawnPoint;
        npcClass.permitSpawnPoint = this.permitSpawnPoint;
        npcClass.ErrorProbability = ErrorProbability;
        passportReceiver.npc = npcClass;

        movingToWaitPos = true;
    }

    public void switchNpc (bool isApproved)
    {
        if (isApproved) {
            npc.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            movingToEndPos = true;
        } else {
            npc.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
            movingBackToStart = true;
        }
    }

    public void AddFail ()
    {
        failCounter++;
        if (failCounter >= MistakesToLose)
        {
            GameObject fail = (GameObject)Instantiate(Resources.Load("Fail"), passportSpawnPoint.position, Quaternion.Euler(-90, 0, 0));
            dayCycle.SetGameOver(fail.GetComponentInChildren<StampableSurfaceController>());
        }
    }
}
