using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class DirtyRoadInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    
    public GameObject dirtyAddParent;
    public GameObject trashCan;
        
    void Start()
    {
        trashCan.GetComponent<OnCollide>().OnCollided += HandleEndGame;
    }

    public void EnableCollision(List<Road> roads)
    {
        foreach (var road in roads)
        {
            Debug.Log("road");
            foreach (var ad in road.Ads)
            {
                Debug.Log("Ad");
                ad.OnCollided += HandleEndGame;
            }
        }
    }
    
    public void DisableCollision(List<Road> roads)
    {
        foreach (var road in roads)
        {
            foreach (var ad in road.Ads)
            {
                ad.OnCollided -= HandleEndGame;
            }
        }
    }
    void HandleEndGame(bool win)
    {
        OnGameEnd?.Invoke(win);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < dirtyAddParent.transform.childCount; i++)
        {
            var adChild = dirtyAddParent.transform.GetChild(i).GetComponent<OnCollide>();
            if (adChild != null)
            {
                adChild.OnCollided -= HandleEndGame;
            }
        }
        trashCan.GetComponent<OnCollide>().OnCollided -= HandleEndGame;
    }
}
