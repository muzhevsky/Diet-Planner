using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : Screen
{
    [SerializeField] Text _mealTypeHeaderText;
    public override void Show()
    {
        base.Show();
        DBOperator dbOperator = new DBOperator();
        Meal _currentMeal = dbOperator.GetMeal(_controller.UserData);
        _mealTypeHeaderText.text = _currentMeal?.Type;
    }
}
