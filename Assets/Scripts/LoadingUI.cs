using System.Collections;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI loadingText;

    private IEnumerator loadingRoutine;

    private void Awake()
    {
        loadingRoutine = LoadingRoutine();
    }
    
    private void OnEnable()
    {
        StartCoroutine(loadingRoutine);
    }

    private void OnDisable()
    {
        StopCoroutine(loadingRoutine);
    }

    private IEnumerator LoadingRoutine()
    {
        int tick = 0;
        string mainText = "Loading";
        string[] pointsTexts = { ".", "..", "..." };

        while (true)
        {
            loadingText.text = mainText + pointsTexts[tick];
            tick = (tick + 1) % 3;
            yield return new WaitForSeconds(0.2f);
        } 
    }
    
}
