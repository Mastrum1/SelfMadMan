using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class CrossyPornInteractableManager : MonoBehaviour
{
    public event Action<bool> OnGameEnd;
    
    public GameObject pornAddParent;
    public GameObject trashCan;
        
    void Start()
    {
        for (int i = 0; i < pornAddParent.transform.childCount; i++)
        {
            var addChild = pornAddParent.transform.GetChild(i).GetComponent<OnAddsCollide>();
            if (addChild != null)
            {
                addChild.OnCollided += HandleEndGame;
            }
        }

        trashCan.GetComponent<OnAddsCollide>().OnCollided += HandleEndGame;
    }

    void HandleEndGame(bool win)
    {
        Debug.Log("yes");
        OnGameEnd?.Invoke(win);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < pornAddParent.transform.childCount; i++)
        {
            var addChild = pornAddParent.transform.GetChild(i).GetComponent<OnAddsCollide>();
            if (addChild != null)
            {
                addChild.OnCollided -= HandleEndGame;
            }
        }
        trashCan.GetComponent<OnAddsCollide>().OnCollided -= HandleEndGame;
    }
}
