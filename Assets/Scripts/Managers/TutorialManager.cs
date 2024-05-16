using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    public Vector3[] ChadPos;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

       
    }


    private void Start()
    {
        if (!GameManager.instance.Player.TutorialPlayed)
            InTutorial = true;
        else
            InTutorial = false;
    }
    [System.Serializable]
    public class Step
    {
        private Canvas canvas;
        private RectTransform _rectTransform;
        [SerializeField] public string _instruction;
        [SerializeField] public int _ChadPos;

        public void Init()
        {
            canvas.sortingOrder = 10;
            _rectTransform.anchoredPosition3D = new Vector3(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y, -2);
        }
        public void Reset()
        {
            canvas.sortingOrder = 1;
            _rectTransform.anchoredPosition3D = new Vector3(_rectTransform.anchoredPosition.x, _rectTransform.anchoredPosition.y, 1);
        }
        public void SetButton(Canvas canvas, RectTransform rectTransform)
        { 
            this.canvas = canvas;
            this._rectTransform = rectTransform;
        }
    }
    // Start is called before the first frame update

    
    [SerializeField] public List<Step> Steps;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject TapToStartGO;
    [SerializeField] private RectTransform _popup;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _text;
    public int StepNbr;
    public bool InTutorial = false;
    public bool FinalStepBool = false;

    public void NextStep()
    {
        Steps[StepNbr].Reset();
        StepNbr++;
        panel.SetActive(false);
    }

    public void StepInit()
    {
        Debug.Log("Step : " + StepNbr);
        _popup.anchoredPosition3D = ChadPos[Steps[StepNbr]._ChadPos];
        _text.text = Steps[StepNbr]._instruction;
        Steps[StepNbr].Init();
        panel.SetActive(true);

    }

    public void FinalStep()
    {
        FinalStepBool = true;
        TapToStartGO.SetActive(true);
        _popup.anchoredPosition3D = ChadPos[Steps[StepNbr]._ChadPos];
        _text.text = Steps[StepNbr]._instruction;
        panel.SetActive(true);
    }

    public void EndTutorial()
    {
        InTutorial = false;
        GameManager.instance.Player.TutorialPlayed = true;
        panel.SetActive(false);

    }

    public void SetCamera(Camera camera)
    {
        _canvas.worldCamera = camera;
    }

    public void StepAddDataButton(Canvas canvas, RectTransform rectTransform, int Step)
    {
        Steps[Step].SetButton(canvas, rectTransform); 
    }
}
