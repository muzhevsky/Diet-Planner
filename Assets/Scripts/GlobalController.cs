using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    [SerializeField] GlobalUiController _uiController;
    public string[] MonthNames = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
    public int Day { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }

    public UserData UserData;

    public void OnSkipButtonPush()
    {

    }
    private void Awake()
    {
        Year = DateTime.Now.Year;
        Month = DateTime.Now.Month;
        Day = DateTime.Now.Day;

        UserData = new UserData();
    }

    public void AddWeight(int weight)
    {
        _uiController.GraphicScreen.UpdateLastWeight(weight);
    }
}
