using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("GameObject LIST")]
    [SerializeField] private List<GameObject> _mMenuUI = new List<GameObject>();

    /*public CanvasGroup CurrentCanvas {  get => _mCurrentCanvas; set => _mCurrentCanvas = value; }
    [Header("Current Canvas")]
    [SerializeField] private GameObject _mCurrentGame;*/

    // Links towards our socials medias
    private string _mTwitterURL = "https://twitter.com/selfmadman_";
    private string _mInstagramURL = "https://www.instagram.com/selfmadman_/";
    private string _mTiktokURL = "https://www.tiktok.com/@selfmadman._?_t=8kpgaAFiNd2&_r=1";
    private string _mYoutubeURL = "https://www.youtube.com/@Selfmadman_";

    public void SetOnOff(GameObject obj)
    {
        if (!obj)
            return;
        else if (obj)
            foreach (var c in _mMenuUI)
            {
                c.SetActive(false);
            }
        obj.SetActive(true);
    }

    public void SingleSetOnOff(GameObject obj)
    {
        if (!obj)
            return;
        if (obj.active == true)
            obj.SetActive(false);
        else if (obj.active == false)
            obj.SetActive(true);
    }

    public void ChangeLanguage()
    {
        //
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
            case "Youtube":
                Application.OpenURL(_mYoutubeURL);
                break;
            default:
                return;
        }
    }

    public void Toggle(Toggle toggle)
    {
        if (toggle.isOn)
            LocalSelector.Instance.ChangeLocale(1);
        else if (!toggle.isOn)
            LocalSelector.Instance.ChangeLocale(0);
    }
}
