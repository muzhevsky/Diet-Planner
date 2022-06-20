using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AchievementsScreen : Screen
{
    [SerializeField] Transform _container;
    [SerializeField] GameObject _achievementPrefab;
    [SerializeField] GameObject _alert;
    List<Achievement> list;

    public void Init()
    {
        list = DBOperator.GetAchievements();
    }

    public override void Show()
    {
        base.Show();
        for(int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i).gameObject);
        }

        list = DBOperator.GetAchievements();
        foreach (Achievement achievement in list)
        {
            AchievementContainer achievementContainer = Instantiate(_achievementPrefab, _container).GetComponent<AchievementContainer>();
            achievementContainer.SetAchievementView(achievement);
        }
    }

    public void CheckWeightAchievements()
    {
        int[] lastWeights = DBOperator.GetLastWeights();
        if (GlobalController.Month == 1)
        {
            if (lastWeights[11] - GlobalController.UserData.Weight > 2 && !GlobalController.UserData.AchievementsIds.Contains(Achievements.Lose2)) 
            { 
                GlobalController.UserData.AchievementsIds.Add(Achievements.Lose2);
                ShowAlert();
            }
            if (lastWeights[11] - GlobalController.UserData.Weight  > 5 && !GlobalController.UserData.AchievementsIds.Contains(Achievements.Lose5)) 
            { 
                GlobalController.UserData.AchievementsIds.Add(Achievements.Lose5);
                ShowAlert();
            }
        }
        else
        {
            if (lastWeights[GlobalController.Month - 2] - GlobalController.UserData.Weight >= 2 && !GlobalController.UserData.AchievementsIds.Contains(Achievements.Lose2)) 
            {
                GlobalController.UserData.AchievementsIds.Add(Achievements.Lose2);
                ShowAlert();

            }
            if (lastWeights[GlobalController.Month - 2] - GlobalController.UserData.Weight >= 5 && !GlobalController.UserData.AchievementsIds.Contains(Achievements.Lose5)) 
            {
                GlobalController.UserData.AchievementsIds.Add(Achievements.Lose5);
                ShowAlert();
            }
        }
        DBOperator.UpdateUserInfo(GlobalController.UserData);
    }

    void ShowAlert()
    {
        if (!_alert.activeSelf)
        {
            _alert.SetActive(true);
            _alert.GetComponent<CanvasGroup>().DOFade(0, 2f).OnComplete(() => { _alert.SetActive(false); });
        }
    }
}