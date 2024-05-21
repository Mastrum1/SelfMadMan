using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.SceneManagement;

public class HomePageUIManager : MonoBehaviour
{
    private void Start()
    {
        if (TutorialManager.instance.InTutorial && (TutorialManager.instance.StepNbr == 0 || TutorialManager.instance.StepNbr == 2))
        {

            TutorialManager.instance.SetCamera(Camera);
            TutorialManager.instance.StepInit();
        }

    }

    [SerializeField] private Camera Camera;
    [SerializeField] private SerializedDictionary<GameObject, bool> _mMenuUI;
    public SerializedDictionary<GameObject, bool> MenuUIDictionnary { get => _mMenuUI; set => _mMenuUI = value; }

    [Header("ButtonsList")]
    [SerializeField] private List<BoxCollider2D> _mIcons = new List<BoxCollider2D>();

    // Links towards our socials medias
    private string _mTwitterURL = "https://twitter.com/selfmadman_";
    private string _mInstagramURL = "https://www.instagram.com/selfmadman_/";
    private string _mTiktokURL = "https://www.tiktok.com/@selfmadman._?_t=8kpgaAFiNd2&_r=1";
    private string _mLinkedInURL = "https://www.linkedin.com/in/chad-motivation-1930ab2b7/";
    private string _mInternetSite = "https://selfmadman.fr/";

    public void SetOnOff(GameObject GameObject)
    {


        if (TutorialManager.instance.InTutorial && (TutorialManager.instance.StepNbr == 5))
        {
            Debug.Log("Haha");
            TutorialManager.instance.FinalStep();
        }
        if (_mMenuUI[GameObject])
        {
            if (!GameObject)
                return;
            else if (GameObject)
                foreach (var c in _mMenuUI)
                {
                    c.Key.SetActive(false);
                }
            GameObject.SetActive(true);
        }
    }

    public void SingleSetOnOff(GameObject obj)
    {
        if (!obj)
            return;
        if (obj.activeSelf == true)
        {
            foreach (var c in _mIcons)
            {
                c.enabled = true;
            }

            obj.SetActive(false);
        }
        else if (obj.activeSelf == false)
        {
            foreach (var c in _mIcons)
            {
                c.enabled = false;
            }
            obj.SetActive(true);
        }
    }

    public void Redirection(string platform)
    {
        switch (platform)
        {
            case "Tiktok":
                Application.OpenURL(_mTiktokURL);
                break;
            case "Twitter":
                Application.OpenURL(_mTwitterURL);
                break;
            case "Insta":
                Application.OpenURL(_mInstagramURL);
                break;
            case "LinkedIn":
                Application.OpenURL(_mLinkedInURL);
                break;
            case "InternetSite":
                Application.OpenURL(_mInternetSite);
                break;
            default:
                return;
        }
    }

    public void DataReset()
    {
        GameManager.instance.Player.DataPlayer.FirstSaveData(GameManager.instance.Player);
        SceneManager.LoadScene(1);
    }
}
