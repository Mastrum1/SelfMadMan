using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private InputManager _mInput;


    private bool _EndGame = false;

    [SerializeField]
    private float _mMaxScale = 2.5f;

    [SerializeField]
    private float _mMinTimeForClick = 0.8f;

    [SerializeField]
    private float _mPerfectTiming = 1f;

    [SerializeField]
    private float _mMidTiming = 1.5f;

    [SerializeField]
    private float _mImmondeTiming = 2f;

    [SerializeField]
    private GameObject _mTorus;

    [SerializeField]
    private float _mDecreaseSpeed;

    void Start()
    {
        _mInput = InputManager.Instance;
        _mTorus.transform.localScale = new Vector3(_mMaxScale, _mMaxScale, _mMaxScale);
    }

    // Update is called once per frame

    private void Update()
    {
        if (_mInput.isDragging)
        {
            ObjectTaped();
        }
        else if (_EndGame == false)
        {
            _mTorus.transform.localScale -= new Vector3(Time.deltaTime * _mDecreaseSpeed, Time.deltaTime * _mDecreaseSpeed, Time.deltaTime * _mDecreaseSpeed);
            if(_mTorus.transform.localScale.x < _mMinTimeForClick)
            {
                _EndGame = true;
            }
        }
    }

    private void ObjectTaped()
    {
        if (_mInput.isDragging)
        {
            RaycastHit2D hit = Physics2D.Raycast(_mInput.PrimaryPos(), Vector2.zero);
            if (hit.collider == gameObject.transform.GetChild(0).GetComponent<Collider2D>())
            {
                if (_mTorus.transform.localScale.x < _mPerfectTiming && _mTorus.transform.localScale.x > _mMinTimeForClick)
                {
                    Debug.Log("Perfect click");
                }
                else if (_mTorus.transform.localScale.x < _mMidTiming && _mTorus.transform.localScale.x > _mPerfectTiming)
                {
                    Debug.Log("mid click");
                }
                else if (_mTorus.transform.localScale.x < _mImmondeTiming && _mTorus.transform.localScale.x > _mMidTiming)
                {
                    Debug.Log("mid click");
                }
                else
                {
                    Debug.Log("Tu pues :)");
                }
                _EndGame = true;

            }
        }
    }
}
