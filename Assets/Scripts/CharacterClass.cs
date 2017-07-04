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
    public string passportExpiration;

    public NpcCycle cycle;

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
            passport.ExpirationDate = passportExpiration;
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

    public void CheckPassport()
    {
        //TODO: either move this to another object that tracks points and time or access that object from here

        if (passport.HasBeenStamped)
        {
            if (passport.StampValue != passport.InformationIsCorrect)
            {
                //TODO: add a penalty point
            }

            GameObject.Destroy(passport.gameObject);
            GameObject.Destroy(permit.gameObject);

            cycle.switchNpc(passport.StampValue);
        }
    }
}
