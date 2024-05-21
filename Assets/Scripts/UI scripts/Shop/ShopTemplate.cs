using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopTemplate : MonoBehaviour
{
    public TMP_Text TitleText { get => _mTitleText; private set => _mTitleText = value; }
    [SerializeField] public TMP_Text _mTitleText;

    public TMP_Text CostText { get => _mCostText; private set => _mCostText = value; }
    [SerializeField] public TMP_Text _mCostText;

    public TMP_Text Amount { get => _mAmount; private set => _mAmount = value; }    
    [SerializeField] private TMP_Text _mAmount;

    public Image ImageItem { get => _mImageItem; private set => _mImageItem = value; }
    [SerializeField] private Image _mImageItem;

    public Sprite Nothing { get => _mNothingImage; private set => _mNothingImage = value; }
    [SerializeField] private Sprite _mNothingImage;

    public Image PurchaseBox { get => _mPurchaseBox; private set => _mPurchaseBox = value; }
    [SerializeField] private Image _mPurchaseBox;

    public Sprite PurchaseBoxON { get => _mPurchaseBoxON; private set => _mPurchaseBoxON = value; }
    [SerializeField] private Sprite _mPurchaseBoxON;

    public Sprite PurchaseBoxOFF { get => _mPurchaseBoxOff; private set => _mPurchaseBoxOff = value; }
    [SerializeField] private Sprite _mPurchaseBoxOff;

    public ItemsSO.TYPE Type { get => _mType; set => _mType = value; }
    [SerializeField] private ItemsSO.TYPE _mType;

    public BoxCollider2D TemplateBox { get => _mTemplateBox; private set => _mTemplateBox = value; }
    [SerializeField] private BoxCollider2D _mTemplateBox;

    public ItemsSO ItemInfo { get => _mItemSO; set => _mItemSO = value; }
    [SerializeField] private ItemsSO _mItemSO;

    public bool Purchasable;

    private void OnEnable()
    {
        ONOFF();
    }

    public void ONOFF()
    {
        if (Purchasable)
        {
            _mPurchaseBox.sprite = PurchaseBoxON;
            _mTemplateBox.enabled = true;
            _mCostText.color = new Color(246, 243, 209, 255);
        }
        else if (!Purchasable)
        {
            _mPurchaseBox.sprite = PurchaseBoxOFF;
            _mTemplateBox.enabled = false;
            _mCostText.color = new Color(171, 170, 165, 255);
        }
    }

    private int TextToInt(TMP_Text tmpText)
    {
        int result = 0;
        if (tmpText != null)
        {
            int.TryParse(tmpText.text, out result);
        }
        return result;
    }
}
