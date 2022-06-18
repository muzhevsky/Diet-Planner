using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditAdditionalDataScreen : TestScreen
{
    public override void LoadNextQuestion()
    {
        if (pos < _testItems.Length - 1)
        {
            _testItems[pos++].gameObject.SetActive(false);
            _testItems[pos].gameObject.SetActive(true);
            _backButton.SetActive(true);
        }
        else
        {
            SetupUserData();
            DBOperator.UpdateUserInfo(GlobalController.UserData);
            _uiController.ShowScreen(_uiController.DietChoosingScreen);
        }
    }
}
