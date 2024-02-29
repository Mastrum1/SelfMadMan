using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PornAds : MonoBehaviour
{
    public bool randForce;
    public float forceX;
    public float forceY;
    public float dirX;
    public float dirY;
    private Rigidbody2D _mRigid2d;
    // Start is called before the first frame update
    void Start()
    {
        _mRigid2d = GetComponent<Rigidbody2D>();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        // add collision check
    }

    void Move()
    {
        if (randForce)
        {
            do
            {
                dirX = Random.Range(-1, 1);
                dirY = Random.Range(-1, 1); 
            } while (dirX == 0 || dirY == 0);
            forceX = Random.Range(51, 200);
            forceY = Random.Range(51, 200);
            _mRigid2d.AddForce(new Vector2(forceX * dirX, forceY * dirY));
        }
        else
        {
            if (forceX < 51)
            {
                forceX = 51;
            }
            if (forceY < 51)
            {
                forceY = 51;
            }
            _mRigid2d.AddForce(new Vector2(forceX * dirX, forceY * dirY));
        }
        
    }
}
