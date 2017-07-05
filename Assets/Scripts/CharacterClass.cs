using UnityEngine;
using System;

public class CharacterClass : MonoBehaviour {
    //public int age;
    //public string firstName;
    //public string lastName;
    //public string dateOfBirth;
    //public string nationality;
    //public string sex;
    //public string id;
    //public string purposeOfStay;
    //public string durationOfStay;
    //public string passportExpiration; ATM not used

    public NpcCycle npcCycle;

    public Transform passportSpawnPoint;
    public Transform permitSpawnPoint;

    public int ErrorProbability;

    private PassportInfo passport;
    private PermitInfo permit;
    private int FailCounter = 0;
    private StampableSurfaceController failStamp;

    public void GenerateDocuments()
    {
        System.Random rng = new System.Random();
        string[] Nationalities = { "East Swedish", "West Swedish" };
        string[] FemaleNames = { "Emma", "Amanda", "Anna", "Julia", "Hanna", "Emilia", "Sara", "Johanna", "Matilda", "Ida", "Sandra", "Sofia", "Alice", "Tilda", "Ellen", "Elsa", "Moa", "Caroline", "Lovisa", "Fanny", "Alexandra", "Kajsa", "Ella", "Nora", "Filippa", "Josefin", "Jannike", "Mimmi", "Camilla", "Pernilla" };
        string[] MaleNames = { "Adolf", "Alexander", "Brand", "Carlgustav", "Oscar", "Daniel", "Johan", "Sebastian", "Marcus", "Rasmus", "Erik", "Jacob", "Max", "Isak", "Jesper", "Kalle", "Linus", "Robert", "Robin", "Thomas", "Jonatan", "Mikael", "Simon", "Måns", "Stefan", "Leo", "Anders", "Petter", "Amir", "Joakim" };
        string[] SurNames = { "Johansson", "Andersson", "Karlsson", "Nilsson", "Eriksson", "Larsson", "Olsson", "Persson", "Gustavsson", "Jansson", "Andersen", "Lundberg", "Lindgren", "Berg", "Tapper", "Berglund", "Lindqvist", "Eklund", "Sandberg", "Fors", "Nygren", "Wallin", "Bjergersen", "Ekström", "Lindholm", "Bergman", "Smedlund", "Strohecker", "Strömberg", "Blom" };
        string[] durationofStay = { "One day", "Two days", "Three days", "Four days", "Five days", "Six days", "A week", "Two weeks", "Three weeks", "A month", "Two months", "A year", "Indefinite" };
        string[] purposeOfStay = { "Education", "Conference", "Business meeting", "Passing through", "Immigration", "Seeing family", "Seeing friend", "Vacation" };

        int rand = rng.Next(0, 101);

        if (passportSpawnPoint != null)
        {
            GameObject pass = (GameObject)Instantiate(Resources.Load("passport"), passportSpawnPoint.position, Quaternion.Euler(-90, 0, 0));
            passport = pass.GetComponent<PassportInfo>();
            passport.InformationIsCorrect = true;

            if (rng.Next(0, 101)>= 54)
            {
                passport.Sex = "Male";
            }
            else passport.Sex = "Female";
            if (passport.Sex == "Male")
            {
                passport.Name = MaleNames[rng.Next(0, 30)] + " " + SurNames[rng.Next(0, 30)];
            }
            else passport.Name = FemaleNames[rng.Next(0, 30)] + " " + SurNames[rng.Next(0, 30)];

            passport.Nationality = Nationalities[rng.Next(0, 2)];
            passport.DateOfBirth = rng.Next(1, 31) + "/" + rng.Next(1, 13) + "/" + rng.Next(1900, 2018);
            passport.ID = rng.Next(100000, 200000).ToString();
            passport.ExpirationDate = rng.Next(1, 31) + "/" + rng.Next(1, 13) + "/";
            if (rand < ErrorProbability / 3)
            {
                passport.ExpirationDate += rng.Next(1998, 2016);
                passport.InformationIsCorrect = false;
            }
            else
                passport.ExpirationDate += rng.Next(2018, 2023);

        }
        else
            Debug.Log("No spawn point set for passport!");

        if (permitSpawnPoint != null)
        {
            GameObject perm = (GameObject)Instantiate(Resources.Load("TravelPermit"), permitSpawnPoint.position, Quaternion.identity);
            permit = perm.GetComponent<PermitInfo>();

            if (rand < ErrorProbability / 3 || rand > ErrorProbability * 2/3) permit.Name = passport.Name;
            else if (passport.Sex == "Male")
            {
                permit.Name = MaleNames[rng.Next(0, 30)] + " " + SurNames[rng.Next(0, 30)];
                passport.InformationIsCorrect = false;
            }
            else
            {
                permit.Name = FemaleNames[rng.Next(0, 30)] + " " + SurNames[rng.Next(0, 30)];
                passport.InformationIsCorrect = false;
            }

            if (rand < ErrorProbability * 2/3 || rand > ErrorProbability) permit.ID = passport.ID;
            else
            {
                permit.ID = rng.Next(100000, 200000).ToString();
                passport.InformationIsCorrect = false;
            }

            permit.PurposeOfStay = purposeOfStay[rng.Next(0, 8)];
            permit.DurationOfStay = durationofStay[rng.Next(0, 13)];
        }
        else
            Debug.Log("No spawn point set for travel permit!");
    }

    public void CheckPassport()
    {
        if (passport.HasBeenStamped)
        {
            if (passport.StampValue != passport.InformationIsCorrect)
            {
                FailCounter++;
                if (FailCounter >= 1)
                {
                    GameObject fail = (GameObject)Instantiate(Resources.Load("Fail"), passportSpawnPoint.position, Quaternion.Euler(-90, 0, 0));
                    npcCycle.dayCycle.SetGameOver(fail.GetComponentInChildren<StampableSurfaceController>());
                }
            }

            GameObject.Destroy(passport.gameObject);
            GameObject.Destroy(permit.gameObject);

            npcCycle.switchNpc(passport.StampValue);
        }
    }

    public void CheckFail()
    {
        npcCycle.dayCycle.CheckFail();
    }
}
