using System.Collections;
using UnityEngine;

public class VFXFadeObject : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Renderer rend;

    private void Start()
    {
        if (rend == null)
            rend = GetComponent<Renderer>();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        Color startColor = rend.material.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float t = elapsedTime / fadeDuration;
            rend.material.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        rend.material.color = targetColor;
    }
}
