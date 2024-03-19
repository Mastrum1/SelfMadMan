using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] public Vector3 EndPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * Speed;
        if (transform.position.x >= EndPosition.x)
            this.transform.gameObject.SetActive(false);
    }

    public void Stop()
    {
        Speed = 0;
    }
}
