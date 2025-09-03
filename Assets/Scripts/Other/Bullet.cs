using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IInteractable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceLife;

    public event Action<Bullet> Finished;

    private Coroutine _flightRoutine;

    public void Launch(Vector2 direction)
    {
        if (_flightRoutine != null)
            StopCoroutine(_flightRoutine);

        Vector2 target = (Vector2)transform.position + direction.normalized * _distanceLife;
        _flightRoutine = StartCoroutine(UpdateFlight(target));
    }

    private IEnumerator UpdateFlight(Vector2 target)
    {
        while (enabled && (Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(
                (Vector2)transform.position,
                target,
                Time.deltaTime * _speed);

            yield return null;
        }

        Finished?.Invoke(this);
    }

    public void ResetParameters()
    {
        if (_flightRoutine != null)
        {
            StopCoroutine(_flightRoutine);
            _flightRoutine = null;
        }
    }
}
