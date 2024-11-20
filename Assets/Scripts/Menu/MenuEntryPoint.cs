using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class MenuEntryPoint : MonoBehaviour
{

    [Inject]
    private LoadingUI _loadingUI;
    
    [Inject]
    private IProgressManager _progressManager;
    
    [Inject]
    private LevelsMenu _levelsMenu;
    
    [Inject(Id = "MainContainer1")]
    private DiContainer _container;
    
    [Inject]
    private async void Init(GameData gameData)
    {
        GameSave gameSave = await _progressManager.GetLevelsProgress();
        List<LevelData> levels = gameData.Levels
            .Select(w => gameSave.Levels.FirstOrDefault(lp => lp.Word == w) ?? new LevelData(w)).ToList();
        levels.ForEach(l => _progressManager.SetLevelsProgress(l.Word, l));
        
        _levelsMenu.Init(levels.ToArray(), _container);
        _levelsMenu.gameObject.SetActive(true);
        _loadingUI.gameObject.SetActive(false);
    }

    private async void OnApplicationQuit()
    {
        await _progressManager.SaveLevelsProgress();
    }
}
