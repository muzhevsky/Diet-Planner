using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScreen : Screen
{
    [SerializeField] GameObject[] _testItems;
    [SerializeField] GameObject _backButton;
    
    AnswersList AnswerList;
    int pos = 0;

    private void Start()
    {
        AnswerList = new AnswersList();
    }
    public void LoadNextQuestion()
    {
        if(pos < _testItems.Length-1)
        {
            _testItems[pos++].gameObject.SetActive(false);
            _testItems[pos].gameObject.SetActive(true);
            _backButton.SetActive(true);
        }
        else
        {
            Debug.Log(AnswerList.Gender);
            Debug.Log(AnswerList.Goal);
            Debug.Log(AnswerList.Height);
            Debug.Log(AnswerList.Weight);
            Debug.Log(AnswerList.DesiredWeight);
            foreach(Allergenes item in AnswerList.Allergenes)
            {
                Debug.Log(item);
            }
            Debug.Log(AnswerList.EatingFrequency);
            Debug.Log(AnswerList.ActivityLevel);

            DBOperator dBOperator = new DBOperator();
            dBOperator.AddTestInfo(PlayerPrefs.GetInt("user_id"),AnswerList);
        }
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
