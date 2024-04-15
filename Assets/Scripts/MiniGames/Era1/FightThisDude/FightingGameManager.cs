using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingGame : MiniGameManager
{
    [SerializeField] private FightingUIManager _UIManager;
    [SerializeField] private GameObject LensEffect;
    [SerializeField] private ParticleSystem particlePrefab;

    private Material lensMaterial;

    private void Start()
    {
        Renderer renderer = LensEffect.GetComponent<Renderer>();
        if (renderer != null)
        {
            lensMaterial = renderer.material;
        }
        else
        {

        }
    }

    public void OnClicked()
    {
        _UIManager.Bar.AddValue(150 - (GameManager.instance.FasterLevel * 20));
        Debug.Log("Clicked");
        SpawnParticle(Input.mousePosition);
    }

    private void SpawnParticle(Vector3 tapPosition)
    {
        if (particlePrefab != null)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(tapPosition.x, tapPosition.y, 10f)); // Assuming z = 10f is the desired distance from camera

            Instantiate(particlePrefab, worldPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No prefab uWu!");
        }
    }

    private void SpawnParticle()
    {
        if (particlePrefab != null)
        {
            Instantiate(particlePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No prefab ? wtf");
        }

        if(_gameIsPlaying)
        {
            _UIManager.OnFightImageChange();
            _UIManager.Bar.AddValue(150 - (GameManager.instance.FasterLevel * 20));
            Debug.Log("Clicked");
        }
    }

    public override void Update()
    {
        base.Update();
        if (_gameIsPlaying)
        {
            float ratio = _UIManager.Bar.barValue / _UIManager.Bar.maxBarValue;
            float invertedRatio = 1f - ratio;
            float valueInRange = invertedRatio * 4f;

            if (lensMaterial != null)
            {
                lensMaterial.SetFloat("_Float", valueInRange);
            }

            if (_UIManager.Bar.barValue >= _UIManager.Bar.maxBarValue)
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
