using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float LifeSpan;

    [SerializeField] public int HideOrDestroy; //0 Hide, 1 destroy
    private void Awake()
    {
        switch(HideOrDestroy)
        {
            case 0:
                StartCoroutine(WaitAndHide());
                break;
            case 1:
                StartCoroutine(WaitAndDestroy());
                break;
        }
    }

    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(LifeSpan);
        this.gameObject.SetActive(false);
        yield return null;
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(LifeSpan);
        Destroy(this);
        yield return null;
    }
}
