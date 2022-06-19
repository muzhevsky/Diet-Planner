using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementContainer : MonoBehaviour
{
    [SerializeField] Text _name;
    [SerializeField] Text _description;

    public void SetAchievementView(Achievement achievement)
    {
        _name.text = achievement.Name;
        _description.text = achievement.Description;
    }
}
