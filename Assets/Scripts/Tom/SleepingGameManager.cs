using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingGameManager : MonoBehaviour
{
    [SerializeField] SleepingUIManager UIManager;

    public void OnClicked()
    {
        UIManager.ChangeSlider();
    }

    
}
