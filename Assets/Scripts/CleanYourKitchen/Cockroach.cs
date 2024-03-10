using UnityEngine;

public class Cockroach : MonoBehaviour
{
    [SerializeField] private Rigidbody2D mRigid2d;
    public float speed;
    
    private bool _isDead;
    void Start()
    {
        _isDead = false;
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead)
        {
            Destroy(this);
        }
        
        //transform.up = transform.
    }

    void Move()
    {
        mRigid2d.AddForce(transform.up * speed, ForceMode2D.Force);
    }
}
