using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScene : MonoBehaviour
{
    private void Awake()
    {
        SceneMana.instance.Invoke("NextGame", 5);
        GameManager.instance.AddCurrent();
    }
}
