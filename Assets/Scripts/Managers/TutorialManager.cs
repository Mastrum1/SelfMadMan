using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    public class Step
    {
        private Canvas canvas;
        [SerializeField] public string _instruction;
        [SerializeField] private int _ChadPos;

        public void Init()
        {
            canvas.sortingOrder = 10;
        }
        public void Reset()
        {
            canvas.sortingOrder = 1;
        }
        public void SetCanvas(Canvas canvas)
        { this.canvas = canvas; }
    }
    // Start is called before the first frame update

    
    [SerializeField] public List<Step> Steps;
    [SerializeField] private GameObject panel;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _text;
    public int StepNbr;
    public bool InTutorial = true;

    public void NextStep()
    {
        Steps[StepNbr].Reset();
        StepNbr++;
        panel.SetActive(false);
    }

    public void StepInit()
    {
        Debug.Log("Step : " + StepNbr);
        _text.text = Steps[StepNbr]._instruction;
        Steps[StepNbr].Init();
        panel.SetActive(true);

    }

    public void FinalStep()
    {
        _text.text = Steps[StepNbr]._instruction;
        panel.SetActive(true);
    }

    public void SetCamera(Camera camera)
    {
        _canvas.worldCamera = camera;
    }

    public void StepAddCanvas(Canvas canvas, int Step)
    {
        Steps[Step].SetCanvas(canvas);
    }
    void Start()
    {
       InTutorial = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
