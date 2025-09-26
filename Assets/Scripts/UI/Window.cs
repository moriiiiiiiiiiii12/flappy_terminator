using System;
using UnityEngine;
using UnityEngine.UI;


public class Window : MonoBehaviour
{
    [SerializeField] private RawImage _startMenu;
    [SerializeField] private Button _playButton;

    public event Action StartGame;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(Play);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(Play);
    }

    private void Play()
    {
        StartGame?.Invoke();
        _startMenu.gameObject.SetActive(false);
    }

    public void Pause()
    {
        _startMenu.gameObject.SetActive(true);
    }
}