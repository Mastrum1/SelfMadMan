using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimText : MonoBehaviour
{
    [SerializeField]
    private float minScale = 0.5f; // Minimum scale value
    [SerializeField]
    private float maxScale = 2f; // Maximum scale value
    [SerializeField]
    private float speed = 1f; // Scale change speed

    private bool scalingUp = true; // Flag to determine if currently scaling up

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the new scale value based on time and speed
        float scale = Mathf.Lerp(minScale, maxScale, Mathf.PingPong(Time.time * speed, 1f));

        // Apply the new scale to the GameObject
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
