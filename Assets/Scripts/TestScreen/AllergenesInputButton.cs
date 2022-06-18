using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllergenesInputButton : Answer
{
    [SerializeField] List<AllergenesAnswer> _toggles;
    public void OnClick()
    {
        GlobalController.UserData?.Allergenes.Clear();
        List<Allergenes> allergenesIds = new List<Allergenes>();
        foreach(AllergenesAnswer item in _toggles)
        {
            if(item.Toggle.isOn) allergenesIds.Add(item.AllergenesId);
        }
        _testScreen.SetAllergenes(allergenesIds);
        _testScreen.LoadNextQuestion();
    }
}
