using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddExistingFood : MonoBehaviour
{
    [SerializeField] IngredientContainer _ingredientContainer;
    public Product Product;
    public void ShowIngredientInput()
    {
        _ingredientContainer.ShowFoodWindow();
    }
}