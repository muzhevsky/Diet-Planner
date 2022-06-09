using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityLevelAnswer : Answer
{
    [SerializeField] ActivityLevel _activityLevel;

    public void OnClick()
    {
        _testScreen.SetActivityLevelId(_activityLevel);
        _testScreen.LoadNextQuestion();
    }
}
