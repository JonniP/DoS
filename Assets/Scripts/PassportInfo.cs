using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassportInfo : MonoBehaviour {

    public string Name
    {
        get { return txtName.text; }
        set { txtName.text = value; }
    }

    public string Sex
    {
        get { return txtSex.text; }
        set { txtSex.text = value; }
    }

    public string Nationality
    {
        get { return txtNationality.text; }
        set { txtNationality.text = value; }
    }

    public string DateOfBirth
    {
        get { return txtDateOfBirth.text; }
        set { txtDateOfBirth.text = value; }
    }

    public string ID
    {
        get { return txtID.text; }
        set { txtID.text = value; }
    }

    public bool InformationIsCorrect { get; set; }


    private TextMesh txtName;
    private TextMesh txtSex;
    private TextMesh txtNationality;
    private TextMesh txtDateOfBirth;
    private TextMesh txtID;

    void Start()
    {
        Transform textHolder = transform.Find("TextHolder");
        Transform tempObj = textHolder.Find("txt_name");
        if (tempObj != null) txtName = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Passport has no txt_name!");

        tempObj = textHolder.Find("txt_sex");
        if (tempObj != null) txtSex = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Passport has no txt_sex!");

        tempObj = textHolder.Find("txt_nationality");
        if (tempObj != null) txtNationality = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Passport has no txt_nationality!");

        tempObj = textHolder.Find("txt_dateOfBirth");
        if (tempObj != null) txtDateOfBirth = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Passport has no txt_dateOfBirth!");

        tempObj = textHolder.Find("txt_id");
        if (tempObj != null) txtID = tempObj.GetComponent<TextMesh>();
        else Debug.Log("Passport has no txt_id!");
    }
}
