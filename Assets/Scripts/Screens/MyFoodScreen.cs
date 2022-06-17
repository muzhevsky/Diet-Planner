using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFoodScreen : Screen
{
    [SerializeField] GameObject _userIngredientsPrefab;
    [SerializeField] Transform _container;
    [SerializeField] GameObject _addFoodWindow;
    [SerializeField] AddAnyFood _addFoodButton;


    public override void Show()
    {
        base.Show();
        ReloadData();
    }
    public void ReloadData()
    {
        for(int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i).gameObject);
        }
        DBOperator dbOperator = new DBOperator();
        List<Product> products = dbOperator.GetUserProducts();
        foreach (Product item in products)
        {
            IngredientContainer ingredientContainer = Instantiate(_userIngredientsPrefab, _container).GetComponent<IngredientContainer>();
            ingredientContainer.SetFoodScreen(this);
            ingredientContainer.SetConfirmButton(_addFoodButton);
            ingredientContainer.SetProduct(item);
            ingredientContainer.Init(item);
        }
    }
    public void ShowAddFoodWindow()
    {
        _addFoodWindow.SetActive(true);
    }
    public void HideAddFoodWindow()
    {
        _addFoodWindow.SetActive(false);
    }
    public override void Hide()
    {
        base.Hide();
    }
}
