using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class FileProgressManager : IProgressManager
{

    private readonly string levelsPath = Application.persistentDataPath + "/levelsProgress.json";

    private GameSave _gameSave;

    public ObservableValue<int> Coins { get; set; } = 0;
    public ObservableValue<int> Hints { get; set; } = 0;

    public async Task<GameSave> GetLevelsProgress()
    {
        if (System.IO.File.Exists(levelsPath))
            _gameSave = JsonUtility.FromJson<GameSave>(await System.IO.File.ReadAllTextAsync(levelsPath));
        
        if (_gameSave == null)
            _gameSave = new();

        Coins.Value = _gameSave.Coins;
        Hints.Value = _gameSave.Hints;
        
        Coins.OnValueChanged += value => _gameSave.Coins = value; 
        Hints.OnValueChanged += value => _gameSave.Hints = value; 
        
        return _gameSave;
    }

    public void SetLevelsProgress(string word, LevelData level)
    {
        if (_gameSave.Levels.FirstOrDefault(lp => lp.Word == word) == null)
        {
            List<LevelData> levels =_gameSave.Levels.ToList();
            levels.Add(level);
            _gameSave.Levels = levels.ToArray();
        }
    }

    public async Task SaveLevelsProgress()
    {
        await System.IO.File.WriteAllTextAsync(levelsPath, JsonUtility.ToJson(_gameSave));
    }
    
    [Serializable]
    private class LevelDataArrayWrapper
    {
        public LevelData[] Levels;
        
        public static implicit operator List<LevelData>(LevelDataArrayWrapper wrapper) => wrapper.Levels.ToList();
        public static implicit operator LevelDataArrayWrapper(List<LevelData> array) => new() { Levels = array.ToArray() };
    }
    
}