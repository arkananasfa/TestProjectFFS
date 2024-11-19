using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class GameWordButton : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI wordText;
    
    [Inject]
    private WordInfoPopup _wordInfoPopup;
    
    private Button _button;

    private GuessWord _word;
    
    private void Awake() 
    {
        _button = GetComponent<Button>();
    }

    public void Init(GuessWord word)
    {
        _word = word;
        
        char fillChar = word.IsHintUsed ? '+' : '-';
        wordText.text = word.IsGuessed ? word.GameWord.Word : new string(fillChar, word.GameWord.Word.Length);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(ShowWordInfoPopup);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(ShowWordInfoPopup);
    }

    private void ShowWordInfoPopup()
    {
        Debug.Log("ShowWordInfoPopup");
        _wordInfoPopup.Init(_word);
        _wordInfoPopup.OnHintUsed += UseHint;
    }

    private void UseHint()
    {
        wordText.text = new string('+', _word.GameWord.Word.Length);
    }
    
}
