using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class RegistrationScreen : Screen
{
    [SerializeField] InputField _loginInput;
    [SerializeField] InputField _passwordInput;
    [SerializeField] InputField _confirmPasswordInput;
    [SerializeField] InputField _phoneInput;
    [SerializeField] InputField _displayedNameInput;

    public void CheckRegistrationData()
    {
        if (_loginInput.text == "" ||
            _passwordInput.text == "" ||    
            _confirmPasswordInput.text == "" ||
            _phoneInput.text == "" ||
            _displayedNameInput.text == "")
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
            registrationInfo.DisplayedName = _displayedNameInput.text;

            if (DBOperator.AddUserToDB(registrationInfo))
            {
                _uiController.ShowScreen(_uiController.TestScreen);
                GlobalController.UserData.Login = registrationInfo.Login;
                GlobalController.UserData.PhoneNumber = registrationInfo.Phone;
                GlobalController.UserData.Name = registrationInfo.DisplayedName;
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
    public string DisplayedName;
}
