using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int totalNbSwipeLeft = 5;
    [SerializeField]
    private int totalNbSwipeRight = 10;

    [SerializeField]
    private List<GameObject> listPub;

    [SerializeField]
    private List<GameObject> listPeople;

    [SerializeField]
    private SwipeDetection swipe;

    private int swipeLeft = 0;

    private int swipeRight = 0;

    private bool isPub = false;

    private GameObject actualProfile;

    public string SwipeDir = "Horizontal";

    private void Awake()
    {
        ShowProfile();
    }

    private void OnEnable()
    {
        swipe.OnSwipeLeft += SwipeLeft;
        swipe.OnSwipeRight += SwipeRight;
    }

    private void OnDisable()
    {
        swipe.OnSwipeLeft -= SwipeLeft;
        swipe.OnSwipeRight -= SwipeRight;
    }

    public void SwipeLeft()
    {
        if (isPub)
        {
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("Swipe gauche people");
            Destroy(actualProfile);
            swipeLeft++;
            if (swipeLeft < totalNbSwipeLeft)
            {
                ShowProfile();
            }
            else
            {
                Debug.Log("Win Game");
            }
        }
    }

    public void SwipeRight()
    {
        if (!isPub)
        {
            Debug.Log("Game Over");
        }
        else
        {
            Debug.Log("Swipe Right pub");
            Destroy(actualProfile);
            swipeLeft++;
            ShowProfile();
        }
    }

    private void ShowProfile()
    {
        int pub = Random.Range(0, 100);
        int index = Random.Range(0, listPeople.Count - 1);
        if (pub < 50)
        {
            actualProfile = Instantiate(listPeople[index], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            if (isPub) isPub = false;
        }
        else if (swipeRight < totalNbSwipeRight)
        {
            actualProfile = Instantiate(listPub[index], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            if (!isPub) isPub = true;
        }

    }
}
