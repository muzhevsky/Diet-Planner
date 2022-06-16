using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScreen : Screen
{
    [SerializeField] Text _header;
    [SerializeField] GameObject _foodCardPrefab;
    [SerializeField] Transform _foodCardContainer;
    [SerializeField] GameObject _warning;

    Meal _currentMeal;

    public override void Show()
    {
        base.Show();
        LoadMealData();
    }
    void LoadMealData()
    {
        DBOperator dbOperator = new DBOperator();
        _currentMeal = dbOperator.GetMeal(_controller.UserData);

        if (_currentMeal != null)
        {
            _warning.SetActive(false);
            _header.text = _currentMeal.Type;
            foreach (Food food in _currentMeal.FoodList)
            {
                FoodCardController cardController = Instantiate(_foodCardPrefab, _foodCardContainer).GetComponent<FoodCardController>();
                cardController.SetTexts(food);
            }
        }
        else
        {
            _warning.SetActive(true);
            _header.text = "";
            if (!_controller.UserData.HadBreakfastToday) _controller.UserData.HadBreakfastToday = true;
            else if (!_controller.UserData.HadLunchToday) _controller.UserData.HadLunchToday = true;
            else if (!_controller.UserData.HadSupperToday) _controller.UserData.HadSupperToday = true;
        }
    }
    public void OnEatButtonPush()
    {
        DBOperator dBOperator = new DBOperator();
        dBOperator.CompleteMeal(_currentMeal);
        ClearCardList();
        LoadMealData();
    }

    void ClearCardList()
    {
        for (int i = 0; i < _foodCardContainer.childCount; i++)
        {
            Destroy(_foodCardContainer.GetChild(i).gameObject);
        }
    }

    public void OnSkipButtonPush()
    {
        ClearCardList();
        LoadMealData();
    }
}
