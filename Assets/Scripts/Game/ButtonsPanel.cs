using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ButtonsPanel : MonoBehaviour
{

    [SerializeField] private List<LetterButton> letterButtons;
    [SerializeField] private PanelButton enterButton;
    [SerializeField] private PanelButton eraseButton;

    private Stack<LetterButton> _lastPressedButtonsStack = new();

    [Inject] 
    private TypeText _typeText;
    
    public void Init(List<string> letters)
    {
        _typeText.OnWordDisappear += ClearStack;
        for (int i = 0; i < letters.Count; i++)
        {
            letterButtons[i].Init(letters[i]);
            letterButtons[i].OnClick += RegisterButtonToStack;
        }
    }

    public void ReleaseLastPressedButtons()
    {
        if (_lastPressedButtonsStack.Count != 0)
            _lastPressedButtonsStack.Pop().ReleaseButton();
    }
    
    private void RegisterButtonToStack(LetterButton letterButton)
    {
        _lastPressedButtonsStack.Push(letterButton);
    }

    private void ClearStack()
    {
        _lastPressedButtonsStack.Clear();
    }

}