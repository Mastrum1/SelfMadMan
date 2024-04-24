using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpBrick : MonoBehaviour
{
    [SerializeField] private float scaleUpDuration = 0.1f;
    private Coroutine _mCoroutine;
    public Coroutine Coroutine { get => _mCoroutine; set => _mCoroutine = value; }
    public void BrickSelected()
    {
        StartCoroutine(ScaleBrick());
    }

    public void BrickUnselected()
    {
        StartCoroutine(ScaleBrickDown());
    }

    public void StopTheCoroutine()
    {
        StopCoroutine(ScaleBrick());
        StopCoroutine(ScaleBrickDown());
    }

    public void SetScale()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    public IEnumerator ScaleBrick()
    {
        // Scale up to 1.3
        float timer = 0;
        Vector3 targetScale = gameObject.transform.localScale * 1.2f;

        while (timer < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(gameObject.transform.localScale, targetScale, timer / scaleUpDuration);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator ScaleBrickDown()
    {
        // Scale up to 1.3
        float timer = 0;
        Vector3 targetScale = new Vector3(1,1,1);

        while (timer < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(gameObject.transform.localScale, targetScale, timer / scaleUpDuration);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}