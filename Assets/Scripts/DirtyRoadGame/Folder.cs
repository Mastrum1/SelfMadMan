using UnityEngine;

public class Folder : MonoBehaviour
{
    private InputManager _mInput;

    void Start()
    {
        //_mInput = InputManager.Instance;
    }
    
    void Update()
    {
       // OnDrag();
    }

    private void OnDrag()
    {
        if (_mInput.isDragging)
        {
            RaycastHit2D hit = Physics2D.Raycast(_mInput.PrimaryPos(), Vector2.zero);
            if (hit.collider == gameObject.transform.GetChild(0).GetComponent<Collider2D>())
            {
                transform.position = _mInput.PrimaryPos();
            }
        }
    }
}
