using System;
using UnityEngine;

[Serializable]
public class LevelData
{

    public event Action<float> OnCompletePercentChanged;

    public float Percent
    {
        get => CompletePercent;
        set
        {
            OnCompletePercentChanged?.Invoke(value);
            CompletePercent = value;
        }
    } 
    
    public string Word;
    public bool IsOpened;
    public GuessWord[] GuessWords;
    public float SpentTime;
    
    [SerializeField] float CompletePercent;

    public LevelData(string word)
    {
        Word = word;
        IsOpened = false;
        CompletePercent = 0;
    }

}