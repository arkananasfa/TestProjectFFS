using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{

    public event Action OnClosed;

    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private Button button;
    private void OnEnable()
    {
        button.onClick.AddListener(CloseGameScreen);
    }
    
    private void OnDisable()
    {
        button.onClick.RemoveListener(CloseGameScreen);
    }

    public void SetWord(string word)
    {
        wordText.text = word;
    } 

    private void CloseGameScreen()
    {
        OnClosed?.Invoke();
        gameObject.SetActive(false);
    }
}