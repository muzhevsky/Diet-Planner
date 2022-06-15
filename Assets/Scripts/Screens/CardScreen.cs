using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScreen : Screen
{
    [SerializeField] Text _header;
    [SerializeField] GameObject _foodCardPrefab;
    [SerializeField] Transform _foodCardContainer;
    Meal _currentMeal;

    public override void Show()
    {
        base.Show();
        DBOperator dbOperator = new DBOperator();
        _currentMeal = dbOperator.GetMeal(_controller.UserData);

        _header.text = _currentMeal.Type;
        foreach(Food food in _currentMeal.FoodList)
        {
            FoodCardController cardController = Instantiate(_foodCardPrefab, _foodCardContainer).GetComponent<FoodCardController>();
            cardController.SetTexts(food);
        }
    }
}
