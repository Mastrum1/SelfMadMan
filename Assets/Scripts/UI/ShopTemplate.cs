using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopTemplate : MonoBehaviour, IPointerClickHandler
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


    public void OnPointerClick(PointerEventData eventData)
    {
        // Notify the ClickImageHandler that this template was clicked, passing its index
        ClickImageHandler clickHandler = FindObjectOfType<ClickImageHandler>();
        if (clickHandler != null && PurchaseBox)
        {
            clickHandler.OnTemplateClicked(_mIndex);
        }
    }
}
