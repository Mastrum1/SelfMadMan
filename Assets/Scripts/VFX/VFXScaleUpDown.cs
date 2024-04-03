using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScaleUpDown : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 2f;
    public float speed = 1f;

    private bool scalingUp = true;

    // Update is called once per frame
    void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * speed, 1f));
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
