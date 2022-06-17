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
        List<DietInfo> diets = DBOperator.GetDiets();
        foreach (DietInfo diet in diets)
        {
            DietInfoCard newInfoCard = GameObject.Instantiate(_dietInfoCardPrefab, _container).GetComponent<DietInfoCard>();
            newInfoCard.SetController(_uiController);
            newInfoCard.SetDietInfo(diet);
        }
    }
}
