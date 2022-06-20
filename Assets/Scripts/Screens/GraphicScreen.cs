using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicScreen : Screen
{
    [SerializeField] MyGraphic _graphic;
    GraphicInfo _graphicInfo;
    private void Awake()
    {
        _graphicInfo = new GraphicInfo();
        _graphicInfo.LastWeights = new int[6];
        for(int i = 0; i < _graphicInfo.LastWeights[i];i++) _graphicInfo.LastWeights[i] = 0;
    }
    public override void Show()
    {
        base.Show();
        _graphicInfo.MonthNumber = GlobalController.Month - 1;
        _graphicInfo.LastWeights = DBOperator.GetLastWeights();
        _graphic.DrawGraph(_graphicInfo);
    }
    public void UpdateLastWeight(int weight)
    {
        _graphicInfo.LastWeights[_graphicInfo.LastWeights.Length-1] = weight;
    }
}
