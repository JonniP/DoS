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
    public CharacterClass[] npcPrefabs;

    private float speed = 3.0f;

    private bool cycleRunning = false;
    private CharacterClass npc = null;
    private CharacterClass npcClass = null;
    public StampableSurfaceController failStamp = null;
    public DayCycle dayCycle;
    private int failCounter;

    public int ErrorProbability = 40;
    public int MistakesToLose = 3;


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

    public void onReachedEndPos()
    {
        Debug.Log("reached end pos");

        GameObject.Destroy(npc.gameObject);
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
