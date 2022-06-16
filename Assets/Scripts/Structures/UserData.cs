using System.Collections.Generic;

public class UserData
{
    public int Id;
    public string Name;
    public string Login;
    public string PhoneNumber;
    public Goal GoalId;
    public Gender GenderId;
    public int Weight;
    public int Height;
    public int DesiredWeight;
    public int DietId;
    public List<Allergenes> Allergenes;
    public bool HadBreakfastToday;
    public bool HadLunchToday;
    public bool HadSupperToday;
    public ActivityLevel ActivityLevel;
    public EatingFrequency EatingFrequency;
}
