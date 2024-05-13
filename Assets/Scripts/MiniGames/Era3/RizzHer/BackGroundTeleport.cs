using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundTeleport : MonoBehaviour
{

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
            Vector3 pos = collision.GetComponent<BackGroundScrolling>().Previous.transform.localPosition;
            collision.gameObject.transform.localPosition = new Vector3(collision.gameObject.transform.localPosition.x, pos.y + 2686, collision.gameObject.transform.localPosition.z);
        }
    }
}
