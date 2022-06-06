using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
public class RegistrationScreen : Screen
{
    [SerializeField] Text _loginInput;
    [SerializeField] Text _passwordInput;
    [SerializeField] Text _confirmPasswordInput;
    [SerializeField] Text _emailInput;

    public void CheckLoginData()
    {
        if (_loginInput.text == "" ||
            _passwordInput.text == "" ||
            _confirmPasswordInput.text == "" ||
            _emailInput.text == "")
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
