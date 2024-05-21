using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Loading : MonoBehaviour
{

    [SerializeField] public RectTransform fill;
    [SerializeField]  public float duration = 3f;
    private float startTime;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    [SerializeField] private CinematicHandler _cinematicHandler;
    [SerializeField] private GameObject _MyPanel;
    private bool _videoPlayin = false;

    void Start()
    {
        targetPosition = new Vector2(0,0) ;
        startPosition = fill.anchoredPosition;
        startTime = Time.time;

        _cinematicHandler.OnReachedPoint += DisplayPanel;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        fill.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

        if (t >= 1.0f && !_videoPlayin)
        {

            /* _cinematicHandler.PlayVideo();
              _videoPlayin = true;
              Debug.Log("playing");*/
            StartGame();


        }
    }
    private void DisplayPanel()
    {
        _MyPanel.SetActive(true);
    }
    public void StartGame()
    {
        mySceneManager.instance.LoadWinScreen();
        mySceneManager.instance.SetScene("HomePage", mySceneManager.LoadMode.ADDITIVE);
    }
}
