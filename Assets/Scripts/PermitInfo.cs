using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermitInfo : MonoBehaviour
{

    public string Name
    {
        get { return txtName.text; }
        set { txtName.text = value; }
    }

    public string ID
    {
        get { return txtID.text; }
        set { txtID.text = value; }
    }

    public string PurposeOfStay
    {
        get { return txtPurpose.text; }
        set { txtPurpose.text = value; }
    }

    public string DurationOfStay
    {
        get { return txtDuration.text; }
        set { txtDuration.text = value; }
    }

    private TextMesh txtName;
    private TextMesh txtID;
    private TextMesh txtPurpose;
    private TextMesh txtDuration;

    private StampableSurfaceController stampable;

    void Awake()
    {
        Transform textHolder = transform.Find("TextHolder");
        if (textHolder == null)
        {
            Debug.Log("Permit has no TextHolder!");
            return;
        }

        Transform tempObj = textHolder.Find("txt_name");
        if (tempObj != null) txtName = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Permit has no txt_name!");

        tempObj = textHolder.Find("txt_id");
        if (tempObj != null) txtID = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Permit has no txt_id!");

        tempObj = textHolder.Find("txt_purpose");
        if (tempObj != null) txtPurpose = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Permit has no txt_purpose!");

        tempObj = textHolder.Find("txt_duration");
        if (tempObj != null) txtDuration = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Permit has no txt_duration!");
    }
}
