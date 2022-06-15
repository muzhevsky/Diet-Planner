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

    Meal _currentMeal;
    public UserData UserData;
    public void GetCurrentMeal(Meal currentMeal)
    {
        _currentMeal = currentMeal;
    }

    public void OnEatButtonPush()
    {
        DBOperator dBOperator = new DBOperator();
        dBOperator.CompleteMeal(_currentMeal);
    }
    public void OnSkipButtonPush()
    {

    }
    private void Awake()
    {
        //_achievementController = new AchievementController();
        Year = DateTime.Now.Year;
        Month = DateTime.Now.Month;
        Year = DateTime.Now.Year;

        if (PlayerPrefs.HasKey("user_id"))
        {
            _uiController.ShowScreen(_uiController.MainScreen);
        }

    }

    public void AddWeight(int weight)
    {
        //_achievementController.CheckForWeightAchievements(weight - LastWeights[DateTime.Today.Month - 1]);
        //LastWeights[DateTime.Today.Month - 1] = weight;
        _uiController.GraphicScreen.UpdateLastWeight(weight);
    }
}
