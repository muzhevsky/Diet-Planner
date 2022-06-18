using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;

public class ProfileScreen : Screen
{
    [SerializeField] Text _nameText;
    [SerializeField] Text _loginText;
    [SerializeField] Text _heightText;
    [SerializeField] Text _weightText;
    [SerializeField] Text _goalText;
    [SerializeField] Text _dietText;
    [SerializeField] Text _allergenesText;


    Meal _currentMeal;
    public override void Show()
    {
        base.Show();
        SetInfoValues();
    }
    public void SetInfoValues()
    {
        ProfileData profileData = DBOperator.GetUserViewData();

        _nameText.text = profileData.Name;
        _loginText.text = profileData.Login;
        _heightText.text = profileData.Height.ToString();
        _weightText.text = profileData.Weight.ToString();
        _dietText.text = profileData.Diet;
        _goalText.text = profileData.Goal;
        _allergenesText.text = "";
        foreach (string item in profileData.AllergenesNames)
        {
            _allergenesText.text += item + " ";
        }

        if (profileData.AllergenesNames.Count == 0) _allergenesText.text = "Отсутствуют";
    }

    public void LogOut()
    {
        PlayerPrefs.DeleteKey("user_id");
        GlobalController.UserData = new UserData();
        _uiController.ShowScreen(_uiController.LoginScreen);
    }
}


