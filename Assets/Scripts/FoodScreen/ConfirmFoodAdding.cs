using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmFoodAdding : MonoBehaviour
{
    public Product Product;
    [SerializeField] MyFoodScreen _foodScreen;
    [SerializeField] InputField _name;
    [SerializeField] InputField _amount;

    public void AddIngredients()
    {
        DBOperator dbOperator = new DBOperator();
        Product.Amount += int.Parse(_amount.text);
        dbOperator.AddProduct(Product);
        _foodScreen.ReloadData();
        _foodScreen.HideAddFoodWindow();
    }

    public void Init()
    {
        _name.text = Product.Name;
    }
}
