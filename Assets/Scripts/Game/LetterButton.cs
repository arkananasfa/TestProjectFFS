using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class LetterButton : PanelButton
{

    public event Action<LetterButton> OnClick;
    
    protected override bool IsActive
    {
        get => isActive;
        set
        {
            base.IsActive = value;
            button.interactable = isActive;
            letterText.text = value ? _letter : string.Empty;
        }
    }
    
    [Header("Letter")]
    [SerializeField] private TextMeshProUGUI letterText;

    private string _letter;

    protected override void Awake()
    {
        base.Awake();
        ClearInfo();
        typeText.OnWordDisappear += ReleaseButton;
        typeText.OnUncorrectWordTyped += TurnOffThenOn;
    }

    private void TurnOffThenOn()
    {
        if (!string.IsNullOrEmpty(_letter))
            StartCoroutine(TurnOffThenOnRoutine());
    }

    private IEnumerator TurnOffThenOnRoutine()
    {
        IsActive = false;
        toInactiveTween.Restart();
        yield return new WaitForSeconds(0.8f);
        toActiveTween.Restart();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        ClearInfo();
    }

    public void Init(string letter)
    {
        _letter = letter;
        toActiveTween.Restart();
    }

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        typeText.AddLetter(_letter);
        OnClick?.Invoke(this);
    }

    private void ClearInfo()
    {
        button.interactable = false;
        buttonTopImage.anchoredPosition = new(0f, yInactivePosition);
        _letter = "";
        letterText.text = "";
    }
}