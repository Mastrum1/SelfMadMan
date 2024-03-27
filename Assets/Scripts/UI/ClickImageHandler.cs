using UnityEngine;
using UnityEngine.EventSystems;

public class ClickImageHandler : MonoBehaviour
{
    public void OnTemplateClicked(int index, Items[] items)
    {
        // Retrieve the cost of the ShopItem using the index
        int cost = items[index].Cost;

        // Call the purchase function with the index and cost
        ShopManager.Instance.PurchaseItem(items, index, cost);
    }
}
