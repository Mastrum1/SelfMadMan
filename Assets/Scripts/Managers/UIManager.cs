using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("PANEL LIST")]
    [SerializeField] private List<GameObject> _mPanels = new List<GameObject>();

    [Header("BUTTON LIST")]
    [SerializeField] private List<GameObject> _mButtons = new List<GameObject>();

    public void SetAlpha(GameObject panel)
    {
        if (!panel.GetComponent<CanvasGroup>())
            return;
        else if (panel.GetComponent<CanvasGroup>())
            foreach (var c in _mPanels)
            {
                c.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                c.gameObject.GetComponent<CanvasGroup>().interactable = false;
                c.gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        panel.GetComponent<CanvasGroup>().alpha = 1;
        panel.GetComponent<CanvasGroup>().interactable = true;
        panel.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void ChangeLanguage()
    {
        
    }
}
