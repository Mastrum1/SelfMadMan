using System;
using TMPro;
using UnityEngine;
using System.Collections;


public class TapWithTimer : MonoBehaviour
{

    [SerializeField] private float _mMaxTimeFadeOut = 0.1f;

    [SerializeField] private VFXScaleUp _mVFXScaleUp;

    public event Action<bool> OnLoose;
    public bool StopTorus { get => _StopTorus; set => _StopTorus = value; }

    public bool _StopTorus = true;

    public TextMeshProUGUI Number { get => _Number; private set => _Number = value; }

    [SerializeField] private float scaleUpDuration = 0.5f;

    [SerializeField] private SpriteRenderer _mSpriteRenderer;

    [SerializeField] private SpriteRenderer _mTorusSpriteRenderer;

    [SerializeField] private TextMeshProUGUI _Number;

    [SerializeField] private float _mMaxScale = 2.5f;

    [SerializeField] private float _mMinTimeForClick = 0.8f;

    [SerializeField] private float _mPerfectTiming = 1f;

    [SerializeField] private float _mMidTiming = 1.5f;

    [SerializeField] private float _mImmondeTiming = 2f;
    public GameObject Torus { get => _mTorus; set => _mTorus = value; }

    [SerializeField] private GameObject _mTorus;

    private float _mDecrease;

    [SerializeField] private float _mDecreaseSpeed;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }
    void OnEnable()
    {
        _mTorus.transform.localScale = new Vector3(_mMaxScale, _mMaxScale, _mMaxScale);
        _Number.color = new Color(_Number.color.r, _Number.color.g, _Number.color.b, 255);
        _mSpriteRenderer.color = new Color(_mSpriteRenderer.color.r, _mSpriteRenderer.color.g, _mSpriteRenderer.color.b, 255);
        _Number.enabled = false;
        _mTorusSpriteRenderer.color = new Color(_mTorusSpriteRenderer.color.r, _mTorusSpriteRenderer.color.g, _mTorusSpriteRenderer.color.b, 255);
        gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        _mTorusSpriteRenderer.enabled = false;
        _mSpriteRenderer.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!StopTorus)
        {
            switch (GameManager.instance.FasterLevel)
            {
                case 1:
                    _mDecrease = GameManager.instance.FasterLevel;
                    break;
                default:
                    _mDecrease = (GameManager.instance.FasterLevel / 1.45f);
                    break;
            }

            _mTorus.transform.localScale -= new Vector3(Time.deltaTime * _mDecreaseSpeed * _mDecrease, Time.deltaTime * _mDecreaseSpeed * _mDecrease, Time.deltaTime * _mDecreaseSpeed * _mDecrease);
            if (_mTorus.transform.localScale.x < _mMinTimeForClick)
            {
                _StopTorus = true;
                OnLoose?.Invoke(false);
                _mTorus.SetActive(false);
                _mVFXScaleUp.OnObjectClicked();
            }
        }
    }

    public void ObjectTaped()
    {

        Debug.Log("Object taped"); 
        _StopTorus = true;
        _mTorus.SetActive(false);

        if (_mTorus.transform.localScale.x < _mPerfectTiming && _mTorus.transform.localScale.x > _mMinTimeForClick)
        {
            Debug.Log("Perfect click");
            _audioManager.PlaySFX(0);
            StartCoroutine(FadeOut());
        }
        else if (_mTorus.transform.localScale.x < _mMidTiming && _mTorus.transform.localScale.x > _mPerfectTiming)
        {
            Debug.Log("mid click");
            _audioManager.PlaySFX(0);
            StartCoroutine(FadeOut());

        }
        else if (_mTorus.transform.localScale.x > _mMidTiming)
        {
            Debug.Log("Immonde click");
            _audioManager.PlaySFX(0);
            StartCoroutine(FadeOut());

        }
    }

    private IEnumerator FadeOut()
    {
        _StopTorus = true;
        float counter = 0;
        StartCoroutine(ScaleUp());
        while (counter < _mMaxTimeFadeOut)
        {
            _StopTorus = true;

            counter += Time.deltaTime;
            //Fade from 1 to 0
            float alpha = Mathf.Lerp(1, 0, counter / _mMaxTimeFadeOut);

            _Number.color = new Color(_Number.color.r, _Number.color.g, _Number.color.b, alpha);

            _mSpriteRenderer.color = new Color(_mSpriteRenderer.color.r, _mSpriteRenderer.color.g, _mSpriteRenderer.color.b, alpha);

            _mTorusSpriteRenderer.color = new Color(_mTorusSpriteRenderer.color.r, _mTorusSpriteRenderer.color.g, _mTorusSpriteRenderer.color.b, alpha);

            //Wait for a frame
            yield return null;
        }

        gameObject.SetActive(false);
        StopCoroutine(FadeOut());

    }

    private IEnumerator ScaleUp()
    {
        // Scale up to 1.3
        float timer = 0f;
        Vector3 targetScale = gameObject.transform.localScale * 1.5f;

        while (timer < scaleUpDuration)
        {
            transform.localScale = Vector3.Lerp(gameObject.transform.localScale, targetScale, timer / scaleUpDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(false);

    }

}
