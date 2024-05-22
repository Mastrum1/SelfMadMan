using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFadeOutObject : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ObstacleParent"))
        {
            collision.gameObject.GetComponent<ObstacleMovement>().FadeChilds();
        }
    }
}
