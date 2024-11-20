using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class ControlButton : PanelButton
{

    [Inject]
    private ButtonsPanel buttonsPanel;
    
    [SerializeField] private UsageType usage;

    protected override void ButtonClicked()
    {
        base.ButtonClicked();
        if (usage == UsageType.Clear)
            typeText.ClearWord();
        else
            typeText.RemoveLastLetter();
        ReleaseButton();
    }

    protected override void Awake()
    {
        base.Awake();
        IsActive = true;
        button.interactable = true;
        buttonTopImage.anchoredPosition = new(0f, yActivePosition);
    }

    public override void ReleaseButton()
    {
        StartCoroutine(WaitThenRelease());
    }

    private IEnumerator WaitThenRelease()
    {
        yield return new WaitForSeconds(animationDuration);
        base.ReleaseButton();
    }

    [Serializable]
    private enum UsageType
    {
        Clear,
        Remove
    }
    
}