using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RizzHerHand : MonoBehaviour
{

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
        transform.localPosition = new Vector3(transform.localPosition.x - 340, transform.localPosition.y, transform.localPosition.z);
    }

    public void SwipeRight()
    {
        transform.localPosition = new Vector3(transform.localPosition.x + 340, transform.localPosition.y, transform.localPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.GetComponent<VFXScaleUp>().OnObjectClicked();
            OnGameEnd?.Invoke(false);
        }
    }
}
