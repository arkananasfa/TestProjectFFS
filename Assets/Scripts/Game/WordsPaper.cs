using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class WordsPaper : MonoBehaviour
{

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
        int i = 0;
        foreach (var word in levelData.GuessWords)
        {
            if (i < gameWords.Count)
            {
                gameWords[i].gameObject.SetActive(true);
                gameWords[i].Init(word);
            }
            else
            {
                var gameWord = _container.InstantiatePrefab(gameWordPrefab, gameWordsContainer).GetComponent<GameWordButton>();
                gameWord.Init(word);
                gameWords.Add(gameWord);
            }
            i++;
        }

        for (int j = i; j < gameWords.Count; j++) 
            gameWords[j].gameObject.SetActive(false);
        
        _loadingUI.gameObject.SetActive(false);

        _appearSequence.Restart();
    }

    private void BackToStartPosition()
    {
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;
    }

    [Serializable]
    private class AnimationStep
    {
        public RectTransform point;
        public float duration;
    }

}