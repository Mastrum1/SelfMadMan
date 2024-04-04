using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MoveEcolo : MonoBehaviour
{
    [SerializeField] private Transform car;
    [SerializeField] private float speed;

    public event Action<bool> OnLoose;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime; // calculate distance to move
        transform.position = new Vector3(Vector3.MoveTowards(transform.position, car.position, step).x, transform.position.y, transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "car")
        {
            gameObject.GetComponent<Animator>().SetBool("EndGame", true);
            OnLoose?.Invoke(false);
            Debug.Log("endGame");
        }
    }

    public void EcoloGetHit()
    {
        gameObject.GetComponent<Animator>().SetBool("GetHit", true);
        gameObject.SetActive(false);
    }
}
