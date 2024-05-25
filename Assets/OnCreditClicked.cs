using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCreditClicked : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        mySceneManager.instance.LoadWinScreen();
        mySceneManager.instance.LoadHomeScreen();
    }
}
