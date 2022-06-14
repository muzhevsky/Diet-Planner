using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientContainer : MonoBehaviour
{
    [SerializeField] Text _name;
    [SerializeField] Text _amount;

    [SerializeField] ConfirmFoodAdding _confirmButton;
    Product _product;
    MyFoodScreen _foodScreen;
    
    public void ShowFoodWindow()
    {
        _confirmButton.Product = _product;
        _foodScreen.ShowAddFoodWindow();
        _confirmButton.Init();
    }
    public void Init(Product product)
    {
        _name.text = product.Name;
        _amount.text = product.Amount.ToString();
        _product = product;
        _confirmButton.Product = _product;
    }
    public void SetFoodScreen(MyFoodScreen screen)
    {
        _foodScreen = screen;
    }
    public void SetConfirmButton(ConfirmFoodAdding confirmButton)
    {
        _confirmButton = confirmButton;
    }
}
