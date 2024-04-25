using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FurnitureManager;

public class CustomisationManager : MonoBehaviour
{
    [Header("FurnitureManager")]
    [SerializeField] private FurnitureManager _mFurnitureManager;

    [Header("FurnituresSO")]
    [SerializeField] private FurnitureSO[] _mFrameFurnitures;
    [SerializeField] private FurnitureSO[] _mStatuesFurnitures;
    [SerializeField] private FurnitureSO[] _mObjectsFurnitures;

    [Header("TemplatesGO")]
    [SerializeField] private List<CustomisationFrame> _mFrameTemplates;
    [SerializeField] private List<CustomisationFrame> _mStatueTemplates;
    [SerializeField] private List<CustomisationFrame> _mObjectTemplates;

    [SerializeField] private List<Sprite> _mBGButtons;

    private void Start()
    {
        LoadFur(_mFrameFurnitures);
        LoadFur(_mStatuesFurnitures);
        LoadFur(_mObjectsFurnitures);
    }

    private void LoadFur(ItemsSO[] items)
    {
        for (int i = 0; i < items.Length; i++) 
        { 
            if (items[i].Type == ItemsSO.TYPE.FURNITURE)
            {
                FurnitureSO fur = items[i] as FurnitureSO;

                CheckLocked(items);

                if (fur.FurnitureType == FurnitureManager.FurnitureType.FRAME)
                {
                    _mFrameTemplates[i].FurnitureImage.sprite = fur.Icon;
                }

                else if (fur.FurnitureType == FurnitureManager.FurnitureType.STATUE)
                {
                    _mStatueTemplates[i].FurnitureImage.sprite = fur.Icon;
                }

                else if (fur.FurnitureType == FurnitureManager.FurnitureType.OBJECT)
                {
                    _mObjectTemplates[i].FurnitureImage.sprite = fur.Icon;
                }

            }
        }
    }

    private void CheckLocked(ItemsSO[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Furniture furniture = _mFurnitureManager.FurnitureList[i];

            FurnitureSO fur = items[i] as FurnitureSO;

            if (furniture.Unlocked)
            {
                if (furniture.Picked)
                {
                    _mFrameTemplates[i].BoxCollider.enabled = true;
                    _mStatueTemplates[i].BoxCollider.enabled = true;
                    _mObjectTemplates[i].BoxCollider.enabled = true;

                    if (fur.FurnitureType == FurnitureManager.FurnitureType.FRAME)
                    {
                        _mFrameTemplates[i].BGButton.sprite = _mBGButtons[0];
                        _mFrameTemplates[i].ButtonText.text = "Picked";
                    }

                    else if (fur.FurnitureType == FurnitureManager.FurnitureType.STATUE)
                    {
                        _mStatueTemplates[i].BGButton.sprite = _mBGButtons[0];
                        _mStatueTemplates[i].ButtonText.text = "Picked";
                    }

                    else if (fur.FurnitureType == FurnitureManager.FurnitureType.OBJECT)
                    {
                        _mObjectTemplates[i].BGButton.sprite = _mBGButtons[0];
                        _mObjectTemplates[i].ButtonText.text = "Picked";
                    }
                }
                else if (!furniture.Picked)
                {
                    if (fur.FurnitureType == FurnitureManager.FurnitureType.FRAME)
                    {
                        _mFrameTemplates[i].BGButton.sprite = _mBGButtons[1];
                        _mFrameTemplates[i].ButtonText.text = "Pick";
                    }

                    else if (fur.FurnitureType == FurnitureManager.FurnitureType.STATUE)
                    {
                        _mStatueTemplates[i].BGButton.sprite = _mBGButtons[1];
                        _mStatueTemplates[i].ButtonText.text = "Pick";
                    }

                    else if (fur.FurnitureType == FurnitureManager.FurnitureType.OBJECT)
                    {
                        _mObjectTemplates[i].BGButton.sprite = _mBGButtons[1];
                        _mObjectTemplates[i].ButtonText.text = "Pick";
                    }
                }
            }
            else if (!furniture.Unlocked)
            {
                _mFrameTemplates[i].BoxCollider.enabled = false;
                _mStatueTemplates[i].BoxCollider.enabled = false;
                _mObjectTemplates[i].BoxCollider.enabled = false;

                if (items[i].Type == ItemsSO.TYPE.FURNITURE)
                {

                    if (fur.FurnitureType == FurnitureManager.FurnitureType.FRAME)
                    {
                        Debug.Log(_mFrameTemplates[i].FurnitureImage.sprite);
                        _mFrameTemplates[i].BGButton.sprite = _mBGButtons[2];
                        _mFrameTemplates[i].FurnitureImage.sprite = _mBGButtons[3];
                        _mFrameTemplates[i].ButtonText.text = "Locked";
                    }

                    else if (fur.FurnitureType == FurnitureManager.FurnitureType.STATUE)
                    {
                        _mStatueTemplates[i].BGButton.sprite = _mBGButtons[2];
                        _mStatueTemplates[i].FurnitureImage.sprite = _mBGButtons[3];
                        _mStatueTemplates[i].ButtonText.text = "Locked";
                    }

                    else if (fur.FurnitureType == FurnitureManager.FurnitureType.OBJECT)
                    {
                        _mObjectTemplates[i].BGButton.sprite = _mBGButtons[2];
                        _mObjectTemplates[i].FurnitureImage.sprite = _mBGButtons[3];
                        _mObjectTemplates[i].ButtonText.text = "Locked";
                    }
                } 
            }
        }   
    }
}

