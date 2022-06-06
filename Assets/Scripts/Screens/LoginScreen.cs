using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class LoginScreen : Screen
{
    [SerializeField] Text _loginInput;
    [SerializeField] Text _passwordInput;

    public void CheckLoginData()
    {
        if(_loginInput.text == "" || 
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

        string json = JsonConvert.SerializeObject(loginInfo);
        print(json);

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
