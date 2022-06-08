using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AnswersList 
{
    public Gender Gender;
    public Goal Goal;
    public int Height;
    public int Weight;
    public int DesiredWeight;
    public List<Allergenes> Allergenes;
    public EatingFrequency EatingFrequency;
    public ActivityLevel ActivityLevel;
}

public enum Allergenes
{
    None = 1,
    Milk,
    Nuts,
    Citrus,
    Coffee,
    Berries
}
public enum Gender
{
    Female = 1,
    Male
}
public enum Goal
{
    LoseWeight = 1,
    RaiseWeight,
    SaveWeight
}
public enum EatingFrequency
{
    OneTime = 1,
    TwoThreeTimes,
    MoreThenThreeTimes
}
public enum ActivityLevel
{
    High = 1,
    Medium,
    Low
}