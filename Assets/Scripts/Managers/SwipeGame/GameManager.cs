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

    private int swipeLeft = 0;

    private int swipeRight = 0;

    private bool isPub = false;

    private GameObject actualProfile;

    private void Awake()
    {
       ShowProfile();
    }

    public void Swipe(string dir)
    {
        if(dir == "Right" && isPub)
        {
            Debug.Log("Swipe droite pub");
            Destroy(actualProfile);
            ShowProfile();
        }
        else if (dir == "Right" && !isPub || dir == "Left" && isPub)
        {
            Debug.Log("Game Over");
        }
        else if(dir == "Left" && !isPub)
        {
            Debug.Log("Swipe gauche personne");
            Destroy(actualProfile);
            ShowProfile();
        }
    }

    private void ShowProfile()
    {
        int pub = Random.Range(0, 100);
        int index = Random.Range(0, listPeople.Count-1);
        if (pub < 50)
        {
            actualProfile = Instantiate(listPeople[index], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            if(isPub) isPub = false;
        }
        else
        {
            actualProfile = Instantiate(listPub[index], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            if(!isPub) isPub = true;
        }

    }
}
