using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{

    [SerializeField] TMP_Text _mScoreText;

    // Start is called before the first frame update
    void Start()
    {
        _mScoreText = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _mScoreText.text = GameManager.instance.DisplayScore();
    }
}
