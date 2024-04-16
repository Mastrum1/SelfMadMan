using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirtyRoadInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    
    [SerializeField] private List<GameObject> _dirtyAds;
    [SerializeField] private GameObject _trashCan;
    [SerializeField] private GameObject _trashFull;

    private void Start()
    {
        _trashCan.GetComponent<OnCollide>().OnCollided += HandleEndGame;
    }

    public void EnableAds(int numOfAds)
    {
        for (var i = 0; i < numOfAds; i++)
        {
            if (i > _dirtyAds.Count)
            {
                Debug.LogError("ads to spawn > ads on screen");
                return;
            }
            
            _dirtyAds[i].SetActive(true);
            
            var adCollider = _dirtyAds[i].GetComponent<OnCollide>();
            adCollider.OnCollided += HandleEndGame;
        }
    }
    
    private void DisableCollision()
    {
        foreach (var ad in _dirtyAds.Where(ad => ad.activeSelf))
        {
            var colliderScript = ad.GetComponent<OnCollide>();
            colliderScript.OnCollided -= HandleEndGame;
        }
    }

    private void HandleEndGame(bool win)
    {
        _trashCan.SetActive(false);
        _trashFull.SetActive(true);
        
        OnGameEnd?.Invoke(win);
    }

    private void OnDestroy()
    {
        _trashCan.GetComponent<OnCollide>().OnCollided -= HandleEndGame;
        DisableCollision();
    }
}
