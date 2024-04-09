using System.Collections;
using UnityEngine;

public class WaterBottle : MonoBehaviour
{
    public void TiltDown()
    {
        StopCoroutine(TiltUp());
        if (transform.eulerAngles.z <= 100)
        {
            transform.Rotate(0,0,60 * Time.deltaTime);
        }
    }

    public void StartTiltUp()
    {
        StartCoroutine(TiltUp());
    }
    
    private IEnumerator TiltUp()
    {
        while (transform.eulerAngles.z >= 55)
        {
            transform.Rotate(0,0,-50 * Time.deltaTime);

            yield return null;
        }
    }
}
