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
    [SerializeField] private ScaleUpBrick _mScaleUpBrick;
    [SerializeField] private TrailRenderer _mTrailEffect;
    private bool _mIsThrowing = false;

    private Vector2 _mDelta;

    public void OnSlide(Vector2 finalDelta)
    {
        if (_mIsThrowing)
            return;
        _mDelta = finalDelta;
        _mThrowLimit.SetActive(false);
        StartCoroutine("ThrowBrick");
        _mIsThrowing = true;
    }

    public void Move(Vector3 pos)
    {
        if (pos.y < _mThrowLimit.transform.position.y)
            transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ecolo"))
        {
            _mTrailEffect.time = 0f;
            _mTrailEffect.enabled = false;
            collision.GetComponent<MoveEcolo>().EcoloGetHit();
            _mSpriteBrick.SetActive(false);
            StartCoroutine(waitBeforeRespawn());
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Parachutistes"))
        {
            _mTrailEffect.time = 0f;
            _mTrailEffect.enabled = false;
            collision.GetComponent<MoveParachutistes>().EcoloGetHit();
            _mSpriteBrick.SetActive(false);
            StartCoroutine(waitBeforeRespawn());
        }
    }

    private void RespawnBrick()
    {
        StopCoroutine("ThrowBrick");
        gameObject.transform.localScale = _mSpawnBrick.transform.localScale;
        _mSpriteBrick.transform.localScale = _mSpawnBrick.transform.localScale;
        gameObject.transform.position = _mSpawnBrick.transform.position;
        _mSpriteBrick.transform.rotation = _mSpawnBrick.transform.rotation;
        _mThrowLimit.SetActive(true);
        _mIsThrowing = false;
        _mTrailEffect.enabled = true;
        _mTrailEffect.time = 0.2f;
        _mSpriteBrick.SetActive(true);

    }

    private IEnumerator ThrowBrick()
    {
        _mScaleUpBrick.StopTheCoroutine();
        _mScaleUpBrick.SetScale();

        // Récupérer la position du centre de l'écran
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Debug.Log(screenCenter);
        //screenCenter.y += 10;
        while (true)
        {
            // Calculer la distance de l'objet au centre de l'écran
            float distanceToCenter = Vector3.Distance(Camera.main.WorldToScreenPoint(transform.position), screenCenter);

            // Calculer le facteur d'échelle en fonction de la distance à l'écran
            float scaleFactor = Mathf.Lerp(0.045f, 0.03f, distanceToCenter / (Screen.width / 2));

            // Appliquer le déplacement et la rotation
            transform.Translate(_mDelta * _mSpeed * Time.deltaTime);
            _mSpriteBrick.transform.Rotate(Vector3.forward, 10);

            // Vérifier si l'objet est sorti de l'écran
            if (transform.position.y > 0.5f && transform.position.x < -1 || transform.position.y > 0.5f && transform.position.x > 1 || transform.position.x > 3 || transform.position.x < -3 || transform.position.y > 3)
            {
                _mTrailEffect.enabled = false;
                _mSpriteBrick.SetActive(false);
                StartCoroutine(waitBeforeRespawn());
            }

            // Réduire l'échelle de l'objet
            transform.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);

            yield return null;
        }
    }

    private IEnumerator waitBeforeRespawn()
    {
        yield return new WaitForSeconds(0.18f);
        RespawnBrick();
        StopCoroutine(waitBeforeRespawn());
    }
}
