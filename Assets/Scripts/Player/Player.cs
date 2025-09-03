using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Mover _birdMover;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private CollisionHandler _handler;

    public event Action GameOver;

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is Enemy)
        {
            GameOver?.Invoke();
        }

        if (interactable is Bullet)
        {
            GameOver?.Invoke();

            Time.timeScale = 0f;
        }
    }

    public void Reset()
    {
        _scoreCounter.Reset();
        _birdMover.Reset();
    }
}