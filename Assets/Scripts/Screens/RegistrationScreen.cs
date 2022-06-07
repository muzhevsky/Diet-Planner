using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class RegistrationScreen : Screen
{
    [SerializeField] InputField _loginInput;
    [SerializeField] InputField _passwordInput;
    [SerializeField] InputField _confirmPasswordInput;
    [SerializeField] InputField _phoneInput;

    public void CheckRegistrationData()
    {
        if (_loginInput.text == "" ||
            _passwordInput.text == "" ||    
            _confirmPasswordInput.text == "" ||
            _phoneInput.text == "")
        {
            AlertWrongInput();
            return;
        }
        if(_passwordInput.text == _confirmPasswordInput.text)
        {
            RegistrationInfo registrationInfo = new RegistrationInfo();
            registrationInfo.Login = _loginInput.text;
            registrationInfo.Password = _passwordInput.text;
            registrationInfo.Phone = _phoneInput.text;

            DBOperator dbOperator = new DBOperator();
            if (dbOperator.AddUserToDB(registrationInfo))
            {
                _uiController.ShowScreen(_uiController.TestScreen);
            }
        }
    }

    void AlertWrongInput()
    {
        print("somthing went wrong");
    }
}

public struct RegistrationInfo
{
    public string Login;
    public string Password;
    public string Phone;
}
