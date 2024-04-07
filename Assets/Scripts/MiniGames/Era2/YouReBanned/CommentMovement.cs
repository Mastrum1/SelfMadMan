using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;

public class CommentMovement : MonoBehaviour
{
    [SerializeField] private float _mSpeed = 0.75f;
    private Rigidbody2D _mRigidBody2D;
    
    void Start()
    {
        _mRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //transform.Translate(Vector3.up * _mSpeed * Time.deltaTime);
        _mRigidBody2D.velocity = Vector3.up * _mSpeed;
    }

    
    public void Stop()
    {
        _mSpeed = 0;
    }

    public void MoveFaster(Vector3 position, int times = 1) 
    {
        if (transform.position.y < position.y) {
            Debug.Log("Fassssssssssssssssterrrrrrrrrrrrrr");
           /*// _mSpeed =  times * 3;
           _mSpeed = times * 2.5F;
            StartCoroutine(ResetSpeed());*/
        }
    }

    /*IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(0.5f);
        _mSpeed = 0.5f;
    }*/


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
