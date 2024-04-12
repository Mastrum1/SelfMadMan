using UnityEngine;

public class ArrowMilk : MonoBehaviour
{
    [SerializeField] private GameObject _arrowPos;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, _arrowPos.transform.position.y);
    }
}
