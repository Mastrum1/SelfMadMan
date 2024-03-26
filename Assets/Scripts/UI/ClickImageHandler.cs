using UnityEngine;
using UnityEngine.EventSystems;

public class ClickImageHandler : MonoBehaviour
{
    public void Purchase(int index, int cost)
    {
        // Call OnImageClicked with the correct index
        ShopManager.Instance.OnImageClicked(index, cost);
    }

    public void OnTemplateClicked(int index)
    {
        // Retrieve the cost of the ShopItem using the index
        int cost = ShopManager.Instance.ShopItems[index].Cost;

        // Call the purchase function with the index and cost
        ShopManager.Instance.PurchaseItem(index, cost);
    }
}
