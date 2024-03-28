using UnityEngine;

public class VFXShrink : MonoBehaviour
{
    [SerializeField]
    private float shrinkSpeed = 0.1f; 

    private float initialWidth; 
    private Vector3 initialPosition; 

    private void Start()
    {
        initialWidth = transform.localScale.x;
        initialPosition = transform.position;
    }

    private void Update()
    {
        float newWidth = Mathf.Max(transform.localScale.x - shrinkSpeed * Time.deltaTime, 0f);

        float deltaWidth = initialWidth - newWidth;

        float offsetX = deltaWidth / 2f;

        transform.localScale = new Vector3(newWidth, transform.localScale.y, transform.localScale.z);

        transform.position = new Vector3(initialPosition.x + offsetX, initialPosition.y, initialPosition.z);

        UpdateMask(newWidth);
    }

    private void UpdateMask(float maskWidth)
    {
        GetComponent<Renderer>().material.SetFloat("_MaskWidth", maskWidth);
    }
}
