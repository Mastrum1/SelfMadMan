using UnityEngine;
using System.Collections;

public class VFXPump : MonoBehaviour
{
    void Start()
    {
        // Get the parent transform
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            // Set the local scale of this object to match the parent's local scale
            transform.localScale = parentTransform.localScale;

            // Set this object's scale to (1, 1, 1)
            transform.localScale = Vector3.one;

            // Get the parent's SpriteRenderer component
            SpriteRenderer parentSpriteRenderer = parentTransform.GetComponent<SpriteRenderer>();

            if (parentSpriteRenderer != null)
            {
                // Get the SpriteRenderer component of this object
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    // Apply the parent's color to this object's SpriteRenderer
                    spriteRenderer.color = parentSpriteRenderer.color;
                }
                else
                {
                    Debug.LogWarning("SpriteRenderer component not found on object: " + name);
                }
            }
            else
            {
                Debug.LogWarning("SpriteRenderer component not found on parent object: " + parentTransform.name);
            }
        }
        else
        {
            Debug.LogWarning("No parent object found for: " + name);
        }
    }
}
