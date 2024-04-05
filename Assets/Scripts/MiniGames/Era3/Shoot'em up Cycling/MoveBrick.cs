using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class MoveBrick : MonoBehaviour
{
    [SerializeField] int _mSpeed;
    [SerializeField] private GameObject _mSpawnBrick;
    [SerializeField] private GameObject _mSpriteBrick;
    [SerializeField] private GameObject _mThrowLimit;

    private Vector2 _mDelta;

    public void OnSlide(Vector2 finalDelta)
    {
        _mDelta = finalDelta;
        _mThrowLimit.SetActive(false);
        StartCoroutine("ThrowBrick");

    }

    public void Move(Vector3 pos)
    {
        if(pos.y < _mThrowLimit.transform.position.y)
            transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ecolo"))
        {
            collision.gameObject.GetComponent<MoveEcolo>().EcoloGetHit();
            RespawnBrick();
        }
    }

    private void RespawnBrick()
    {
        StopCoroutine("ThrowBrick");
        gameObject.transform.position = _mSpawnBrick.transform.position;
        _mSpriteBrick.transform.rotation = _mSpawnBrick.transform.rotation;
        gameObject.transform.localScale = _mSpawnBrick.transform.localScale;
        _mThrowLimit.SetActive(true);

    }

    private IEnumerator ThrowBrick()
    {
        while (true)
        {
            gameObject.transform.Translate(new Vector3(_mDelta.x, _mDelta.y, 0) *  _mSpeed * Time.deltaTime);
            _mSpriteBrick.transform.Rotate(new Vector3(0, 0, 1) * 10);
            if (gameObject.transform.position.y > 0.5f)
            {
                RespawnBrick();
            }
            gameObject.transform.localScale -= new Vector3(0.04f, 0.04f, 0.04f);
            yield return null;

        }

    }

}
