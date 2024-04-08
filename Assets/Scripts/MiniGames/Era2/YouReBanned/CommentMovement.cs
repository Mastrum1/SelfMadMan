using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class CommentMovement : MonoBehaviour
{
    [SerializeField] private float _mSpeed = 0.75f;
    [SerializeField] CommentBorder _mUpBorder;
    [SerializeField] CommentBorder _mDownBorder;
    private float _mSpeedInitial;
   private Rigidbody2D _mRigidBody2D;
    
    void Start()
    {
        _mRigidBody2D = GetComponent<Rigidbody2D>();
        _mSpeedInitial = _mSpeed;
        _mUpBorder.OnEnter += OnUpBorderEnter;
        _mDownBorder.OnEnter += OnDownBorderEnter;
    }

    void Update()
    {
       // transform.Translate(Vector3.up * _mSpeed * Time.deltaTime);
        _mRigidBody2D.velocity = Vector3.up * _mSpeed;
    }

    
    public void Stop()
    {
        _mSpeed = 0;
    }

    public void MoveFaster(Vector3 position, int times = 1) 
    {
        if (transform.position.y < position.y) {
            _mSpeed += (1.289999f ) / 0.25f; //_mSpeed * (times * 5);
            Debug.Log("CALLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLED " + transform.gameObject.name + " sp: " + _mSpeed + " pos: " + (position.y - transform.position.y));
            StartCoroutine(Reset());   
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.transform.position.y > transform.position.y) {
            Debug.Log(this.gameObject.name + " meeeeeeeeeeeeeeee?" + col.gameObject.name);
            ResetSpeed();
        }
    }
    
    void OnUpBorderEnter(GameObject other)
    {
        /*if (other.CompareTag("TIkTokComment") && other != this.gameObject) {
            Debug.Log(this.gameObject.name + " meeeeeeeeeeeeeeee?" + other.gameObject.name);
            ResetSpeed();
        }*/
    }

    void OnDownBorderEnter(GameObject other)
    {
        /*if (other.CompareTag("TIkTokComment")) {
           // ResetSpeed();
           Debug.Log(this.gameObject.name + " OTTTTTTTTTTTTTTTTTTTTTHEEEEEEEEEEEEEEEEEE" + other.gameObject.name);
            other.GetComponent<CommentMovement>().ResetSpeed();
        }*/
    }

    public void ResetSpeed()
    {
        Debug.Log("Resssssssssss" + transform.gameObject.name);
        _mSpeed = _mSpeedInitial;
        
    }

    void OnDestroy()
    {
        _mUpBorder.OnEnter -= OnUpBorderEnter;
        _mDownBorder.OnEnter -= OnDownBorderEnter;
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.3f);
        _mSpeed = _mSpeedInitial;
        Debug.Log("Ressssssssssssssssssssssettttttttttttttttt");
    }


/*
    IEnumerator ResetSpeed(int times, float time, float gap)
    {
        yield return new WaitForSeconds(0.5f);
        _mSpeed = 0.5f;
    }*/

    /*private void OnCollisionExit2D(Collision2D collision)
    {
       /* var D = collider.position - position;

        velocity -= normalize(D) * Dot(velocity, D)/
        // Annuler les forces de 
        //Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision.collider);
        Rigidbody2D otherRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (otherRigidbody != null)
        {
           // otherRigidbody.velocity = Vector3.zero;
           // otherRigidbody.angularVelocity = 0f;
            _mRigidBody2D.velocity = Vector3.zero;
            _mRigidBody2D.angularVelocity = 0f;

        }
    }*/
}
