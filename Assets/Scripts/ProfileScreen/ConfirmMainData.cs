using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmMainData : MonoBehaviour
{
    [SerializeField] InputField _email;
    [SerializeField] InputField _weight;
    [SerializeField] InputField _height;
    [SerializeField] GameObject _window;
    [SerializeField] GlobalUiController _uiController;
    public void Confirm()
    {
        if(_email.text!="" && _weight.text != "" && _height.text != "")
        {
            GlobalController.UserData.Weight = int.Parse(_weight.text);
            GlobalController.UserData.Height = int.Parse(_height.text);
            GlobalController.UserData.Login = _email.text;

            DBOperator.UpdateUserInfo(GlobalController.UserData);
            _uiController.ProfileScreen.SetInfoValues();
            _window.SetActive(false);
        }
    }

    private void OnEnable()
    {
        ProfileData userData = DBOperator.GetUserViewData();
        _email.text = userData.Login;
        _weight.text = userData.Weight.ToString();
        _height.text = userData.Height.ToString();
    }
}
