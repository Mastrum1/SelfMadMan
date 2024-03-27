using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Instruction : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(DeleteInstruction());
    }
    // Start is called before the first frame update
    IEnumerator DeleteInstruction()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
