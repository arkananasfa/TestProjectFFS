using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LetterButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI letterText;

    private Button _button;
    private string _letter;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Init(string letter)
    {
        _letter = letter;
        letterText.text = letter;
    }

    private void ButtonClicked()
    {
        letterText.text = 
    }
}