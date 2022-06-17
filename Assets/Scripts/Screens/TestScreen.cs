using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreen : Screen
{
    [SerializeField] protected GameObject[] _testItems;
    [SerializeField] protected GameObject _backButton;

    protected AnswersList AnswerList;
    protected int pos = 0;

    private void Start()
    {
        AnswerList = new AnswersList();
    }
    public virtual void LoadNextQuestion()
    {
        if(pos < _testItems.Length-1)
        {
            _testItems[pos++].gameObject.SetActive(false);
            _testItems[pos].gameObject.SetActive(true);
            _backButton.SetActive(true);
        }
        else
        {
            DBOperator.AddTestInfo(AnswerList);
            _uiController.ShowScreen(_uiController.DietChoosingScreen);

            SetupUserData();
        }
    }
    protected void SetupUserData()
    {
        GlobalController.UserData.Weight = AnswerList.Weight;
        GlobalController.UserData.Height = AnswerList.Height;
        GlobalController.UserData.DesiredWeight = AnswerList.DesiredWeight;
        GlobalController.UserData.GenderId = AnswerList.Gender;
        GlobalController.UserData.Allergenes = AnswerList.Allergenes;
        GlobalController.UserData.GoalId = AnswerList.Goal;
        GlobalController.UserData.EatingFrequency = AnswerList.EatingFrequency;
        GlobalController.UserData.ActivityLevel = AnswerList.ActivityLevel;
    }
    public void LoadPrevQuestion()
    {

        if (pos == 0)
        {
            return;
        }

        _testItems[pos--].gameObject.SetActive(false);
        _testItems[pos].gameObject.SetActive(true);
        if (pos==0) _backButton.SetActive(false);
    }
    public void SetWeight(int weight)
    {
        AnswerList.Weight = weight;
    }
    public void SetHeight(int height)
    {
        AnswerList.Height = height;
    }
    public void SetDesiredWeight(int weight)
    {
        AnswerList.DesiredWeight = weight;
    }
    public void SetGoalId(Goal id)
    {
        AnswerList.Goal = id;
    }
    public void SetActivityLevelId(ActivityLevel id)
    {
        AnswerList.ActivityLevel = id;
    }
    public void SetEatingFrequency(EatingFrequency id)
    {
        AnswerList.EatingFrequency = id;
    }
    public void SetAllergenes(List<Allergenes> allergenes)
    {
        AnswerList.Allergenes = allergenes;
    }
    public void SetGenderId(Gender id)
    {
        AnswerList.Gender = id;
    }
}
