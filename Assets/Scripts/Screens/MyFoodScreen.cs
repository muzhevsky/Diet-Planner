using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFoodScreen : Screen
{
    [SerializeField] GameObject _userIngredientsPrefab;
    [SerializeField] Transform _container;
    [SerializeField] GameObject _addFoodWindow;
    [SerializeField] ConfirmFoodAdding _confirmButton;

    bool isLoaded;

    private void Start()
    {
        isLoaded = false;
    }
    public override void Show()
    {
        base.Show();
        if (!isLoaded)
        {
            ReloadData();
            isLoaded = true;
        }
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
            IngredientContainer ingredient = Instantiate(_userIngredientsPrefab, _container).GetComponent<IngredientContainer>();
            ingredient.Init(item);
            ingredient.SetFoodScreen(this);
            ingredient.SetConfirmButton(_confirmButton);
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
