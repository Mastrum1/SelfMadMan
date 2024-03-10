using UnityEngine;

public class Cockroach : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mRigid2d;
    public float speed;
    void Start()
    {
        Move();
    }
    
    void Update()
    {
        CheckDeath();

        transform.up = mRigid2d.velocity;
    }

    void Move()
    {
        mRigid2d.AddForce(transform.up * speed, ForceMode2D.Force);
    }

    void CheckDeath()
    {
        
    }
    
    
}
