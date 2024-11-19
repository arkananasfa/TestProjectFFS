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

        while (true)
        {
            loadingText.text = mainText + new string('.', tick+1);
            tick = (tick + 1) % 3;
            yield return new WaitForSeconds(0.2f);
        } 
    }
    
}
