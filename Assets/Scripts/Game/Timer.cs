using System;
using TMPro;
using UnityEngine;
using Zenject;

public class Timer : MonoBehaviour
{

    private int Minutes => Mathf.FloorToInt(_levelData.SpentTimeSeconds / 60);
    private int Seconds => Mathf.FloorToInt(_levelData.SpentTimeSeconds % 60);
    
    [SerializeField] private TextMeshProUGUI timerText;

    private LevelData _levelData;

    public void Init(LevelData levelData)
    {
        _levelData = levelData;
    }

    private void Update()
    {
        if (_levelData == null)
            return;
        
        _levelData.SpentTimeSeconds += Time.deltaTime;
        UpdateText();
    }

    private void UpdateText()
    {
        timerText.text = Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }
}