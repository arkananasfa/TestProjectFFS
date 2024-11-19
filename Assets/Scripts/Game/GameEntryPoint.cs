using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    
    [SerializeField] private WordsPaper _wordsPaper;

    [Inject]
    private GameData _gameData;
    
    [Inject]
    private LoadingUI _loadingUI;
    
    [Inject]
    private GameScreen _gameScreen;
    
    private LevelData _levelData;    
        
    public void Init(LevelData levelData)
    {
        _loadingUI.gameObject.SetActive(true);
        _gameScreen.gameObject.SetActive(true);
        _gameScreen.SetWord(levelData.Word);
        
        _levelData = levelData;

        RemoveDuplicates();

        if (levelData.GuessWords == null || levelData.GuessWords.Length == 0)
            SetGuessWords();
        
        _wordsPaper.Init(_levelData);
    }

    private void RemoveDuplicates()
    {
        _gameData.GameWords = 
            _gameData.GameWords.GroupBy(gw => gw.Word).Select(g => g.First()).ToArray();
    }

    private void SetGuessWords()
    {
        HashSet<string> words = new HashSet<string>();

        string word = _levelData.Word;
        var word1CharCount = 
            word.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        
        _levelData.GuessWords = _gameData.GameWords.
        Where(gw1 => word != gw1.Word && IsContainsAllChars(word1CharCount, gw1.Word)).
        Select(gw2 => new GuessWord(gw2)).
        OrderBy(guessWord => guessWord.GameWord.Word.Length).
        ToArray();
    }

    private bool IsContainsAllChars(Dictionary<char, int> word1CharCount, string word2)
    {
        var word2CharCount = word2.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        foreach (var kvp in word2CharCount)
        {
            if (!word1CharCount.ContainsKey(kvp.Key) || word1CharCount[kvp.Key] < kvp.Value)
            {
                return false;
            }
        }

        return true;
    }
    
}