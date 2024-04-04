using System;
using System.Collections;
using UnityEngine;

public class FeelTheProtsInteractableManager : InteractableManager
{
    public event Action<bool> OnEndGame; 
    
    public bool IsHolding { get; set; }
    
    [SerializeField] private SuccessBar _successBar;
    [SerializeField] private FailBar _failBar;
    void Start()
    {
        _successBar.OnParticleSuccess += HandleWin;
        _failBar.OnFail += HandleLose;
    }

    private void Update()
    {
        if (IsHolding)
        {
            StopCoroutine(OnWin());
        }
    }

    void HandleWin()
    {
        if (!IsHolding)
        {
            StartCoroutine(OnWin());
        }
    }

    private IEnumerator OnWin()
    {
        yield return new WaitForSeconds(1f);
        OnEndGame?.Invoke(true);
    }
    void HandleLose()
    {
        OnEndGame?.Invoke(false);
    }

    private void OnDestroy()
    {
        _successBar.OnParticleSuccess -= HandleWin;
        _failBar.OnFail -= HandleLose;
    }
}