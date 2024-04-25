using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FurnitureManager;

public class CustomisationManager : MonoBehaviour
{
    public static CustomisationManager instance;

    [Header("FurnitureManager")]
    [SerializeField] private FurnitureManager _mFurnitureManager;

    public FurnitureSO[] FrameFurnitures { get => _mFrameFurnitures; set => _mFrameFurnitures = value; }
    [Header("FurnituresSO")]
    [SerializeField] private FurnitureSO[] _mFrameFurnitures;

    [Header("TemplatesGO")]
    [SerializeField] private List<CustomisationFrame> _mFrameTemplates;

    [SerializeField] private List<Sprite> _mBGButtons;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        LoadFur(_mFrameFurnitures);
    }

    public void LoadFur(FurnitureSO[] items)
    {
        for (int i = 0; i < items.Length; i++) 
        {
            foreach (var fur in _mFurnitureManager.FurnitureList)
            {
                if (items[i].FurniturePrefab.name == fur.PrefabParent.name)
                {
                    _mFrameTemplates[i].FurnitureSO = items[i];

                    if (!fur.Locked)
                    {
                        _mFrameTemplates[i].FurnitureImage.sprite = items[i].Icon;
                        _mFrameTemplates[i].BoxCollider.enabled = true;

                        if (fur.Picked)
                        {
                            _mFrameTemplates[i].BGButton.sprite = _mBGButtons[0];
                            _mFrameTemplates[i].ButtonText.text = "Picked";
                        }

                        else if (!fur.Picked)
                        {
                            _mFrameTemplates[i].BGButton.sprite = _mBGButtons[1];
                            _mFrameTemplates[i].ButtonText.text = "Pick";
                        }
                    }

                    else if (fur.Locked)
                    {
                        _mFrameTemplates[i].BoxCollider.enabled = false;
                        _mFrameTemplates[i].BGButton.sprite = _mBGButtons[2];
                        _mFrameTemplates[i].FurnitureImage.sprite = _mBGButtons[3];
                        _mFrameTemplates[i].ButtonText.text = "Locked";
                    }
                }
            }
        }
    }
}

