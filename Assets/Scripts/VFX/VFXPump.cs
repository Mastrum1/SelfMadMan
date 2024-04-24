using System.Collections;
using UnityEngine;

public class VFXPump : MonoBehaviour
{
    [SerializeField] private float scaleFactor = 1.5f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private GameObject prefabToInstantiate;

    private SpriteRenderer spriteRenderer;
    private bool hasStartedCoroutine = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the coroutine hasn't started yet
        if (!hasStartedCoroutine)
        {
            // Start the coroutine
            StartCoroutine(SpawnAndAnimate());
            hasStartedCoroutine = true;
        }
    }

    private IEnumerator SpawnAndAnimate()
    {
        // Instantiate a new object at the same position
        GameObject newObject = Instantiate(prefabToInstantiate, transform.position, Quaternion.identity);
        SpriteRenderer newSpriteRenderer = newObject.GetComponent<SpriteRenderer>();

        // Set the sprite to be the same as the original
        newSpriteRenderer.sprite = spriteRenderer.sprite;

        // Fade out and scale up
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float scaleFactorThisFrame = Mathf.Lerp(1f, scaleFactor, timer / fadeDuration);
            newObject.transform.localScale = Vector3.one * scaleFactorThisFrame;

            Color newColor = newSpriteRenderer.color;
            newColor.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            newSpriteRenderer.color = newColor;

            timer += Time.deltaTime;
            yield return null;
        }

        // Clean up
        Destroy(newObject);
    }
}
