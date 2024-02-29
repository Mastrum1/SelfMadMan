using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class CrossyPornInteractableManager : MonoBehaviour
{
    public event Action OnGameEnd;
    
    public GameObject pornAddParent;
        
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
    }

    void HandleEndGame()
    {
        OnGameEnd?.Invoke();
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
    }
}
