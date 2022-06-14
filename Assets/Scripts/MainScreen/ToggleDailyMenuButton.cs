using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleDailyMenuButton : MonoBehaviour
{
    [SerializeField] GameObject _calendar;
    [SerializeField] GameObject _dailyMenu;

    public void Toggle()
    {
        _dailyMenu.SetActive(!_dailyMenu.activeSelf);
        _calendar.SetActive(!_calendar.activeSelf); 
    }
}
