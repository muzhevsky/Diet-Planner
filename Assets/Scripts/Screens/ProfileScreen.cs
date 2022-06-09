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

    public override void Show()
    {
        base.Show();
        DBOperator dBOperator = new DBOperator();
        UserData userData = dBOperator.GetUserData();
        _nameText.text = userData.Name;
        _loginText.text = userData.Login;
        _heightText.text = userData.Height.ToString();
        _weightText.text = userData.Weight.ToString();
        _goalText.text = userData.Goal;

        if (userData.Allergenes_id == 1) _allergenesText.text = "Отсутствуют";
        else _allergenesText.text = "";
    }
}


public struct UserData
{
    public string Name;
    public string Login;
    public int Weight;
    public int Height;
    public string Goal;
    public string Diet;
    public int Allergenes_id;
}