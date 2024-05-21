using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SceneManaProxy : MonoBehaviour
{
    List<string> _mMinigameNames = new List<string>();

    [Scene] public string SceneName;

    [SerializeField] private RatingPopup _RatingPopup;

    public void ChangeScene()
    {
        if (!mySceneManager.instance)
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        else if (mySceneManager.instance != null)
            mySceneManager.instance.SetScene(SceneName, mySceneManager.LoadMode.ADDITIVE);

    }

    public void StartGame()
    {
        GameManager.instance.OnGameStart();
        GameManager.instance.FirstGamePlayed = true;
    }

    public void RestartGame()
    {
        GameManager.instance.OnRestart();
    }

    // When mySceneManager is not avaible
    public void MinigamesChoice()
    {
        SceneManager.LoadScene("MinigamesChoice", LoadSceneMode.Single);
    }
}