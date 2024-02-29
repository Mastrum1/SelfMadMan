using UnityEngine;
using Random = UnityEngine.Random;

public class PornAdds : MonoBehaviour
{
    [SerializeField] private bool mRandForce;
    [SerializeField] private float mForceX;
    [SerializeField] private float mForceY;
    [SerializeField] private float mDirX;
    [SerializeField] private float mDirY;
    [SerializeField] private Rigidbody2D mRigid2d;

    void Start()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
           //send event to gamemanager
        }
    }

    void Move()
    {
        if (mRandForce)
        {
            do
            {
                mDirX = Random.Range(-1, 2);
                mDirY = Random.Range(-1, 2); 
            } while (mDirX == 0 || mDirY == 0);
            
            mForceX = Random.Range(51, 200);
            mForceY = Random.Range(51, 200);
            mRigid2d.AddForce(new Vector2(mForceX * mDirX, mForceY * mDirY));
        }
        else
        {
            if (mForceX < 51)
            {
                mForceX = 51;
            }
            if (mForceY < 51)
            {
                mForceY = 51;
            }
            mRigid2d.AddForce(new Vector2(mForceX * mDirX, mForceY * mDirY));
        }
    }
}
