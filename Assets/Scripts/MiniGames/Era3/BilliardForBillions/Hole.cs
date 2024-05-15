using System;
using System.Collections;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public event Action<bool> OnCueBall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("8Ball")) return;
        //if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 2) return;
        
        GameObject o;
        (o = other.gameObject).transform.position = transform.position;
        o.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        o.transform.GetChild(0).gameObject.SetActive(false);
        o.transform.GetChild(1).gameObject.SetActive(false);
        StartCoroutine(CueBallAnim(o));
        
        OnCueBall?.Invoke(true);
    }
    
    private IEnumerator CueBallAnim(GameObject cueBall)
    {
        while (cueBall.activeSelf)
        {
            cueBall.transform.localScale -= new Vector3(0.2f * Time.deltaTime, 0.2f * Time.deltaTime);
            if (cueBall.transform.localScale.x < 0.01f)
            {
                cueBall.SetActive(false);
            }
            yield return null;
        }
    }
}
