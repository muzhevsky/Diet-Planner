using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodAmountInput : MonoBehaviour
{
    InputField _input;
    [SerializeField] AddAnyFood _addFoodButton;
    private void Start()
    {
        _input = GetComponent<InputField>();
    }

    public void SetAmount()
    {
        _addFoodButton.Product.Amount = int.Parse(_input.text);
        if (_addFoodButton.Product.Amount < 0) _addFoodButton.Product.Amount *= -1;
    }
}
