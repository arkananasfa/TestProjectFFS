using TMPro;
using UnityEngine;
using Zenject;

public class CoinsCountText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI text; 
        
    [Inject(Id = "Coins")] 
    private ObservableValue<int> _coinsObservableValue;

    private void Awake()
    {
        _coinsObservableValue.OnValueChanged += value => text.text = value.ToString();
        text.text = _coinsObservableValue.Value.ToString();
    }
}