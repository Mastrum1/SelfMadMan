using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSleepClick : MonoBehaviour
{

    [SerializeField] FightingGame gameManager;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        gameManager.OnClicked();
    }
}
