using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceLife;
    [SerializeField] private CollisionHandler _collisionHandler;

    public event Action<Bullet> Finished;

    private Coroutine _flightRoutine;
    private bool _finished;

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= ProcessCollision;
    }

    public void Launch(Vector2 direction)
    {
        ResetParameters();

        Vector2 target = (Vector2)transform.position + direction.normalized * _distanceLife;
        _flightRoutine = StartCoroutine(UpdateFlight(target));
    }

    private void ProcessCollision(IInteractable interactable)
    {
        TryFinish();
    }

    private IEnumerator UpdateFlight(Vector2 target)
    {
        while (enabled && (Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, target, Time.deltaTime * _speed);
            Vector2 direction = (target - (Vector2)transform.position).normalized;
            transform.right = direction;

            yield return null;
        }

        TryFinish();
    }

    private void TryFinish()
    {
        if (_finished)
            return;

        _finished = true;
        Finished?.Invoke(this);
    }

    public void ResetParameters()
    {
        if (_flightRoutine != null)
        {
            StopCoroutine(_flightRoutine);
            _flightRoutine = null;
        }

        _finished = false;
    }
}
