using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    [SerializeField] Day[] _days;
    [SerializeField] Text _month;
    [SerializeField] Text _year;
    int[] _monthCapacity = { 31, 28, 31, 30, 31, 31, 30, 31, 30, 31, 30, 31 };
    

    private void Start()
    {
        FillCalendar();
    }
    public void FillCalendar()
    {
        int thisDay = DateTime.Today.Day;
        int thisMonth = DateTime.Today.Month;
        int thisYear = DateTime.Today.Year;

        _month.text = GlobalController.MonthNames[thisMonth-1];
        _year.text = thisYear.ToString();
        if (IsLeapYear(thisYear)) _monthCapacity[1] = 29;
        else _monthCapacity[1] = 28;

        int pos = 0;
        while (pos != (int)DateTime.Today.DayOfWeek-1) pos++;

        for(int i = pos; i < _monthCapacity[thisMonth] +pos; i++)
        {
            _days[i].SetText((i - pos + 1).ToString());
            _days[pos + thisDay - 1].Highlight(false);
        }

        _days[pos + thisDay - 1].Highlight(true);
    }
    public bool IsLeapYear(int year)
    {
        if (year % 4 != 0) return false;
        else
        {
            if(year % 100 != 0)
            {
                if (year % 400 == 0) return true;
                else return false;
            }
        }
        return true;
    }
}