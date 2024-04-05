using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private GameObject _gameObjectTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test");
        _gameObjectTriggered = collision.gameObject;
    }

    public Quarter FetchQuarterData() => _gameObjectTriggered.GetComponent<Quarter>();

}
