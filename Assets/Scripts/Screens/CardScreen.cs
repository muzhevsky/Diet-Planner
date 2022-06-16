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

    DBOperator dbOperator;
    Meal _currentMeal;

    private void Start()
    {
        dbOperator = new DBOperator();
    }

    public override void Show()
    {
        base.Show();
        SetupCurrentMeal();
        LoadMealData();
    }
    void LoadMealData()
    {
        ClearCardList();
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
        }
    }
    public void OnEatButtonPush()
    {
        dbOperator.CompleteMeal(_currentMeal);
        MarkMealAsDone();
        SetupCurrentMeal();
        LoadMealData();
    }

    void ClearCardList()
    {
        for (int i = 0; i < _foodCardContainer.childCount; i++)
        {
            Destroy(_foodCardContainer.GetChild(i).gameObject);
        }
    }

    void MarkMealAsDone()
    {
        if(_currentMeal != null)
        {
            switch (_currentMeal.Type)
            {
                case "Завтрак":
                    _controller.UserData.HadBreakfastToday = true;
                    break;
                case "Обед":
                    _controller.UserData.HadLunchToday = true;
                    break;
                case "Ужин":
                    _controller.UserData.HadSupperToday = true;
                    break;
            }
        }
    }
    public void OnSkipButtonPush()
    {
        MarkMealAsDone();
        SetupCurrentMeal();
        LoadMealData();
    }

    void SetupCurrentMeal()
    {
        _currentMeal = dbOperator.GetMeal(_controller.UserData);
    }
}
