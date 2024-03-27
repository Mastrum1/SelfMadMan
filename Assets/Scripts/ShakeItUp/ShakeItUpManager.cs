using Unity.Mathematics;
using UnityEngine;

public class ShakeItUpManager : MiniGameManager
{
    [SerializeField] private GameObject _waterParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeGravityOrientation(Quaternion orientation)
    {
        Vector2 gravityDirection = orientation * Vector2.down;
        
        Physics2D.gravity = gravityDirection.normalized * Physics2D.gravity.magnitude;
    }
}
