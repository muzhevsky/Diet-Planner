using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeightInputButton : Answer
{
    [SerializeField] InputField _weightInput;

    public void OnClick()
    {
        _testScreen.SetWeight(int.Parse(_weightInput.text));
        _testScreen.LoadNextQuestion();
    }
}
