using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MiniGameManager
{
    [SerializeField] private FightingUIManager _UIManager;
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private GameObject targetParticle;


    public void OnClicked()
    {
        _UIManager.Bar.AddValue(150 - (GameManager.instance.FasterLevel * 20));
        _UIManager.OnFightImageChange();
        Debug.Log("Clicked");
        SpawnParticle();
    }

    private void SpawnParticle()
    {
        if (particlePrefab != null)
        {
            Vector3 targetPosition = targetParticle.transform.position;

            Instantiate(particlePrefab, targetPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No prefab uWu!");
        }
    }

    public override void Update()
    {
        base.Update();
        if (_gameIsPlaying)
        {
            if (_UIManager.Bar.barValue == 0)
            {
                Amount++;
                EndMiniGame(true, miniGameScore);
            }
            if (_UIManager.Bar.barValue == _UIManager.Bar.maxBarValue)
            {
                EndMiniGame(false, miniGameScore);
            }
        }
    }
}
