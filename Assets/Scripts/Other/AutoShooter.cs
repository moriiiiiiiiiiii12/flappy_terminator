using System.Collections;
using UnityEngine;

class AutoShooter : MonoBehaviour
{
    [SerializeField] private float _cooldown = 0.5f;
    [SerializeField] private SpawnerBullet _spawnerBullet;
    [SerializeField] private Vector2 _direction;

    private Coroutine _loop;

    private void OnEnable()
    {
        if (_loop == null)
            _loop = StartCoroutine(FireLoop());
    }

    private void OnDisable()
    {
        if (_loop != null)
        {
            StopCoroutine(_loop);
            _loop = null;
        }
    }

    private IEnumerator FireLoop()
    {
        WaitForSeconds wait = new WaitForSeconds(_cooldown);

        yield return wait;

        while (true)
        {
            Vector2 dir = _direction.sqrMagnitude > 0.0001f
                ? _direction.normalized
                : (Vector2)transform.right;

            _spawnerBullet.Spawn(transform.position, dir);

            yield return wait;
        }
    }

    public void Reset()
    {
        _spawnerBullet.Reset();
    }
}
