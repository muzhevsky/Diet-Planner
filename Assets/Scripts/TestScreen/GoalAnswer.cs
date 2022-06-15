using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnswer : Answer
{
    public Goal _goalId;
    public void OnClick()
    {
        _testScreen.SetGoalId(_goalId);
        _testScreen.LoadNextQuestion();
    }
}
