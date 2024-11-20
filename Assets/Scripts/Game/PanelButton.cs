using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PanelButton : MonoBehaviour
{
    
    protected virtual bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }
    
    [SerializeField] protected RectTransform buttonTopImage;

    [Header("Animation")]
    [SerializeField] protected float yActivePosition;
    [SerializeField] protected float yInactivePosition;
    [SerializeField] protected float animationDuration;
    
    [Inject]
    protected TypeText typeText;
    
    protected Button button;
    protected Tween toActiveTween;
    protected Tween toInactiveTween;
   
    protected bool isActive;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();

        toActiveTween = buttonTopImage.DOAnchorPosY(yActivePosition, animationDuration).
            OnComplete(Activate).
            Pause().
            SetAutoKill(false);
        
        toInactiveTween = buttonTopImage.DOAnchorPosY(yInactivePosition, animationDuration).
            Pause().
            SetAutoKill(false);
    }

    private void OnEnable()
    {
        button.onClick.AddListener(ButtonClicked);
    }

    protected virtual void OnDisable()
    {
        button.onClick.RemoveListener(ButtonClicked);
    }
    
    protected virtual void ButtonClicked()
    {
        IsActive = false;
        toInactiveTween.Restart();
    }

    public virtual void ReleaseButton()
    {
        if (!IsActive)
            toActiveTween.Restart();
    }

    private void Activate() => IsActive = true;
    

}