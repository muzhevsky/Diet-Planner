using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graphic : MonoBehaviour
{
    [SerializeField] Text[] _horizontal;
    [SerializeField] Text[] _vertical;
    [SerializeField] GlobalController _controller;
    [SerializeField] float _offset;

    int maxWeight;
    int minWeight;
    int weightStep;
    int weightLimit;
    float k;

    [SerializeField] GameObject _testObject;
    RectTransform rectTransform;
    Vector2[] _points;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void DrawGraph(GraphicInfo graphicInfo)
    {
        ClearGraph();
        SetupVerticalAxis(graphicInfo.LastWeights);
        SetupHorizontalAxis(graphicInfo.MonthNumber);
        
        _points = new Vector2[6];
        k = rectTransform.sizeDelta.y*6/7/(float)weightLimit;
        for (int i = 0; i < _points.Length; i++)
        {
            float temp = (float)graphicInfo.LastWeights[i] - (float)minWeight;
            _points[i].y = temp*k+_offset;
            _points[i].x = rectTransform.sizeDelta.x*i/7+_offset;
            GameObject newPoint = Instantiate(_testObject, this.transform);
            RectTransform newPointRT = ((RectTransform)newPoint.transform);
            newPointRT.anchorMin = new Vector2(0, 0);
            newPointRT.anchoredPosition = _points[i];
        }

        for(int i = 0; i < _points.Length-1; i++)
        {
            GameObject newLine = Instantiate(_testObject, this.transform);
            RectTransform newLineRT = ((RectTransform)newLine.transform);
            newLineRT.anchorMin = new Vector2(0, 0);
            newLineRT.sizeDelta = new Vector2(Mathf.Sqrt(Mathf.Pow((_points[i+1].x-_points[i].x),2)+ Mathf.Pow((_points[i + 1].y - _points[i].y), 2)), newLineRT.sizeDelta.y);
            newLineRT.pivot = new Vector2(0, 0.5f);
            newLineRT.anchoredPosition = new Vector2(_points[i].x+7.5f,_points[i].y + 7.5f);
            newLine.transform.Rotate(0, 0, 57.3f*Mathf.Atan((_points[i + 1].y - _points[i].y) / (_points[i + 1].x - _points[i].x)));
        }
    }
    void ClearGraph()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    void SetupVerticalAxis(int[] weights)
    {
        maxWeight = weights[0];
        minWeight = weights[0];
        for (int i = 0; i < weights.Length; i++)
        {
            if (maxWeight < weights[i]) maxWeight = weights[i];
            if (minWeight > weights[i]) minWeight = weights[i];
        }
        weightLimit = maxWeight - minWeight;
        weightStep = weightLimit / (weights.Length-1);

        for (int i = 0; i < _vertical.Length; i++)
        {
            _vertical[i].text = (minWeight + weightStep * i).ToString();
        }
    }

    void SetupHorizontalAxis(int MonthNumber)
    {
        MonthNumber += 7;
        int i = 0;
        while(i < 6)
        {
            _horizontal[i].text = _controller.MonthNames[(MonthNumber++)%12].Substring(0,3).ToString();
            i++;
        }
    }

}