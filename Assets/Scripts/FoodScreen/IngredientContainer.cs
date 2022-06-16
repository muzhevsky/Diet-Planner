using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientContainer : MonoBehaviour
{
    [SerializeField] Text _name;
    [SerializeField] Text _amount;
    [SerializeField] Text _measure;

    [SerializeField] AddAnyFood _addFoodButton;
    Product _product;
    MyFoodScreen _foodScreen;
    
    public void ShowFoodWindow()
    {
        _addFoodButton.Product = _product;
        _foodScreen.ShowAddFoodWindow();
    }
    public void Init(Product product)
    {
        _name.text = product.Name;
        _amount.text = product.Amount.ToString();
        _measure.text = product.Measure.ToString();
        _product = product;
    }
    public void SetFoodScreen(MyFoodScreen screen)
    {
        _foodScreen = screen;
    }
    public void SetConfirmButton(AddAnyFood confirmButton)
    {
        _addFoodButton = confirmButton;
    }
}
