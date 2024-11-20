using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class TypeText : MonoBehaviour
{

    public event Action OnWordDisappear;
    public event Action OnUncorrectWordTyped;
    
    private string Word
    {
        get => wordText.text;
        set => wordText.text = value;
    }
    
    [SerializeField] private TextMeshProUGUI wordText;

    [Header("Shake animation")]
    [SerializeField] private Color shakeColor;
    [SerializeField] private float duration = 1f;
    
    [Inject]
    private WordsPaper _wordsPaper;

    private IEnumerator _checkWordRoutine;

    private Color _startColor;
    private Tween _shakeTween;

    private void Awake()
    {
        _shakeTween = wordText.GetComponent<RectTransform>().DOShakeAnchorPos(duration, 10f).OnComplete(SetStartColor).Pause().SetAutoKill(false);
        _startColor = wordText.color;
        _checkWordRoutine = CheckWordRoutine();
    }
    
    public void AddLetter(string letter)
    {
        Word += letter;
        RestartCheckWordRoutine();
    }

    public void ClearWord()
    {
        Word = "";
        OnWordDisappear?.Invoke();
        StopCoroutine(_checkWordRoutine);
    }

    public void RemoveLastLetter()
    {
        if (Word.Length > 0)
            Word = Word.Remove(Word.Length - 1);

        if (Word.Length == 0)
            StopCoroutine(_checkWordRoutine);
        else
            RestartCheckWordRoutine();
    }

    private void RestartCheckWordRoutine()
    {
        StopCoroutine(_checkWordRoutine);
        _checkWordRoutine = CheckWordRoutine();
        StartCoroutine(_checkWordRoutine);
    }
    
    private IEnumerator CheckWordRoutine()
    {
        yield return new WaitForSeconds(1f);
        EnterWord();
    }

    private void EnterWord()
    {
        if (_wordsPaper.TryGuessWord(Word))
            ClearWord();
        else
            ShakeIncorrect();
    }

    private void ShakeIncorrect()
    {
        OnUncorrectWordTyped?.Invoke();
        wordText.color = shakeColor;
        _shakeTween.Restart();
    }

    private void SetStartColor()
    {
        wordText.color = _startColor;
        Word = "";
    }

}