using UnityEngine;
using System.Collections;
public class VFXPump : MonoBehaviour
{
    // Speed of scaling and fading
    public float scaleSpeed = 1f;
    public float alphaSpeed = 1f;

    // Reference to the parent's SpriteRenderer
    private SpriteRenderer parentSpriteRenderer;

    void OnEnable()
    {
        // Get the parent's SpriteRenderer component
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            parentSpriteRenderer = parentTransform.GetComponent<SpriteRenderer>();

            if (parentSpriteRenderer != null)
            {
                // Copy the parent's sprite
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = parentSpriteRenderer.sprite;
                    spriteRenderer.color = parentSpriteRenderer.color;

                    // Start scaling and fading
                    StartCoroutine(ScaleAndFade());
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

    IEnumerator ScaleAndFade()
    {
        // Initial scale
        transform.localScale = Vector3.one;

        // Initial alpha
        Color targetColor = parentSpriteRenderer.color;
        targetColor.a = 0f;

        // Lerp scale and alpha
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * scaleSpeed;

            // Lerp scale to 2
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2f, t);

            // Lerp alpha from 1 to 0
            Color newColor = Color.Lerp(parentSpriteRenderer.color, targetColor, t);
            GetComponent<SpriteRenderer>().color = newColor;

            yield return null;
        }

        // Disable the object when alpha is 0
        gameObject.SetActive(false);
    }
}
