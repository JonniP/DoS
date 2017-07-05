using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassportReceiverController : MonoBehaviour {

    public CharacterClass npc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("passport"))
        {
            npc.CheckPassport();
        }
        else if (other.CompareTag("fail"))
        {
            npc.CheckFail();
        }
    }
}
