using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Load());   
    }

    IEnumerator Load()
    {
        yield return new WaitForSeconds(3f);
        mySceneManager.instance.SetScene("HomePage", mySceneManager.LoadMode.SINGLE);
    }
}
