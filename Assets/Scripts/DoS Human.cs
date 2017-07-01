using System;

//class for the civilians trying to cross the border
public class Human
{
    public int age;
    public string firstName;
    public string lastName;
    public string dateOfBirth;
    public string nationality;
    public string sex;
    public string purposeOfStay;
    public string durationOfStay;

	public Human()
    {
        this.age = 420;
        this.firstName = "You";
        this.lastName = "Failed";
        this.dateOfBirth = "00.00.0000";
        this.nationality = "Martian";
        this.sex = "Attack Helicopter";
        this.purposeOfStay = "To inform you stupid devs that this human's attributes are stuck to their default values";
        this.durationOfStay = "For as long as it takes you to fix this";
	}


}
