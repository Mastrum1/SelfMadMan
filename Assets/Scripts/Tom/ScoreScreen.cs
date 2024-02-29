using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        StartCoroutine(Loading());   
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(3f);
        mySceneManager.instance.RandomGameChoice(0); //random
    }
}
