using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] GameManager _mgameManager;

    [SerializeField] Text _mscoreText;

    // Start is called before the first frame update
    void Start()
    {
        _mgameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _mscoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //_mscoreText.text = _mgameManager.DisplayScore();
    }
}
