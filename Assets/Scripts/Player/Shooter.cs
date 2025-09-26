using System.Collections;
using UnityEngine;

class Shooter : MonoBehaviour
{
    [SerializeField] private SpawnerBullet _spawnerBullet;
    [SerializeField] private float _cooldown;

    private Coroutine _loop;

    public void ExecuteShot()
    {
        if (enabled == false || _loop != null) return;

        _spawnerBullet.Spawn(transform.position, transform.right);
        _loop = StartCoroutine(WaitCooldown());
    }

    private IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(_cooldown);

        _loop = null;
    }

    private void OnDisable()
    {
        if (_loop != null)
        {
            StopCoroutine(_loop);
            _loop = null;
        }
    }

    public void Reset()
    {
        if (_loop != null)
        {
            StopCoroutine(_loop);
        }

        _spawnerBullet.Reset();
        _loop = StartCoroutine(WaitCooldown());
    }
}
