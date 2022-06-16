using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchProductInput : MonoBehaviour
{
    InputField _input;
    [SerializeField] Text _measure;
    [SerializeField] GlobalController _controller;
    [SerializeField] AddAnyFood _addFoodButton;
    private void Start()
    {
        _input = GetComponent<InputField>();
    }

    public void SearchProduct()
    {
        DBOperator dbOperator = new DBOperator();
        List<Product> products = dbOperator.GetAllDietProducts(_controller.UserData);
        
        foreach(Product product in products)
        {
            if (_input.text!=""  && product.Name.Contains(_input.text.ToLower().ToLower().Substring(0, _input.text.Length))){
                _measure.text = product.Measure;
                _input.text = product.Name;
                _addFoodButton.Product.Name = product.Name;
                break;
            }
        }
    }
}