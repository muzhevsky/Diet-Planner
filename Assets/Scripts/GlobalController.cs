using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalController
{
    [SerializeField] static GlobalUiController _uiController;
    public static string[] MonthNames { get; private set; }
    public static int Day { get; private set; }
    public static int Month { get; private set; }
    public static int Year { get; private set; }
    
    public static UserData UserData;

    static int _lastDayUsed;
    public static void Init()
    {
        DBOperator.Init();

        Year = DateTime.Now.Year;
        Month = DateTime.Now.Month;
        Day = DateTime.Now.Day;

        UserData = new UserData();
        if (_lastDayUsed != Day)
        {
            PlayerPrefs.SetInt("HadBreakfastToday", 0);
            PlayerPrefs.SetInt("HadLunchToday", 0);
            PlayerPrefs.SetInt("HadSupperToday", 0);
            _lastDayUsed = Day;
        }

        MonthNames = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
    }
}
