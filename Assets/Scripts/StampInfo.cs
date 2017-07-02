using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampInfo : MonoBehaviour {

    public bool isApproved;
    public GameObject stampObject;

    void Start ()
    {
        if (stampObject == null) Debug.Log("No object for stamp to spawn");
    }
}