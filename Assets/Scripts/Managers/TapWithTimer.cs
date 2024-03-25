using CW.Common;
using UnityEngine;

public class TapWithTimer : MonoBehaviour
{
    [SerializeField]
    private GetBenchManager _mBenchManager;

    [SerializeField] private ChangeSpawn _mChangeSpawn;

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

    void OnEnable()
    {
        _mTorus.transform.localScale = new Vector3(_mMaxScale, _mMaxScale, _mMaxScale);
        _mChangeSpawn.enabled = true;
    }

    // Update is called once per frame


    private void Update()
    {
        if (_EndGame == false)
        {
            _mTorus.transform.localScale -= new Vector3(Time.deltaTime * _mDecreaseSpeed, Time.deltaTime * _mDecreaseSpeed, Time.deltaTime * _mDecreaseSpeed);
            if (_mTorus.transform.localScale.x < _mMinTimeForClick)
            {
                //_mBenchManager.EndMiniGame(false, _mBenchManager.miniGameScore);
                gameObject.SetActive(false);

            }
        }
    }

    public void ObjectTaped()
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
            Debug.Log("Immonde click");
        }
        else
        {
            Debug.Log("Tu pues :) ");
            //_mBenchManager.EndMiniGame(false, _mBenchManager.miniGameScore);

        }
        gameObject.SetActive(false);

    }
}
