using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAnyFood : MonoBehaviour
{
    public Product Product;
    [SerializeField] MyFoodScreen _foodScreen;
    [SerializeField] InputField _name;
    [SerializeField] InputField _amount;
    [SerializeField] Text _measure;

    private void Start()
    {
        Product = new Product();
    }
    public void AddIngredients()
    {
        if(_name.text!="" && _amount.text != "")
        {
            Product.Name = _name.text;
            Product.Amount = int.Parse(_amount.text);
            DBOperator.AddProduct(Product);
            _foodScreen.ReloadData();
            _name.text = "";
            _amount.text = "";
            _foodScreen.HideAddFoodWindow();
        }
    }

    void OnEnable()
    {
        _name.text = Product?.Name;
        _amount.text = Product?.Amount.ToString();
        _measure.text = Product?.Measure;
    }
}
