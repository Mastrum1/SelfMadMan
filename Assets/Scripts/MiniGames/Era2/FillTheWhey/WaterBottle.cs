using System.Collections;
using UnityEngine;

public class WaterBottle : MonoBehaviour
{
    public void TiltDown()
    {
        StopCoroutine(TiltUp());
        if (transform.eulerAngles.z <= 120)
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
        while (transform.eulerAngles.z >= 65)
        {
            transform.Rotate(0,0,-50 * Time.deltaTime);

            yield return null;
        }
    }
}
