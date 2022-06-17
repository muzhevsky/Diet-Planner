using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graphic : MonoBehaviour
{
    [SerializeField] Text[] _horizontal;
    [SerializeField] Text[] _vertical;
    [SerializeField] float _offset;

    int? maxWeight;
    int? minWeight;
    int? verticalStep;

    float _scale;

    const int FULL_VERTICAL_LENGTH = 7;
    const int FULL_HORIZONTAL_LENGTH = 7;
    const float RADIAN = 57.3f;

    float _actualVerticalLength;
    [SerializeField] GameObject _testObject;
    RectTransform rectTransform;
    List<Vector2> _points;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        _actualVerticalLength = rectTransform.sizeDelta.y * (FULL_VERTICAL_LENGTH - 1) / FULL_VERTICAL_LENGTH;
    }
    public void DrawGraph(GraphicInfo graphicInfo)
    {
        ClearGraph();
        SetupVerticalAxis(graphicInfo.LastWeights);
        SetupHorizontalAxis(graphicInfo.MonthNumber);

        for (int i = 0; i < graphicInfo.LastWeights.Length; i++)
        {
            if(graphicInfo.LastWeights[i] != null)
            {
                _points = new List<Vector2>();
                _scale = _actualVerticalLength / (float)(maxWeight - minWeight);

                for (int j = 0; j < graphicInfo.LastWeights.Length; j++)
                {
                    if (graphicInfo.LastWeights[j] == null) continue;
                    float temp = (float)graphicInfo.LastWeights[j] - (float)minWeight;
                    _points.Add(new Vector2(rectTransform.sizeDelta.x * j / FULL_VERTICAL_LENGTH + _offset, temp * _scale + _offset));
                    GameObject newPoint = Instantiate(_testObject, this.transform);
                    RectTransform newPointRT = ((RectTransform)newPoint.transform);
                    newPointRT.anchorMin = new Vector2(0, 0);
                    newPointRT.anchoredPosition = _points[_points.Count - 1];
                }

                for (int j = 0; j < _points.Count - 1; j++)
                {
                    GameObject newLine = Instantiate(_testObject, this.transform);
                    RectTransform newLineRT = ((RectTransform)newLine.transform);
                    newLineRT.anchorMin = new Vector2(0, 0);
                    newLineRT.sizeDelta = new Vector2(Mathf.Sqrt(Mathf.Pow((_points[j + 1].x - _points[j].x), 2) + Mathf.Pow((_points[j + 1].y - _points[j].y), 2)), newLineRT.sizeDelta.y);
                    newLineRT.pivot = new Vector2(0, 0.5f);

                    float anchoredOffset = ((RectTransform)_testObject.transform).sizeDelta.y / 2;
                    newLineRT.anchoredPosition = new Vector2(_points[j].x + anchoredOffset, _points[j].y + anchoredOffset);
                    newLine.transform.Rotate(0, 0, RADIAN * Mathf.Atan((_points[j + 1].y - _points[j].y) / (_points[j + 1].x - _points[j].x)));
                }
            }
        }
        
    }
    void ClearGraph()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    void SetupVerticalAxis(int?[] weights)
    {
        for(int i = 0; i < weights.Length; i++)
        {
            if(weights[i] != null)
            {
                maxWeight = weights[i];
                minWeight = weights[i];
            }
        }
        if (minWeight == null) return;
        for (int i = 0; i < weights.Length; i++)
        {
            if (maxWeight < weights[i]) maxWeight = weights[i];
            if (minWeight > weights[i]) minWeight = weights[i];
        }
        verticalStep = (maxWeight - minWeight) / (weights.Length-1);

        for (int i = 0; i < _vertical.Length; i++)
        {
            _vertical[i].text = (minWeight + verticalStep * i).ToString();
        }
    }

    void SetupHorizontalAxis(int MonthNumber)
    {
        MonthNumber += 7;
        int i = 0;
        while(i < 6)
        {
            _horizontal[i].text = GlobalController.MonthNames[(MonthNumber++)%12].Substring(0,3).ToString();
            i++;
        }
    }

}