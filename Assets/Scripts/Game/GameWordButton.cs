using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class GameWordButton : MonoBehaviour
{

    public GuessWord Word => _word;

    [SerializeField] private TextMeshProUGUI wordText;

    [Header("Blink animation")]
    [SerializeField] private float maxScaleSize;
    [SerializeField] private float duration;
    [SerializeField] private Color correctColor;
    [SerializeField] private Color incorrectColor;
    
    [Inject]
    private WordInfoPopup _wordInfoPopup;
    
    [Inject]
    private WordsCountText _wordsCountText;
    
    [Inject(Id = "Coins")] 
    private ObservableValue<int> _coinsObservableValue;
    
    private Button _button;
    private GuessWord _word;
    private Color startColor;
    private Sequence _blinkAnimation;
    
    private void Awake() 
    {
        startColor = wordText.color;
        _button = GetComponent<Button>();
        
        
        _blinkAnimation = DOTween.Sequence();
        _blinkAnimation.
            Append(transform.DOScale(maxScaleSize, duration / 2f)).
            Append(transform.DOScale(1f, duration / 2f)).
            OnComplete(SetStartColor).
            Pause().
            SetAutoKill(false);
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

    public void Guess()
    {
        if (!_word.IsGuessed)
        {
            _coinsObservableValue.Value++;
            _wordsCountText.GuessOneMoreWord();
        }
        
        wordText.text = Word.GameWord.Word;
        wordText.color = Word.IsGuessed ? incorrectColor : correctColor;
        Word.IsGuessed = true;
        _blinkAnimation.Restart();
    }

    private void ShowWordInfoPopup()
    {
        _wordInfoPopup.Init(_word);
        _wordInfoPopup.OnHintUsed += UseHint;
    }

    private void UseHint()
    {
        wordText.text = new string('+', _word.GameWord.Word.Length);
    }

    private void SetStartColor()
    {
        wordText.color = startColor;
    }
    
}
