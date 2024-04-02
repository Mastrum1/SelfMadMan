using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitArea : MonoBehaviour
{
    [SerializeField] private GameObject _mParent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _mParent.GetComponent<Arrows>().enabled = true;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("loose");
        _mParent.SetActive(false);
        _mParent.GetComponent<Arrows>().enabled = true;

    }
}
