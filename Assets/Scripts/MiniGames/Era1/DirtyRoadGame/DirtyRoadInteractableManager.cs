using System;
using UnityEngine;

public class DirtyRoadInteractableManager : InteractableManager
{
    public event Action<bool> OnGameEnd;
    public event Action<int> OnSpawnMoreAds;
    
    [SerializeField] private GameObject _dirtyAddParent;
    [SerializeField] private GameObject _trashCan;

    private void Start()
    {
        _trashCan.GetComponent<OnCollide>().OnCollided += HandleEndGame;
    }

    public void EnableAds(int numOfAds)
    {
        if (numOfAds > _dirtyAddParent.transform.childCount)
        {
            OnSpawnMoreAds?.Invoke(numOfAds - _dirtyAddParent.transform.childCount);
        }
        
        for (var i = 0; i < numOfAds; i++)
        {
            _dirtyAddParent.transform.GetChild(i).gameObject.SetActive(true);
            
            var adCollider = _dirtyAddParent.transform.GetChild(i).GetComponent<OnCollide>();
            adCollider.OnCollided += HandleEndGame;
        }
    }
    
    private void DisableCollision()
    {
        for (var i = 0; i < _dirtyAddParent.transform.childCount; i++)
        {
            var ad = _dirtyAddParent.transform.GetChild(i).GetComponent<OnCollide>();
            ad.OnCollided -= HandleEndGame;
        }
    }

    private void HandleEndGame(bool win)
    {
        OnGameEnd?.Invoke(win);
    }

    private void OnDestroy()
    {
        _trashCan.GetComponent<OnCollide>().OnCollided -= HandleEndGame;
        DisableCollision();
    }
}
