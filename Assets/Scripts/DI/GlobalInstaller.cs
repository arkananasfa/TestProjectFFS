using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    
    [SerializeField] private LevelsMenu levelsMenu;
    [SerializeField] private GameEntryPoint gameEntryPoint;
    [SerializeField] private LoadingUI loadingUI;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private WordInfoPopup wordInfoPopup;
    [SerializeField] private ButtonsPanel buttonsPanel;
    [SerializeField] private WordsPaper wordsPaper;
    [SerializeField] private TypeText typeText;
    [SerializeField] private WordsCountText wordsCountText;
    
    public override void InstallBindings()
    {
        loadingUI.gameObject.SetActive(true);

        IProgressManager progressManager = new FileProgressManager();
        Container.BindInstance(progressManager).AsSingle().NonLazy();
        Container.BindInstance(progressManager.Coins).WithId("Coins").NonLazy();
        Container.BindInstance(progressManager.Hints).WithId("Hints").NonLazy();
        
        Container.BindInstance(gameEntryPoint).AsSingle().NonLazy();
        Container.BindInstance(levelsMenu).AsSingle().NonLazy();
        Container.BindInstance(loadingUI).AsSingle().NonLazy();
        Container.BindInstance(gameScreen).AsSingle().NonLazy();
        Container.BindInstance(wordInfoPopup).AsSingle().NonLazy();
        Container.BindInstance(buttonsPanel).AsSingle().NonLazy();
        Container.BindInstance(typeText).AsSingle().NonLazy();
        Container.BindInstance(wordsPaper).AsSingle().NonLazy();
        Container.BindInstance(wordsCountText).AsSingle().NonLazy();
        Container.BindInstance(Container).WithId("MainContainer1").AsSingle().NonLazy();
        
        GameData gameData = JsonUtility.FromJson<GameData>(Resources.Load<TextAsset>("Models/TestData").text);
        Container.BindInstance(gameData).AsSingle().NonLazy();
    }
    
}