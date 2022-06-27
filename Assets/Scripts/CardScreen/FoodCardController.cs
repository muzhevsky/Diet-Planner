using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodCardController : MonoBehaviour
{
    [SerializeField] Text _name;
    [SerializeField] Text _calories;
    [SerializeField] Text _proteins;
    [SerializeField] Text _fats;
    [SerializeField] Text _carbohydrates;
    [SerializeField] Text _ingredients;
    [SerializeField] Text _recipe;

    public Text Type;
    public void SetTexts(Food food)
    {
        _name.text = food.Name;
        _calories.text = "Калорийность: " + food.Calories.ToString()+" ккал.";
        _proteins.text = "Белки: " + food.Proteins.ToString() + " г.";
        _fats.text = "Жиры: "+food.Fats.ToString() + " г.";
        _carbohydrates.text = "Углеводы: " + food.Carbohydrates.ToString() + " г.";
        _recipe.text = food.Recipe.ToString();

        foreach(Product item in food.Ingredients)
        {
            _ingredients.text += item.Name + " " + item.Amount.ToString() + " " + item.Measure;
        }
    }
}
