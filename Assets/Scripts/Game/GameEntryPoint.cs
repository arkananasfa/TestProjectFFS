using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{

    [Inject]
    private GameData _gameData;
    
    private LevelData _levelData;    
        
    public void Init(LevelData levelData)
    {
        _levelData = levelData;

        RemoveDuplicates();

        if (levelData.GuessWords == null || levelData.GuessWords.Length == 0)
            SetGuessWords();
    }

    private void RemoveDuplicates()
    {
        _gameData.GameWords = 
            _gameData.GameWords.GroupBy(gw => gw.Word).Select(g => g.First()).ToArray();
    }

    private void SetGuessWords()
    {
        //Debug.Log($"CHECKING {_levelData.Word}");
        HashSet<string> words = new HashSet<string>();

        string word = _levelData.Word;
        var word1CharCount = 
            word.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        
        _levelData.GuessWords = _gameData.GameWords.
        Where(gw => word != gw.Word && IsContainsAllChars(word1CharCount, gw.Word)).
        Select(gw => new GuessWord(gw)).
        ToArray();
    }

    private bool IsContainsAllChars(Dictionary<char, int> word1CharCount, string word2)
    {
        var word2CharCount = word2.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        foreach (var kvp in word2CharCount)
        {
            if (!word1CharCount.ContainsKey(kvp.Key) || word1CharCount[kvp.Key] < kvp.Value)
            {
                //Debug.Log("NO");
                return false;
            }
        }

        return true;
    }
    
}