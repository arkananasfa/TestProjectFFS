using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    
    [SerializeField] private LevelsMenu levelsMenu;
    [SerializeField] private GameEntryPoint gameEntryPoint;
    [SerializeField] private LoadingUI loadingUI;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private WordInfoPopup wordInfoPopup;
    
    public override void InstallBindings()
    {
        levelsMenu.gameObject.SetActive(true);
        
        Container.BindInstance<IProgressManager>(new FileProgressManager()).AsSingle().NonLazy();
        Container.BindInstance(levelsMenu).AsSingle().NonLazy();
        Container.BindInstance(loadingUI).AsSingle().NonLazy();
        Container.BindInstance(gameScreen).AsSingle().NonLazy();
        Container.BindInstance(wordInfoPopup).AsSingle().NonLazy();
        Container.BindInstance<GameEntryPoint>(gameEntryPoint).AsSingle().NonLazy();
        Container.BindInstance(Container).WithId("MainContainer1").AsSingle().NonLazy();
        
        GameData gameData = JsonUtility.FromJson<GameData>(Resources.Load<TextAsset>("Models/TestData").text);
        Container.BindInstance(gameData).AsSingle().NonLazy();
        
    }
    
}