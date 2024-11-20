using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HintsCountPanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button buyHintsButton;
        
    [Inject(Id = "Hints")] 
    private ObservableValue<int> _hintsObservableValue;
    
    [Inject(Id = "Coins")] 
    private ObservableValue<int> _coinsObservableValue;

    private void Awake()
    {
        _hintsObservableValue.OnValueChanged += value => text.text = value.ToString();
        text.text = _hintsObservableValue.Value.ToString();
    }

    private void OnEnable()
    {
        buyHintsButton.onClick.AddListener(BuyHints);
    }
    
    private void OnDisable()
    {
        buyHintsButton.onClick.RemoveListener(BuyHints);
    }

    private void BuyHints()
    {
        if (_coinsObservableValue.Value > 5)
        {
            _coinsObservableValue.Value -= 5;
            _hintsObservableValue.Value++;
        }
    }
}