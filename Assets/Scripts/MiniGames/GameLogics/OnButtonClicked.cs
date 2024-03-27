using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnButtonClicked : MonoBehaviour
{
    public string SceneToLoad;

    public void OnClicked()
    {
        ChangeScene(SceneToLoad);
    }
    public void ChangeScene(string name)
    {
        LoadingScreen.Instance.LoadScene(name);
    }
}
