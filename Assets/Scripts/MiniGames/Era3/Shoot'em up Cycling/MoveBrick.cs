using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MoveBrick : MonoBehaviour
{
    [SerializeField] int _mSpeed;
    [SerializeField] private GameObject _mSpawnBrick;
    private Vector2 _mDelta;
    public void OnSlide(Vector2 finalDelta)
    {
        Debug.Log(finalDelta);
        _mDelta = finalDelta;
        StartCoroutine("ThrowBrick");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ecolo"))
        {
            collision.gameObject.GetComponent<MoveEcolo>().EcoloGetHit();
            StopCoroutine("ThrowBrick");
            gameObject.transform.position = _mSpawnBrick.transform.position;
            gameObject.transform.rotation = _mSpawnBrick.transform.rotation;
            gameObject.transform.localScale = _mSpawnBrick.transform.localScale;

        }
    }

    private IEnumerator ThrowBrick()
    {
        while (true)
        {
            Debug.Log("test");
            gameObject.transform.Translate(new Vector3(_mDelta.x, _mDelta.y, 0) * _mSpeed * Time.deltaTime);
            gameObject.transform.localScale -= new Vector3(0.025f,0.025f,0.025f);
            yield return null;

        }

    }

}
