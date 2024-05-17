using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowDragAndDropManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public UnityEvent<Vector3> _mOnDragAndDrop;

    [SerializeField] public UnityEvent<Vector2> _mOnThrow;


}
