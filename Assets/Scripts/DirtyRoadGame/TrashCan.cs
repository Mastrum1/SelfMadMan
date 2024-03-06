using UnityEngine;

public class TrashCan : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            print("Win");
            //send event to gamemanager
        }
    }
}
