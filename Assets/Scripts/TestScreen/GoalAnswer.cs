using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnswer : Answer
{
    [SerializeField] Goal _goalId;
    public void OnClick()
    {
        _testScreen.SetGoalId(_goalId);
        _testScreen.LoadNextQuestion();
    }
}
