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
            DBOperator dBOperator = new DBOperator();
            dBOperator.EditAdditionalUserInfo(AnswerList.Goal, AnswerList.Allergenes);
            _uiController.ShowScreen(_uiController.MainScreen);
        }
    }
}
