using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RizzHerHand : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    public event Action<bool> OnGameEnd;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwipeLeft()
    {
        StartCoroutine(Movement(-340));
    }

    public void SwipeRight()
    {
        StartCoroutine(Movement(340));
    }

    private IEnumerator Movement(int dist)
    {
        Vector3 targetPos = new Vector3(transform.localPosition.x + dist, transform.localPosition.y, transform.localPosition.z);
        Debug.Log(targetPos);
        while (transform.localPosition != targetPos)
        {

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, moveSpeed * Time.deltaTime);

            yield return null;
        }
        Debug.Log("test");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.GetComponent<VFXScaleUp>().OnObjectClicked();
            OnGameEnd?.Invoke(false);
        }

        if (collision.gameObject.CompareTag("OtherHand"))
        {
            OnGameEnd?.Invoke(true);
        }
    }
}
