using UnityEngine;

public class VFXDestroyTiles : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an object tagged "ToClean"
        if (collision.gameObject.CompareTag("ToClean"))
        {
            Debug.Log("g");
            Destroy(collision.gameObject);
        }
    }
}
