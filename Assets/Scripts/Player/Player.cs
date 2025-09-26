using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private CollisionHandler _handler;
    [SerializeField] private Shooter _shooter;

    public event Action GameOver;

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
        _inputReader.PressJumpInput += ExecuteJump;
        _inputReader.AttackInput += ExecuteAttack;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
        _inputReader.PressJumpInput -= ExecuteJump;
        _inputReader.AttackInput -= ExecuteAttack;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        GameOver?.Invoke();
    }

    public void ExecuteAttack()
    {
        _shooter.ExecuteShot();
    }

    public void ExecuteJump()
    {
        _mover.ExecuteJump();
    }

    public void Reset()
    {
        _shooter.Reset();
        _scoreCounter.Reset();
        _mover.Reset();
    }
}