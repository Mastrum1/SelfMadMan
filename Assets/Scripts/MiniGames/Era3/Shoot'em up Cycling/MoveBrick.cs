using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBrick : MonoBehaviour
{
    [SerializeField] int _mSpeed;
    [SerializeField] private GameObject _mSpawnBrick;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnRelease()
    {

        //Debug.Log("release");
        //gameObject.transform.position = _mSpawnBrick.transform.position;
        //gameObject.transform.rotation = _mSpawnBrick.transform.rotation;
        //gameObject.transform.localScale = _mSpawnBrick.transform.localScale;

    }

    public void move(Vector3 pos)
    {
        Debug.Log(pos);
        transform.LookAt(pos);
        StartCoroutine("ThrowBrick");
        //transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ecolo"))
        {
            collision.gameObject.GetComponent<MoveEcolo>().EcoloGetHit();

        }
    }

    private IEnumerator ThrowBrick()
    {
        while (true)
        {
            Debug.Log("test");
            gameObject.transform.Translate(Vector3.forward * _mSpeed *  Time.deltaTime);
            yield return null;

        }

    }

}
