using System;
using TMPro;
using UnityEngine;

class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _scoreCounter.ScoreChanged += Display;
    }

    private void OnDisable()
    {
        _scoreCounter.ScoreChanged += Display;
    }

    private void Display(int score)
    {
        _text.text = Convert.ToString(score);
    }
}