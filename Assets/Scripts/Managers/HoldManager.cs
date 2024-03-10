using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldManager : MonoBehaviour
{
    private InputManager _mInput;

    void Start()
    {
        _mInput = InputManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        OnHold();
    }

    private void OnHold()
    {
        if (_mInput.isDragging)
        {
            RaycastHit2D hit = Physics2D.Raycast(_mInput.PrimaryPos(), Vector2.zero);
            if (hit.collider == gameObject.transform.GetChild(0).GetComponent<Collider2D>())
            {
                Debug.Log("Holding");
            }
        }
    }
}
