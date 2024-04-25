using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomisationFrame : MonoBehaviour
{
    public Image FurnitureImage { get => _mFurnitureImage; set => _mFurnitureImage = value; }
    [SerializeField] private Image _mFurnitureImage;

    public Image BGButton { get => _mBGButton; set => _mBGButton = value; }
    [SerializeField] private Image _mBGButton;
    
    public TMP_Text ButtonText { get => _mButtonText; set => _mButtonText = value; }
    [SerializeField] private TMP_Text _mButtonText;

    public BoxCollider2D BoxCollider { get => _mBoxCollider; set => _mBoxCollider = value; }
    [SerializeField] private BoxCollider2D _mBoxCollider;

    public FurnitureSO FurnitureSO;

    public void OnClick()
    {
        if (FurnitureSO != null)
        {
            FurnitureManager.instance.PickFurniture(FurnitureSO.FurniturePrefab.name);
            CustomisationManager.instance.LoadFur(CustomisationManager.instance.FrameFurnitures);
        }
    }
}
