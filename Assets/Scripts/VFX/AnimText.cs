using System.Collections;
using UnityEngine;

public class TextMovement : MonoBehaviour
{
    [SerializeField] private Transform[] targetObjects;
    [SerializeField] private float fastSpeed = 10f;
    [SerializeField] private float slowSpeed = 2f;

    void Start()
    {
        StartCoroutine(MoveToTarget(targetObjects[0], fastSpeed)); // Start moving towards the first target at fast speed
    }

    IEnumerator MoveToTarget(Transform target, float speed)
    {
        while (Vector2.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            yield return null;
        }

        // Check if the current target is not the last one
        if (System.Array.IndexOf(targetObjects, target) < targetObjects.Length - 1)
        {
            // Determine the speed for the next target based on its index
            float nextSpeed = (System.Array.IndexOf(targetObjects, target) % 2 == 0) ? slowSpeed : fastSpeed;

            // Move to the next target with the determined speed
            StartCoroutine(MoveToTarget(targetObjects[System.Array.IndexOf(targetObjects, target) + 1], nextSpeed));
        }
    }
}
