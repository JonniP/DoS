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
    private PassportInfo passport;

    public void CreatePassport()
    {
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
    }
}
