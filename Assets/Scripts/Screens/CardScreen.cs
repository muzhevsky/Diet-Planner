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
    [SerializeField] GameObject _ateButton;
    [SerializeField] GameObject _skippedButton;

    Meal _currentMeal;

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
            _ateButton.gameObject.SetActive(true);
            _skippedButton.gameObject.SetActive(true);
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
            _ateButton.gameObject.SetActive(false);
            _skippedButton.gameObject.SetActive(false);
        }
    }
    public void OnEatButtonPush()
    {
        DBOperator.CompleteMeal(_currentMeal);
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
                    DBOperator.SetBreakfastState(1);
                    break;
                case "Обед":
                    DBOperator.SetLunchState(1);
                    break;
                case "Ужин":
                    DBOperator.SetSupperState(1);
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
        _currentMeal = DBOperator.GetMeal(GlobalController.UserData);
    }
}
