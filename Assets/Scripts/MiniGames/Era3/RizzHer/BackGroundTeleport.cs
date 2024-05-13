using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundTeleport : MonoBehaviour
{
    [SerializeField] private GameObject _previous;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BackGround"))
        {
            Vector3 pos = collision.GetComponent<BackGroundScrolling>().Previous.transform.position;
            collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, pos.y + 2686, collision.gameObject.transform.position.z);
        }
    }
}
