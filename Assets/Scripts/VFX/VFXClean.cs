using UnityEngine;

public class VFXClean : MonoBehaviour
{
    public Texture2D brushMask; // Texture mask indicating where to apply changes

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ToClean"))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = renderer.material;

                // Update material properties based on texture mask
                material.SetTexture("_MaskTexture", brushMask);
            }
        }
    }
}
