using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblesLifeTime : MonoBehaviour
{
    [SerializeField] float _mLifeTime = 0.25f;
    public void InitBubble()
    {
        StartCoroutine(DestroyBubble());
    }

    IEnumerator DestroyBubble()
    {
        yield return new WaitForSeconds(_mLifeTime);
        this.gameObject.SetActive(false);
    }

}
