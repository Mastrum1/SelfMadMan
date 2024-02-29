using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManaProxy : MonoBehaviour
{

    [Scene] public string scenName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        SceneMana.instance.NextGame();
    }
}
