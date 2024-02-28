using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSleepClick : MonoBehaviour
{

    [SerializeField] SleepingGameManager gameManager;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        Debug.Log("clicked");
    }
}
