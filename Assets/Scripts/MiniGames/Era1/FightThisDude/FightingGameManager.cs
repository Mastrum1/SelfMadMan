using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MiniGameManager
{
    [SerializeField] private FightingUIManager _UIManager;
    [SerializeField] private ParticleSystem particlePrefab;
    [SerializeField] private GameObject targetParticle;
    [SerializeField] private GameObject _mPauseMenu;

    private AudioManager _audioManager;

    public void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    public void OnClicked()
    {
        if (_mPauseMenu.activeSelf == true)
        {
            Debug.Log("Nop");
        }
        else
        {
            _UIManager.Bar.AddValue(150 - (GameManager.instance.FasterLevel * 10));
            _UIManager.OnFightImageChange();
            Debug.Log("Clicked");
            SpawnParticle();
        }

        int randomIndex = UnityEngine.Random.Range(0, 4);

        _audioManager.PlaySFX(randomIndex);

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
