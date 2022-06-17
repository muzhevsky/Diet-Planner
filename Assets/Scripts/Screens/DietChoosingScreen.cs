using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DietChoosingScreen : Screen
{
    [SerializeField] GameObject _dietInfoCardPrefab;
    [SerializeField] Transform _container;
    public override void Show()
    {
        base.Show();
        DBOperator dbOperator = new DBOperator();
        List<DietInfo> diets = dbOperator.GetDiets();
        foreach (DietInfo diet in diets)
        {
            DietInfoCard newInfoCard = GameObject.Instantiate(_dietInfoCardPrefab, _container).GetComponent<DietInfoCard>();
            newInfoCard.SetControllers(_controller, _uiController);
            newInfoCard.SetDietInfo(diet);
        }
    }
}
