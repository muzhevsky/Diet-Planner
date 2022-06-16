using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDailyMenuButton : MonoBehaviour
{
    [SerializeField] GameObject _calendar;
    [SerializeField] GameObject _dailyMenu;
    [SerializeField] GameObject _mealPrefab;
    [SerializeField] Transform _mealContainer;
    [SerializeField] Text _dateText;
    [SerializeField] Text _mealType;
    [SerializeField] GlobalController _controller;
    public void Toggle()
    {
        DBOperator dbOperator = new DBOperator();
        Meal meal = dbOperator.GetMeal(_controller.UserData);

        for(int i = 0; i < _mealContainer.childCount; i++)
        {
            Destroy(_mealContainer.GetChild(i).gameObject);
        }

        StartCoroutine(Delay(meal));
    }
    IEnumerator Delay(Meal meal)
    {
        _dailyMenu.SetActive(!_dailyMenu.activeSelf);
        _mealType.text = meal.Type;
        _dateText.text = _controller.Day + " " + _controller.MonthNames[_controller.Month];
        foreach (Food food in meal.FoodList)
        {
            GameObject newCard = Instantiate(_mealPrefab, _mealContainer);
            newCard.GetComponent<FoodCardController>().SetTexts(food);
        }
        yield return new WaitForEndOfFrame();
        _calendar.SetActive(!_calendar.activeSelf);
    }
}
