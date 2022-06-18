using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreen : Screen
{
    [SerializeField] protected GameObject[] _testItems;
    [SerializeField] protected GameObject _backButton;

    protected AnswersList AnswerList;
    protected int pos = 0;

    public void RestartTest()
    {
        foreach (GameObject item in _testItems) item.SetActive(false);
        _testItems[0].SetActive(true);
        pos = 0;
    }

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
            SetupUserData();
            DBOperator.UpdateWeights(GlobalController.Month);

            _uiController.ShowScreen(_uiController.DietChoosingScreen);
        }
    }
    protected void SetupUserData()
    {
        if(AnswerList.Weight!=0) GlobalController.UserData.Weight = AnswerList.Weight;
        if (AnswerList.Height != 0) GlobalController.UserData.Height = AnswerList.Height;
        if (AnswerList.DesiredWeight != 0) GlobalController.UserData.DesiredWeight = AnswerList.DesiredWeight;
        if (AnswerList.Gender != 0) GlobalController.UserData.GenderId = AnswerList.Gender;
        if (AnswerList.Allergenes.Count != 0) GlobalController.UserData.Allergenes = AnswerList.Allergenes;
        if (AnswerList.Goal != 0) GlobalController.UserData.GoalId = AnswerList.Goal;
        if (AnswerList.EatingFrequency != 0) GlobalController.UserData.EatingFrequency = AnswerList.EatingFrequency;
        if (AnswerList.ActivityLevel != 0) GlobalController.UserData.ActivityLevel = AnswerList.ActivityLevel;
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
