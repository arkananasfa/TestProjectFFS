using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelButton : MonoBehaviour
{

    [SerializeField] private GameObject openedLevelObject;
    [SerializeField] private GameObject closedLevelObject;
    
    [Header("Opened Level")]
    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private TextMeshProUGUI completePercentText;
    [SerializeField] private GameObject completeLevelImage;
    [SerializeField] private Image medalImage;
    [SerializeField] private float textWithMedalRightMargin;

    [Header("Medals Sprites")] 
    [SerializeField] CustomDictionary<int, Sprite> medalsSpritesByPercents;

    [Inject]
    private GameEntryPoint _gameStarter;

    private LevelData _data;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    public void Init(LevelData data)
    {
        _data = data;
        
        SetDefaultState();

        if (!data.IsOpened)
        {
            InitClosed();
            
            return;
        }
        
        openedLevelObject.SetActive(true);
        _button.enabled = true;
        
        wordText.text = data.Word;
        if (data.Percent > 99f)
        {
            completeLevelImage.SetActive(true);
            return;
        }
        
        completePercentText.text = Mathf.RoundToInt(data.Percent) + "%";
        completePercentText.gameObject.SetActive(true);
        
        medalImage.sprite = medalsSpritesByPercents.First(m => m.Item1 > data.Percent).Item2;
        if (medalImage.sprite == null)
        {
            completePercentText.margin = Vector4.zero;
        }
        else
        {
            completePercentText.margin = new Vector4(0, 0, textWithMedalRightMargin, 0);
            medalImage.gameObject.SetActive(true);
        }
        
        _button.onClick.AddListener(StartLevel);
    }

    public void InitClosed()
    {
        closedLevelObject.SetActive(true);
        _button.enabled = true;
        
        _button.onClick.AddListener(ShowOpenLevelPopup);
    }

    private void SetDefaultState()
    {
        openedLevelObject.SetActive(false);
        closedLevelObject.SetActive(false);
        
        completePercentText.gameObject.SetActive(false);
        medalImage.gameObject.SetActive(false);
        
        _button.enabled = false;
        _button.onClick.RemoveAllListeners();
    }

    private void StartLevel()
    {
        _gameStarter.Init(_data);
    }

    private void ShowOpenLevelPopup()
    {
        _data.IsOpened = true;
        Init(_data);
    }

}
