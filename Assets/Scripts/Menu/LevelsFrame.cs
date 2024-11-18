using System;
using UnityEngine;

public class LevelsFrame : MonoBehaviour
{

    [SerializeField] private LevelButton[] levelButtons;
    
    public void Init(LevelData[] levels)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            levelButtons[i].Init(levels[i]);
        }
    }
    
}
