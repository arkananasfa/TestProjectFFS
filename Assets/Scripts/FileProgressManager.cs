using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class FileProgressManager : IProgressManager
{

    private readonly string levelsPath = Application.persistentDataPath + "/levelsProgress.json";
    private readonly string resourcesPath = Application.persistentDataPath + "/resources.json";

    private List<LevelData> _levelsProgress;
    
    public async Task<List<LevelData>> GetLevelsProgress()
    {
        if (System.IO.File.Exists(levelsPath))
            _levelsProgress = JsonUtility.FromJson<LevelDataArrayWrapper>(await System.IO.File.ReadAllTextAsync(levelsPath));
        
        if (_levelsProgress == null)
            _levelsProgress = new();
        
        return _levelsProgress;
    }

    public void SetLevelsProgress(string word, LevelData level)
    {
        if (_levelsProgress.FirstOrDefault(lp => lp.Word == word) == null)
            _levelsProgress.Add(level);
    }

    public async Task SaveLevelsProgress()
    {
        LevelDataArrayWrapper wrapper = _levelsProgress;
        await System.IO.File.WriteAllTextAsync(levelsPath, JsonUtility.ToJson(wrapper));
    }
    
    [Serializable]
    private class LevelDataArrayWrapper
    {
        public LevelData[] Levels;
        
        public static implicit operator List<LevelData>(LevelDataArrayWrapper wrapper) => wrapper.Levels.ToList();
        public static implicit operator LevelDataArrayWrapper(List<LevelData> array) => new() { Levels = array.ToArray() };
    }
    
}