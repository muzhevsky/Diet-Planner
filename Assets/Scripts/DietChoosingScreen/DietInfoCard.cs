using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DietInfoCard : MonoBehaviour
{
    DietInfo _dietInfo;
    [SerializeField] Text _name;
    [SerializeField] Text _description;
    [SerializeField] GlobalUiController _uiController;
    public void SelectDiet()
    {
        DBOperator.SetDiet(_dietInfo);
        GlobalController.UserData.DietId = _dietInfo.Id;
        _uiController.ShowScreen(_uiController.MainScreen);
    }
    public void SetDietInfo(DietInfo info)
    {
        _dietInfo = info;
        _name.text = info.Name;
        _description.text = info.Description;
    }
    public void SetController(GlobalUiController globalUiController)
    {
        _uiController = globalUiController;
    }
}
