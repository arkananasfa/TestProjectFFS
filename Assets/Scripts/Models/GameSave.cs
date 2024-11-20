using System;

[Serializable]
public class GameSave
{
    public LevelData[] Levels;
    public int Coins;
    public int Hints;

    public GameSave()
    {
        Levels = new LevelData[0];
        Coins = 50;
        Hints = 10;
    }
}