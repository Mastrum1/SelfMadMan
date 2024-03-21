using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Canvas LIST")]
    [SerializeField] private List<CanvasGroup> _mCanvasGroup = new List<CanvasGroup>();

    public CanvasGroup CanvasGroup {  get => _mCurrentCanvas; set => _mCurrentCanvas = value; }
    [SerializeField] private CanvasGroup _mCurrentCanvas;

    // Links towards our socials medias
    private string _mTwitterURL = "https://twitter.com/selfmadman_";
    private string _mInstagramURL = "https://www.instagram.com/selfmadman_/";
    private string _mTiktokURL = "https://www.tiktok.com/@selfmadman._?_t=8kpgaAFiNd2&_r=1";
    private string _mYoutubeURL = "https://www.youtube.com/@Selfmadman_";

    public void SetAlpha(CanvasGroup canvasGroup)
    {
        if (!canvasGroup)
            return;
        else if (canvasGroup)
            foreach (var c in _mCanvasGroup)
            {
                c.alpha = 0;
                c.interactable = false;
                c.blocksRaycasts = false;
            }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        _mCurrentCanvas = canvasGroup;
    }

    public void ChangeLanguage()
    {
        // Localization package ?
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
}
