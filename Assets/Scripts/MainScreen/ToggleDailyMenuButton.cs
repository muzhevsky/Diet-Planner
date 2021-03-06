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
    [SerializeField] Text _buttonText;

    public void Toggle()
    {
        List<Meal> meal = DBOperator.GetAllMeal(GlobalController.UserData);

        for (int i = 0; i < _mealContainer.childCount; i++)
        {
            Destroy(_mealContainer.GetChild(i).gameObject);
        }
        foreach(Meal item in meal)
        {
            StartCoroutine(Delay(item));
        }
    }
    IEnumerator Delay(Meal meal)
    {
        _dailyMenu.SetActive(!_dailyMenu.activeSelf);
        if (_dailyMenu.activeSelf == false) _buttonText.text = "????";
        else _buttonText.text = "?????????";

        if (meal != null)
        {
            _mealType.text = meal.Type;
            _dateText.text = GlobalController.Day + " " + GlobalController.MonthNames[GlobalController.Month-1];
            foreach (Food food in meal.FoodList)
            {
                GameObject newCard = Instantiate(_mealPrefab, _mealContainer);
                newCard.GetComponent<FoodCardController>().SetTexts(food);
                newCard.GetComponent<FoodCardController>().Type.text = meal.Type;
            }
            yield return new WaitForEndOfFrame();
            _calendar.SetActive(!_calendar.activeSelf);
        }

        else _mealType.text = "????????????? ??????";
    }
}
