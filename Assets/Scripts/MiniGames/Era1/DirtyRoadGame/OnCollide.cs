using UnityEngine;
using System;
using System.Collections;

public class OnCollide : MonoBehaviour
{
    public event Action<bool> OnCollided;
    [SerializeField] private bool _win;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        if (gameObject.CompareTag("DirtyAd"))
        {
            transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);
            StartCoroutine(ScaleBack());
        }
        
        OnCollided?.Invoke(_win);
        
        if (_win)
        {
            col.gameObject.SetActive(false);
        }
    }

    IEnumerator ScaleBack()
    {
        yield return new WaitForSeconds(0.2f);
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
}
