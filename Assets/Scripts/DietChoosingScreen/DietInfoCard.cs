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
    [SerializeField] GlobalController _controller;
    public void SelectDiet()
    {
        DBOperator dbOperator = new DBOperator();
        dbOperator.SetDiet(_dietInfo);
        _controller.UserData.DietId = _dietInfo.Id;
        _uiController.ShowScreen(_uiController.MainScreen);
    }
    public void SetDietInfo(DietInfo info)
    {
        _dietInfo = info;
        _name.text = info.Name;
        _description.text = info.Description;
    }
    public void SetControllers(GlobalController controller, GlobalUiController globalUiController)
    {
        _controller = controller;
        _uiController = globalUiController;
    }
}
