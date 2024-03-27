using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweetMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _mSpeed = 2;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _mSpeed * Time.deltaTime);
    }

    public void Stop()
    {
        _mSpeed = 0;
    }
}
