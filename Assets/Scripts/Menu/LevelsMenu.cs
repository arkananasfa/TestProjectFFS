using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelsMenu : MonoBehaviour
{
    
    [SerializeField] private LevelsFrame levelsFramePrefab;
    
    [SerializeField] private ScrollRect scroll;
    [SerializeField] private Transform framesContainer;

    [SerializeField] private int levelsCountInFrame;
    
    public void Init(LevelData[] levels, DiContainer container)
    {
        int framesCount = Math.Clamp((levels.Length - 1) / 9 + 1, 1, int.MaxValue);

        for (int i = 0; i < framesCount; i++)
        {
            int startLevelIndex = i * 9;
            int endLevelIndex = startLevelIndex + 9;
            
            var frame = container.InstantiatePrefab(levelsFramePrefab, framesContainer).GetComponent<LevelsFrame>();
            frame.Init(levels[startLevelIndex..Math.Min(levels.Length, endLevelIndex)]);
        }

        scroll.horizontalNormalizedPosition = 0f;
    }

}