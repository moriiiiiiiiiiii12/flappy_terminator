using System.Collections;
using UnityEngine;

class Shooter : MonoBehaviour
{
    [SerializeField] private float _cooldown;
    [SerializeField] private SpawnerBullet _spawnerBullet;
    [SerializeField] private Vector2 _direction; 

    private void Start()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_cooldown);

        while (enabled)
        {
            _spawnerBullet.Spawn(transform.position, _direction);

            yield return waitForSeconds;
        }
    }
}