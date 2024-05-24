using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Loading : MonoBehaviour
{

    [SerializeField] public RectTransform fill;
    [SerializeField] public float duration = 3f;
    private float startTime;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    [SerializeField] private GameObject _MyPanel;
    [SerializeField] private GameObject _myBorders;
    private bool _videoPlayin = false;
    private AsyncOperation asyncOperation;
    private bool isLoading = false;

    void Start()
    {
        targetPosition = new Vector2(0, 0);
        startPosition = fill.anchoredPosition;
        startTime = Time.time;

        // Start the loading process
        StartCoroutine(OnStart());
    }

    public IEnumerator OnStart()
    {
        yield return new WaitForSeconds(0.2f);
        StartLoading();
    }

    void Update()
    {
        if (isLoading)
        {
            // Update fill anchored position based on asyncOperation progress
            float t = asyncOperation.progress / 0.9f; // Normalize progress to be between 0 and 1
            fill.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            // Check if loading is almost complete
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }
        }
    }

    private void StartLoading()
    {
        
        asyncOperation = GameManager.instance.Player.IntroPlayed ? SceneManager.LoadSceneAsync("HomePage") : SceneManager.LoadSceneAsync("CinematicScene");
        asyncOperation.allowSceneActivation = false;
        isLoading = true;

        StartCoroutine(WaitForSceneLoad());
    }

    private IEnumerator WaitForSceneLoad()
    {
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    

}
