using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommentMovement : MonoBehaviour
{
    [SerializeField] private float _mSpeed = 1.5f;
    private float _mGapFillSpeed = 0.75f;
    void Start()
    {
    }

    void Update()
    {
        transform.Translate(Vector3.up * _mSpeed * Time.deltaTime);
    }

    public void Stop()
    {
        _mSpeed = 0;
    }

    public void MoveFaster(Vector3 position, int times = 1) 
    {
        if (transform.position.y < position.y) {
            _mSpeed += (_mGapFillSpeed * times);
            StartCoroutine(ResetSpeed(times));
        }
    }

    IEnumerator  ResetSpeed(int times)
    {
        yield return new WaitForSeconds(0.8f);
        _mSpeed -= (_mGapFillSpeed * times);
    }
}
