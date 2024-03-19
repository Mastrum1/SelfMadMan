using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("PANEL LIST")]
    [SerializeField] private List<GameObject> _mPanels = new List<GameObject>();

    [Header("BUTTON LIST")]
    [SerializeField] private List<GameObject> _mButtons = new List<GameObject>();

    public void DesactivateGO(GameObject panel)
    {
        panel.SetActive(false);
    }
}
