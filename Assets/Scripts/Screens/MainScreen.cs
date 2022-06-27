using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : Screen
{
    [SerializeField] Text _mealTypeHeaderText;
    [SerializeField] ToggleDailyMenuButton _toggleDailyMenuButton;
    public override void Show()
    {
        base.Show();

        Meal _currentMeal = DBOperator.GetMeal(GlobalController.UserData);
        if(_currentMeal!=null)_mealTypeHeaderText.text = _currentMeal.Type;
        else _mealTypeHeaderText.text = "Дождитесь утра";

        _toggleDailyMenuButton.Toggle();
        _toggleDailyMenuButton.Toggle();
    }
}
