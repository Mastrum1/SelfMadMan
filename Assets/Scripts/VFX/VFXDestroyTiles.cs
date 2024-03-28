using UnityEngine;

public class VFXDestroyTiles : MonoBehaviour
{
    [SerializeField]
    private GameObject Foam; // GameObject to instantiate with 20% chance

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an object tagged "ToClean"
        if (collision.gameObject.CompareTag("ToClean"))
        {
            Debug.Log("Destroying " + collision.gameObject.name);

            if (Random.value < 0.05f && Foam != null)
            {
                Debug.Log("Instantiating " + Foam.name);
                Instantiate(Foam, collision.transform.position, Quaternion.identity);
            }

            Destroy(collision.gameObject);
        }
    }
}
