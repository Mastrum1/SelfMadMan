using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMovement : MonoBehaviour
{
    [SerializeField] private float _mSpeed;
    [SerializeField] private Vector3 _mEndPosition;
    [SerializeField] WheelRotation _mWheel1;
    [SerializeField] WheelRotation _mWheel2;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * _mSpeed;
        if (transform.position.x >= _mEndPosition.x)
            this.transform.gameObject.SetActive(false);
    }

    public void Stop()
    {
        _mSpeed = 0;
        _mWheel1.Stop();
        _mWheel2.Stop();
    }
}
