using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private AutoShooter _shooter;
    [SerializeField] private Movement _movement;

    public event Action<Enemy> Die;

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IInteractable interactable)
    {
        if (interactable is DeadZone)
        {
            Die?.Invoke(this);
        } 

        if (interactable is Bullet)
        {
            Die?.Invoke(this);
        }
    }

    public void Reset()
    {
        _shooter.Reset();
    }
}
