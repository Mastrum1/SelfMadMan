using System;
using System.Collections;
using UnityEngine;

public class FeelTheProtsInteractableManager : InteractableManager
{
    public event Action<bool> OnEndGame; 
    
    public bool IsHolding { get; set; }

    [SerializeField] private GameObject _bars;
    public GameObject Bars => _bars;
    [SerializeField] private SuccessBar _successBar;
    [SerializeField] private FailBar _failBar;

    private bool _lost;
    private bool _win;
    private void Start()
    {
        _lost = false;
        _successBar.OnParticleSuccess += HandleWin;
        _failBar.OnFail += HandleLose;
    }

    private void HandleWin()
    {
        if (IsHolding || _lost || _win) return;
        
        _win = true;
        OnEndGame?.Invoke(true);
    }

    private void HandleLose()
    {
        _lost = true;
        OnEndGame?.Invoke(false);
    }

    private void OnDestroy()
    {
        _successBar.OnParticleSuccess -= HandleWin;
        _failBar.OnFail -= HandleLose;
    }
}