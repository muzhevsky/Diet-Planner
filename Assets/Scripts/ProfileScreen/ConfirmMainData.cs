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
    [SerializeField] GlobalController _controller;
    public void Confirm()
    {
        if(_email.text!="" && _weight.text != "" && _height.text != "")
        {
            DBOperator dbOperator = new DBOperator();
            dbOperator.EditMainUserInfo(_email.text, int.Parse(_weight.text), int.Parse(_height.text));
            _window.SetActive(false);
            _uiController.ProfileScreen.SetInfoValues();
            _controller.AddWeight(int.Parse(_weight.text));
        }
    }

    private void OnEnable()
    {

        DBOperator dbOperator = new DBOperator();
        ProfileData userData = dbOperator.GetUserViewData();
        _email.text = userData.Login;
        _weight.text = userData.Weight.ToString();
        _height.text = userData.Height.ToString();
    }
}
