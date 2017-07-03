using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcCycle : MonoBehaviour {

    public Transform spawnPos;
    public Transform waitPos;
    public Transform endPos;
    public GameObject[] npcPrefabs;

    private bool cycleRunning = false;
    private GameObject npc = null;

    public void startNpcCycle()
    {
        Debug.Log("starting npc cycle...");
        cycleRunning = true;
    }

    public void endtNpcCycle()
    {
        Debug.Log("ending npc cycle...");
        cycleRunning = false;
    }

    private void spawnNPC()
    {
        if (!cycleRunning) return;

        npc = Instantiate(npcPrefabs[0], new Vector3(spawnPos.position.x, 
            spawnPos.position.y, spawnPos.position.z), Quaternion.identity);

        // Move npc to waitPos
    }

    private void switchNpc()
    {
        // TODO: Move npc to endPos
        if (npc != null) {
            GameObject.Destroy(npc);
        }

        spawnNPC();
    }

}
