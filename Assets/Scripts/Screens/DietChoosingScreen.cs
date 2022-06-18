using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DietChoosingScreen : Screen
{
    [SerializeField] GameObject _dietInfoCardPrefab;
    [SerializeField] Transform _container;
    [SerializeField] Text _header;
    [SerializeField] GameObject _restartTest;
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

        if (diets.Count == 0)
        {
            _header.text = "По вашим данным диеты не найдены";
            _restartTest.SetActive(true);
        }
        else
        {
            _restartTest.SetActive(false);
            _header.text = "Подходящие диеты:";
        } 
    }
}
