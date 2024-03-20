using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MinigamesButton : MonoBehaviour
{
    [SerializeField] TMP_Dropdown _mEraChoice;
    public void GoToMinigame()
    {
        string choice = _mEraChoice.options[_mEraChoice.value].text;
        if (!mySceneManager.instance)
            SceneManager.LoadScene(choice);
        else
            mySceneManager.instance.SetScene(choice);
    }
}
