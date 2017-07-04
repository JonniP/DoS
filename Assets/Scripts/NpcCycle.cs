using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCycle : MonoBehaviour {

    public Transform spawnPos;
    public Transform waitPos;
    public Transform endPos;
    public Transform passportSpawnPoint;
    public Transform permitSpawnPoint;
    public GameObject[] npcPrefabs;

    private float speed = 3;

    private bool movingToWaitPos = false;
    private bool movingToEndPos = false;

    private bool cycleRunning = false;
    private GameObject npc = null;
    private CharacterClass npcClass = null;

    private void Update()
    {
        if(movingToWaitPos)
        {
            npc.transform.position = Vector3.MoveTowards(
                npc.transform.position, waitPos.position, speed * Time.deltaTime);

            if (npc.transform.position == waitPos.position)
            {
                onReachedWaitPos();
            }
        }

        if (movingToEndPos)
        {
            npc.transform.position = Vector3.MoveTowards(
                npc.transform.position, endPos.position, speed * Time.deltaTime);

            if (npc.transform.position == endPos.position)
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
        // TODO: Instead of immediately continuing, give passport to player
        // and fire switchNpc() when passport is given back
        Debug.Log("reached wait pos");
        movingToWaitPos = false;

        npcClass.GenerateDocuments();
    }

    public void onReachedEndPos()
    {
        Debug.Log("reached end pos");
        movingToEndPos = false;

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
        npcClass.passportSpawnPoint = this.passportSpawnPoint;
        npcClass.permitSpawnPoint = this.permitSpawnPoint;

        movingToWaitPos = true;
    }

    private void switchNpc()
    {
        movingToEndPos = true;
    }
}
