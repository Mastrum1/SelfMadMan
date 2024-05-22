using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectShotInteractableManager : InteractableManager
{
    // Start is called before the first frame update
    [SerializeField] private Tower _tower;
    public event Action<bool> OnGameEnded;
    [SerializeField] private RectTransform _PauseButton;
    public void StopTower(Vector3 fingerPos)
    {
        Vector3 PauseButtonPos = RectTransformUtility.WorldToScreenPoint(Camera.main, _PauseButton.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(PauseButtonPos.x, PauseButtonPos.y, 0));

        if (fingerPos.x > worldPos.x - 0.6f && fingerPos.x < worldPos.x + 0.6f && fingerPos.y < worldPos.y + 0.6f && fingerPos.y > worldPos.y - 0.6f)
            return;
        else
        {
            _tower.Rotating = false;
            if (_tower.CurrentRotation >= -5f && _tower.CurrentRotation <= 1)
                OnGameEnded(true);
            else
                OnGameEnded(false);
        }
    }
}
