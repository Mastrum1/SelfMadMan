using System.Collections;
using UnityEngine;

public class WaterBottle : MonoBehaviour
{
    [SerializeField] private GameObject _bottleCap;
    public void TiltDown()
    {
        if (_bottleCap.activeSelf) _bottleCap.SetActive(false);
        
        StopCoroutine(TiltUp());
        
        if (transform.eulerAngles.z <= 100)
        {
            transform.Rotate(0,0,95 * Time.deltaTime);
        }
    }

    public void StartTiltUp()
    {
        if (!_bottleCap.activeSelf) _bottleCap.SetActive(true);

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
