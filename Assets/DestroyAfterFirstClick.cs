using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterFirstClick : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnFirstClick()
    {
        Destroy(gameObject);
    }
}
