using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class DirtyRoadInteractableManager : MonoBehaviour
{
    public event Action<bool> OnGameEnd;
    
    public GameObject dirtyAddParent;
    public GameObject trashCan;
        
    void Start()
    {
        for (int i = 0; i < dirtyAddParent.transform.childCount; i++)
        {
            var addChild = dirtyAddParent.transform.GetChild(i).GetComponent<OnAddsCollide>();
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
        for (int i = 0; i < dirtyAddParent.transform.childCount; i++)
        {
            var addChild = dirtyAddParent.transform.GetChild(i).GetComponent<OnAddsCollide>();
            if (addChild != null)
            {
                addChild.OnCollided -= HandleEndGame;
            }
        }
        trashCan.GetComponent<OnAddsCollide>().OnCollided -= HandleEndGame;
    }
}
