using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/FoodCardInfo")]
public class FoodCard : ScriptableObject
{
    public string FoodName;
    public int Calories;
    public int Proteins;
    public int Fats;
    public int Carbohydrates;
    public string Recipe;
}
