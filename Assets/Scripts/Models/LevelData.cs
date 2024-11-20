using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class LevelData
{

    public event Action<float> OnCompletePercentChanged;

    public float Percent
    {
        get
        {
            if (GuessWords != null && GuessWords.Length > 0)
                return (float)GuessedCount / WordsCount * 100f;
            return 0f;
        }
    }

    public int WordsCount => GuessWords.Length;
    public int GuessedCount => GuessWords.Count(gw => gw.IsGuessed);
    
    public string Word;
    public bool IsOpened;
    public GuessWord[] GuessWords;
    public float SpentTimeSeconds;

    public LevelData(string word)
    {
        Word = word;
        IsOpened = false;
    }

}