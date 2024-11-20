using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

public class WordsPaper : MonoBehaviour
{

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameWordButton gameWordPrefab;
    [SerializeField] private Transform gameWordsContainer;
 
    [Header("Appear animation")] 
    [SerializeField] private Transform startPoint;
    [SerializeField] private List<AnimationStep> animationSteps;

    private List<GameWordButton> gameWords = new();

    [Inject(Id = "MainContainer1")]
    private DiContainer _container;
    
    [Inject]
    private LoadingUI _loadingUI;
    
    [Inject]
    private GameScreen _gameScreen;

    private Sequence _appearSequence;

    private Dictionary<string, GameWordButton> gameWordsByWords = new();
    
    private void Awake()
    {
        BackToStartPosition();
        
        _appearSequence = DOTween.Sequence();
        foreach (var step in animationSteps)
        {
            _appearSequence.Append(GetComponent<RectTransform>().DOAnchorPos(step.point.anchoredPosition, step.duration));
            _appearSequence.Join(transform.DORotate(step.point.rotation.eulerAngles, step.duration));
        }

        _appearSequence.SetAutoKill(false);
    }

    private void OnEnable()
    {
        _gameScreen.OnClosed += BackToStartPosition;
    }
    
    private void OnDisable()
    {
        _gameScreen.OnClosed -= BackToStartPosition;
    }
    
    public void Init(LevelData levelData)
    {
        gameWordsByWords.Clear();
        int i = 0;
        foreach (var word in levelData.GuessWords)
        {
            if (i < gameWords.Count)
            {
                gameWords[i].gameObject.SetActive(true);
                gameWords[i].Init(word);
                gameWordsByWords.Add(word.GameWord.Word, gameWords[i]);
            }
            else
            {
                var gameWord = _container.InstantiatePrefab(gameWordPrefab, gameWordsContainer).GetComponent<GameWordButton>();
                gameWord.Init(word);
                gameWords.Add(gameWord);
                gameWordsByWords.Add(word.GameWord.Word, gameWord);
            }
            i++;
        }

        for (int j = i; j < gameWords.Count; j++) 
            gameWords[j].gameObject.SetActive(false);
        
        _loadingUI.gameObject.SetActive(false);

        _appearSequence.Restart();
    }

    public bool TryGuessWord(string word)
    {
        if (gameWordsByWords.ContainsKey(word))
        {
            var button = gameWordsByWords[word];
            button.Guess();
            SetScrollToElement(button.GetComponent<RectTransform>());
            return true;
        }

        return false;
    }

    private void BackToStartPosition()
    {
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;
    }

    private void SetScrollToElement(RectTransform element)
    {
        float contentHeight = scrollRect.content.rect.height - scrollRect.viewport.rect.height;
        float elementY = -element.anchoredPosition.y;
        float newNormalizedPositionY = 1 - Mathf.Clamp01((elementY-element.rect.height/2f-47) / contentHeight);
        scrollRect.verticalNormalizedPosition = newNormalizedPositionY;
    }

    [Serializable]
    private class AnimationStep
    {
        public RectTransform point;
        public float duration;
    }

}