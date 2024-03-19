using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject instruction;
    bool firstClick = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && firstClick == false) 
        {
            Destroy(instruction);
            firstClick = true;
        }
    }
}
