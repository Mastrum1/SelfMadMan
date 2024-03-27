using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopTemplate : MonoBehaviour/*, IPointerClickHandler*/
{
    public TMP_Text TitleText { get => _mTitleText; set => _mTitleText = value; }
    [SerializeField] public TMP_Text _mTitleText;

    public TMP_Text CostText { get => _mCostText; set => _mCostText = value; }
    [SerializeField] public TMP_Text _mCostText;

    public Image ImageItem { get => _mImageItem; set => _mImageItem = value; }
    [SerializeField] private Image _mImageItem;

    public Image PurchaseBox { get => _mPurchaseBox; set => _mPurchaseBox = value; }
    [SerializeField] private Image _mPurchaseBox;

    public int Index { get => _mIndex; set => _mIndex = value; }
    [SerializeField] private int _mIndex;

    public int Cost { get => _mCost; set => _mCost = value; }
    [SerializeField] private int _mCost;

    public string Type { get => _mType; set => _mType = value; }
    [SerializeField] private string _mType;


    public void OnPointerClick()
    { // Get the parent of the clicked object
        Transform parent = transform.parent;

        // Find the index of the clicked object within its parent
        int index = -1;
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i) == transform)
            {
                index = i;
                break;
            }
        }

        if (index != -1)
        {
            // Determine the correct list based on the clicked object's type
            Items[] itemList = null;

            switch (Type)
            {
                case "MadCoins":
                    itemList = FindObjectOfType<ShopManager>().ShopItems;
                    break;
                case "Furnitures":
                    itemList = FindObjectOfType<ShopManager>().Furnitures;
                    break;
                case "PowerUp":
                    itemList = FindObjectOfType<ShopManager>().PowerUp;
                    break;
                default:
                    Debug.LogError("Unknown item type for clicked object: " + Type);
                    return;
            }
            
            // Call the OnTemplateClicked method with the correct index and item list
            FindObjectOfType<ShopClickImageHandler>().OnTemplateClicked(index, itemList);
        }
        else
        {
            Debug.LogError("Failed to find index of clicked object within its parent.");
        }

    }
}
