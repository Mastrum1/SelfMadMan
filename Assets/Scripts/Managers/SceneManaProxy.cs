using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManaProxy : MonoBehaviour
{
    [SerializeField] string SceneName;

    public void ChangeScene()
    {
        mySceneManager.instance.SetScene(SceneName);
        Debug.Log(SceneName);
    }

    public void RandomGameChoice(int era)
    {
        switch (era)
        {
            case 0:
                SceneName = MiniGameSelector.GetRandomElement<string>(MiniGameSelector.instance.Era1);
                ChangeScene();
                break;
            case 1:
                SceneName = MiniGameSelector.GetRandomElement<string>(MiniGameSelector.instance.Era2);
                ChangeScene();
                break;
            case 2:
                SceneName = MiniGameSelector.GetRandomElement<string>(MiniGameSelector.instance.Era3);
                ChangeScene();
                break;
            default:
                Debug.Log("Choose 1, 2 or 3");
                break;
        }
    }
}