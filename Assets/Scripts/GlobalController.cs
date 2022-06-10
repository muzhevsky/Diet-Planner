using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{
    [SerializeField] GlobalUiController _uiController;
    Meal _currentMeal;

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
        if (PlayerPrefs.HasKey("user_id"))
        {
            _uiController.ShowScreen(_uiController.MainScreen);
        }
    }
}
