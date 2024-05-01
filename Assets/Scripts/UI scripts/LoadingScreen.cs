using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{

    [SerializeField] public RectTransform fill;
    [SerializeField]  public float duration = 3f;
    private float startTime;
    private Vector2 startPosition;
    private Vector2 targetPosition;

    void Start()
    {
        targetPosition = new Vector2(0,0) ;
        startPosition = fill.anchoredPosition;
        startTime = Time.time;
    }

    void Update()
    {
        float t = (Time.time - startTime) / duration;
        fill.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

        if (t >= 1.0f)
        {
            mySceneManager.instance.LoadWinScreen();
            mySceneManager.instance.SetScene("HomePage", mySceneManager.LoadMode.ADDITIVE);
        }
    }
}
