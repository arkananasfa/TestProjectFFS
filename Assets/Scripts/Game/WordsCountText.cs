using TMPro;
using UnityEngine;

public class WordsCountText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;

    private int _allWordsCount;
    private int _guessedWordsCount;
    
    public void Init(int allCount, int guessedCount)
    {
        _allWordsCount = allCount;
        _guessedWordsCount = guessedCount;
        SetText();
    }

    public void GuessOneMoreWord()
    {
        _guessedWordsCount++;
        SetText();
    }

    private void SetText()
    {
        text.text = $"{_guessedWordsCount}/{_allWordsCount}";
    }

}