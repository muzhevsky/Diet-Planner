using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreen : Screen
{
    [SerializeField] InputField _loginInput;
    [SerializeField] InputField _passwordInput;

    public override void Show()
    {
        base.Show();
        _loginInput.text = "";
        _passwordInput.text = "";
    }

    public void CheckLoginData()
    {
        if (_loginInput.text == "" ||
           _passwordInput.text == "")
        {
            AlertWrongInput();
            return;
        }

        string login = _loginInput.text;
        string password = _passwordInput.text;

        LoginInfo loginInfo = new LoginInfo();
        loginInfo.Login = login;
        loginInfo.Password = password;

        if (DBOperator.CheckLogin(loginInfo))
        {
            GlobalController.UserData = DBOperator.GetUserData();

            if(GlobalController.UserData.DietId!=0) _uiController.ShowScreen(_uiController.MainScreen);
            else _uiController.ShowScreen(_uiController.TestScreen);
        }
    }

    void AlertWrongInput()
    {
        print("somthing went wrong");
    }
}

public struct LoginInfo
{
    public string Login;
    public string Password;
}