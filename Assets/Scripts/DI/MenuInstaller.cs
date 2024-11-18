using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    
    [SerializeField] private LevelsMenu levelsMenu;
    [SerializeField] private GameEntryPoint gameEntryPoint;
    [SerializeField] private LoadingUI loadingUI;
    
    public override void InstallBindings()
    {
        levelsMenu.gameObject.SetActive(true);
        
        Container.BindInstance<IProgressManager>(new FileProgressManager()).AsSingle().NonLazy();
        Container.BindInstance(levelsMenu).AsSingle().NonLazy();
        Container.BindInstance(loadingUI).AsSingle().NonLazy();
        Container.BindInstance<GameEntryPoint>(gameEntryPoint).AsSingle().NonLazy();
        Container.BindInstance(Container).WithId("MainContainer1").AsSingle().NonLazy();
        
        GameData gameData = JsonUtility.FromJson<GameData>(Resources.Load<TextAsset>("Models/TestData").text);
        Container.BindInstance(gameData).AsSingle().NonLazy();
        
    }
    
}


// Scene Bootstrap
// - Show loading screen
// -- Loading scene Game
// Scene Game
// - Show loading screen +
// -- Getting GameData from Resources +
// -- Getting levels progress from file +
// -- Combine GameData and levels progress to get LevelData[] +
// -- Inject that LevelData[] to LevelsMenu +
// -- Create LevelData/9 LevelsFrame +
// -- Init these LevelsFrame[] using LevelData +
// -- Set scroll position to 0 +
// -- Getting money count from Prefs
// -- Set money count to text
// - Hide loading screen +