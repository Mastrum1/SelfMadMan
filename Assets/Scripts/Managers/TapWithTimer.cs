using System;
using TMPro;
using UnityEngine;
using System.Collections;


public class TapWithTimer : MonoBehaviour
{
    [SerializeField] private GetBenchManager _mBenchManager;

    [SerializeField] private float _mMaxTimeFadeOut = 0.1f;

    public event Action<bool> OnLoose;
    public bool StopTorus { get => _StopTorus; set => _StopTorus = value; }

    public bool _StopTorus = true;

    public TextMeshProUGUI Number { get => _Number; private set => _Number = value; }

    [SerializeField] private TextMeshProUGUI _Number;

    [SerializeField] private float _mMaxScale = 2.5f;

    [SerializeField] private float _mMinTimeForClick = 0.8f;

    [SerializeField] private float _mPerfectTiming = 1f;

    [SerializeField] private float _mMidTiming = 1.5f;

    [SerializeField] private float _mImmondeTiming = 2f;
    public GameObject Torus { get => _mTorus; set => _mTorus = value; }

    [SerializeField] private GameObject _mTorus;

    [SerializeField] private float _mDecreaseSpeed;

    void OnEnable()
    {
        _mTorus.transform.localScale = new Vector3(_mMaxScale, _mMaxScale, _mMaxScale);
        _Number.color = new Color(_Number.color.r, _Number.color.g, _Number.color.b, 255);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, 255);
        _mTorus.GetComponent<SpriteRenderer>().color = new Color(_mTorus.GetComponent<SpriteRenderer>().color.r, _mTorus.GetComponent<SpriteRenderer>().color.g, _mTorus.GetComponent<SpriteRenderer>().color.b, 255);
        _Number.enabled = false;
        _mTorus.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!StopTorus)
        {
            _mTorus.transform.localScale -= new Vector3(Time.deltaTime * _mDecreaseSpeed, Time.deltaTime * _mDecreaseSpeed, Time.deltaTime * _mDecreaseSpeed);
            if (_mTorus.transform.localScale.x < _mMinTimeForClick)
            {
                OnLoose?.Invoke(false);
                gameObject.SetActive(false);
            }
        }
    }

    public void ObjectTaped()
    {
        if (_mTorus.transform.localScale.x < _mPerfectTiming && _mTorus.transform.localScale.x > _mMinTimeForClick)
        {
            Debug.Log("Perfect click");
            StartCoroutine(FadeOut());
        }
        else if (_mTorus.transform.localScale.x < _mMidTiming && _mTorus.transform.localScale.x > _mPerfectTiming)
        {
            Debug.Log("mid click");
            StartCoroutine(FadeOut());

        }
        else if (_mTorus.transform.localScale.x < _mImmondeTiming && _mTorus.transform.localScale.x > _mMidTiming)
        {
            Debug.Log("Immonde click");
            StartCoroutine(FadeOut());

        }
        else
        {
            OnLoose?.Invoke(false);
        }
    }

    private IEnumerator FadeOut()
    {
        float counter = 0;

        while (counter < _mMaxTimeFadeOut)
        {
            _StopTorus = true;

            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / _mMaxTimeFadeOut);

            Debug.Log(alpha);

            _Number.color = new Color(_Number.color.r, _Number.color.g, _Number.color.b, alpha);

            gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.GetComponent<SpriteRenderer>().color.r, gameObject.GetComponent<SpriteRenderer>().color.g, gameObject.GetComponent<SpriteRenderer>().color.b, alpha);

            _mTorus.GetComponent<SpriteRenderer>().color = new Color(_mTorus.GetComponent<SpriteRenderer>().color.r, _mTorus.GetComponent<SpriteRenderer>().color.g, _mTorus.GetComponent<SpriteRenderer>().color.b, alpha);

            //Wait for a frame
            yield return null;
        }

        gameObject.SetActive(false);
        StopCoroutine(FadeOut());

    }
}
