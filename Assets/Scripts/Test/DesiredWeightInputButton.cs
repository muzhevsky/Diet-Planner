using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DesiredWeightInputButton : Answer
{
    [SerializeField] InputField _desiredWeightInput;

    public void OnClick()
    {
        _testScreen.SetDesiredWeight(int.Parse(_desiredWeightInput.text));
        _testScreen.LoadNextQuestion();
    }
}
