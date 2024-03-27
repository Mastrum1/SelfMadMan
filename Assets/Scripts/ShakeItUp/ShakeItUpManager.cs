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
        Vector3 euler = orientation.eulerAngles;
        euler.z = 0;
        Quaternion flattenedQuaternion = Quaternion.Euler(euler);

        float angle = Mathf.Atan2(flattenedQuaternion.y, flattenedQuaternion.x) * Mathf.Rad2Deg;
        angle = (angle + 360) % 360;

        float radians = angle * Mathf.Deg2Rad;

        Vector2 newGravityDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

        Physics2D.gravity = newGravityDirection.normalized * Physics2D.gravity.magnitude;
    }
}
