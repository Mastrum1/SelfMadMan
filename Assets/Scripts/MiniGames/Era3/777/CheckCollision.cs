using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    [SerializeField] Transform _mSpawnPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EndPos")
        {
            Debug.Log("EnterBox");
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EndPos")
        {
            Debug.Log("ExitBox");
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.position = _mSpawnPos.position;
        }
    }
}
