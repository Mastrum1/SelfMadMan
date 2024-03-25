using System;
using UnityEngine;

public class CleanYourKitchenInteractableManager : InteractableManager
{
    public event Action OnRoachDeath;
    
    [SerializeField] private GameObject _cockroachParent;
    private void Start()
    {
        for (int i = 0; i < _cockroachParent.transform.childCount; i++)
        {
            var cockroachChild = _cockroachParent.transform.GetChild(i).GetComponent<Cockroach>();
            if (cockroachChild != null)
            {
                cockroachChild.OnTouched += HandleRoachDeath;
            }
        }
    }

    void HandleRoachDeath()
    {
        OnRoachDeath?.Invoke();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < _cockroachParent.transform.childCount; i++)
        {
            var cockroachChild = _cockroachParent.transform.GetChild(i).GetComponent<Cockroach>();
            if (cockroachChild != null)
            {
                cockroachChild.OnTouched -= HandleRoachDeath;
            }
        }
    }
}