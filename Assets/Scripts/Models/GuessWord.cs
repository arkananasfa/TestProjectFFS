using System;

[Serializable]
public class GuessWord
{
    public GameWord GameWord;
    public bool IsGuessed;
    public bool IsHintUsed;

    public GuessWord(GameWord gameWord)
    {
        GameWord = gameWord;
        IsGuessed = false;
    }
}