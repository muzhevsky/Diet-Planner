using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingFrequencyAnswer : Answer
{
    [SerializeField] EatingFrequency _eatingFrequency;

    public void OnClick()
    {
        _testScreen.SetEatingFrequency(_eatingFrequency);
        _testScreen.LoadNextQuestion();
    }
}
