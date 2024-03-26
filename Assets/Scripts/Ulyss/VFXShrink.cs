using UnityEngine;

public class VFXShrink : MonoBehaviour
{
    [SerializeField]
    private float shrinkSpeed = 0.1f; // Speed at which the rectangle shrinks

    private float initialWidth; // Initial width of the rectangle
    private Vector3 initialPosition; // Initial position of the rectangle

    private void Start()
    {
        // Store the initial width and position of the rectangle
        initialWidth = transform.localScale.x;
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Calculate the new width based on the shrink speed
        float newWidth = Mathf.Max(transform.localScale.x - shrinkSpeed * Time.deltaTime, 0f);

        // Calculate the change in width
        float deltaWidth = initialWidth - newWidth;

        // Calculate the offset for position adjustment
        float offsetX = deltaWidth / 2f;

        // Update the scale
        transform.localScale = new Vector3(newWidth, transform.localScale.y, transform.localScale.z);

        // Offset the position to maintain the right side fixed
        transform.position = new Vector3(initialPosition.x + offsetX, initialPosition.y, initialPosition.z);

        // Update the mask
        UpdateMask(newWidth);
    }

    private void UpdateMask(float maskWidth)
    {
        // Set the mask width in the shader
        GetComponent<Renderer>().material.SetFloat("_MaskWidth", maskWidth);
    }
}
