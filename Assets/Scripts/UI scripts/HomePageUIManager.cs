using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HomePageUIManager : MonoBehaviour
{
    [Header("GameObject LIST")]
    [SerializeField] private List<GameObject> _mMenuUI = new List<GameObject>();

    // Links towards our socials medias
    private string _mTwitterURL = "https://twitter.com/selfmadman_";
    private string _mInstagramURL = "https://www.instagram.com/selfmadman_/";
    private string _mTiktokURL = "https://www.tiktok.com/@selfmadman._?_t=8kpgaAFiNd2&_r=1";
    private string _mYoutubeURL = "https://www.youtube.com/@Selfmadman_";
    private string _mInternetSite = "https://selfmadman.fr/";

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
        if (obj.activeSelf == true)
            obj.SetActive(false);
        else if (obj.activeSelf == false)
            obj.SetActive(true);  
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
            case "InternetSite":
                Application.OpenURL(_mInternetSite);
                break;
            default:
                return;
        }
    }

    public void ChangeLanguage(Toggle toggle)
    {
        if (toggle.isOn)
            LocalSelector.Instance.ChangeLocale(1);
        else if (!toggle.isOn)
            LocalSelector.Instance.ChangeLocale(0);
    }
}
