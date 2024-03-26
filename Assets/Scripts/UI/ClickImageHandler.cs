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

    /*int GameObjectToIndex(GameObject targetObj)
    {
        ShopTemplate template = targetObj.GetComponent<ShopTemplate>();
        if (template != null)
        {
            // If it has the ShopTemplate component, return its index
            Debug.Log("Found ShopTemplate component on clicked object");
            return template.Index;
        }
        else
        {
            // If the clicked object doesn't have a ShopTemplate component, return -1
            Debug.Log("ShopTemplate component not found on clicked object");
            return -1;
        }
    }*/
}
