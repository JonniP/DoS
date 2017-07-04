using UnityEngine;

public class CharacterClass : MonoBehaviour {
    public int age;
    public string firstName;
    public string lastName;
    public string dateOfBirth;
    public string nationality;
    public string sex;
    public string id;
    public string purposeOfStay;
    public string durationOfStay;

    public Transform passportSpawnPoint;
    public Transform permitSpawnPoint;

    private PassportInfo passport;
    private PermitInfo permit;

    public void GenerateDocuments()
    {
        //TODO: generate errors

        if (passportSpawnPoint != null)
        {
            GameObject pass = (GameObject)Instantiate(Resources.Load("passport"), passportSpawnPoint.position, Quaternion.Euler(-90, 0, 0));
            passport = pass.GetComponent<PassportInfo>();

            passport.Name = firstName + " " + lastName;
            passport.Sex = sex;
            passport.Nationality = nationality;
            passport.DateOfBirth = dateOfBirth;
            passport.ID = id;
        }
        else
            Debug.Log("No spawn point set for passport!");

        if (permitSpawnPoint != null)
        {
            GameObject perm = (GameObject)Instantiate(Resources.Load("TravelPermit"), permitSpawnPoint.position, Quaternion.identity);
            permit = perm.GetComponent<PermitInfo>();

            permit.Name = firstName + " " + lastName;
            permit.ID = id;
            permit.PurposeOfStay = purposeOfStay;
            permit.DurationOfStay = durationOfStay;
        }
        else
            Debug.Log("No spawn point set for travel permit!");
    }
}
