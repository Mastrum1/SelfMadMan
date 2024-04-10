using UnityEngine;

public class VFXDestroyTiles : MonoBehaviour
{
    [SerializeField]
    private GameObject Foam; // GameObject to instantiate with 2% chance
    [SerializeField]
    private GameObject Bubbles; // Additional particle system to instantiate alongside Foam

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an object tagged "ToClean"
        if (collision.gameObject.CompareTag("ToClean"))
        {
            //Debug.Log("Destroying " + collision.gameObject.name);

            if (Random.value < 0.02f && Foam != null)
            {
                Debug.Log("Instantiating " + Foam.name);
                GameObject foamInstance = Instantiate(Foam, collision.transform.position, Quaternion.identity);
                // Instantiate additional particle system if available
                if (Bubbles != null)
                {
                    Instantiate(Bubbles, collision.transform.position, Quaternion.identity);
                }
            }
            //Destroy(collision.gameObject);
        }
    }
}
