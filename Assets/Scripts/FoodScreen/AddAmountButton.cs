using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAmountButton : MonoBehaviour
{
    [SerializeField] IngredientContainer _ingredientContainer;
    public void ShowIngredientInput()
    {
        _ingredientContainer.ShowFoodWindow();
    }
}