using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreen : Screen
{
    [SerializeField] InputField _loginInput;
    [SerializeField] InputField _passwordInput;

    DBOperator dbOperator;
    
    
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

        DBOperator dbOperator = new DBOperator();
        if (dbOperator.CheckLogin(loginInfo))
        {
            _controller.UserData = dbOperator.GetUserData();

            if(_controller.UserData.DietId!=0) _uiController.ShowScreen(_uiController.MainScreen);
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