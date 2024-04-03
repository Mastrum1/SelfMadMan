using UnityEngine;

public class WaterBottle : MonoBehaviour
{
    public void TiltDown()
    {
        if (transform.eulerAngles.z <= 120)
        {
            transform.Rotate(0,0,60 * Time.deltaTime);
        }
    }

    public void TiltUp()
    {
        if (transform.eulerAngles.z >= 65)
        {
            transform.Rotate(0,0,-60 * Time.deltaTime);
        }
    }
}
