using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        ProfileData profile = DBOperator.GetUserViewData();

        _nameText.text = profile.Name;
        _loginText.text = profile.Login;
        _heightText.text = profile.Height.ToString();
        _weightText.text = profile.Weight.ToString();
        _dietText.text = profile.Diet;
        _goalText.text = profile.Goal;
        _allergenesText.text = "";
        foreach(string item in profile.AllergenesNames)
        {
            _allergenesText.text += item+" ";
        }

        if (profile.AllergenesNames.Count == 0) _allergenesText.text = "Отсутствуют";
    }

    public void LogOut()
    {
        PlayerPrefs.DeleteKey("user_id");
        GlobalController.UserData = new UserData();
        _uiController.ShowScreen(_uiController.LoginScreen);
    }
}


