using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordInfoPopup : MonoBehaviour
{

    public event Action OnHintUsed;
    
    [Header("Information")]
    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject hiddenDescriptionImage;
    
    [Header("Buttons")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button okButton;
    [SerializeField] private Button hintButton;
    
    private GuessWord _gameWord;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(ClosePopup);
        okButton.onClick.AddListener(ClosePopup);
        hintButton.onClick.AddListener(ShowHint);
    }
    
    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(ClosePopup);
        okButton.onClick.RemoveListener(ClosePopup);
        hintButton.onClick.RemoveListener(ShowHint);

        OnHintUsed = default;
    }

    public void Init(GuessWord word)
    {
        gameObject.SetActive(true);
        
        _gameWord = word;
        if (word.IsGuessed)
            SetGuessedState();
        else
            SetHiddenState();
    }

    private void SetGuessedState()
    {
        wordText.text = _gameWord.GameWord.Word;
        SetDescription();
        SetHintCanBeUsed(false);
    }

    private void SetHiddenState()
    {
        wordText.text = new string(_gameWord.IsHintUsed ? '+' : '-', _gameWord.GameWord.Word.Length);
        SetHintCanBeUsed(!_gameWord.IsHintUsed);
    }

    private void ShowHint()
    {
        _gameWord.IsHintUsed = true;
        OnHintUsed?.Invoke();
        wordText.text = new string('+', _gameWord.GameWord.Word.Length);
        SetDescription();
        SetHintCanBeUsed(false);
    }

    private void SetHintCanBeUsed(bool canBeUsed)
    {
        okButton.gameObject.SetActive(!canBeUsed);
        hintButton.gameObject.SetActive(canBeUsed);
    }

    private void SetDescription()
    {
        descriptionText.gameObject.SetActive(true);
        descriptionText.text = _gameWord.GameWord.Description;
        hiddenDescriptionImage.gameObject.SetActive(false);
    }
    
    private void ClosePopup()
    {
        gameObject.SetActive(false);
    }

}