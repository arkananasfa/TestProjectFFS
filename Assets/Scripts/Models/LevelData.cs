using System;

[Serializable]
public class LevelData
{

    public string Word;
    public bool IsOpened;
    public float CompletePercent;
    public GuessWord[] GuessWords;

    public LevelData(string word)
    {
        Word = word;
        IsOpened = false;
        CompletePercent = 0;
    }

}

[Serializable]
public class GuessWord
{
    public GameWord GameWord;
    public bool IsGuessed;

    public GuessWord(GameWord gameWord)
    {
        GameWord = gameWord;
        IsGuessed = false;
    }
}