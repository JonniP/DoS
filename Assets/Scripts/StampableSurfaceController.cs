using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampableSurfaceController : MonoBehaviour {

    private bool _hasBeenStamped = false;
    public bool HasBeenStamped { get { return _hasBeenStamped; } }
    private bool _stampValue; //is the stamp approved or denied
    public bool StampValue { get { return _stampValue; } }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider collider)
    {
        if (!_hasBeenStamped)
        {
            GameObject other = collider.gameObject;
            if (other.CompareTag("stamp"))
            {
                StampInfo info = other.GetComponent<StampInfo>();
                if (info != null)
                {
                    GameObject stampObj = Instantiate(info.stampObject);
                    stampObj.transform.SetParent(transform);
                    //TODO: put this at the right position
                    stampObj.transform.localPosition = new Vector3();
                    Vector3 objAngle = stampObj.transform.localRotation.eulerAngles;
                    stampObj.transform.localRotation = Quaternion.Euler(0, 0, objAngle.z);

                    _hasBeenStamped = true;
                    _stampValue = info.isApproved;
                }
                else Debug.Log("Stamp has no info!");
            }
        }
    }
}
