using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class SpawnerEnemy : Spawner<Enemy>
{
    [Header("Арена спавна (BoxCollider2D или Renderer)")]
    [SerializeField] private Collider2D _arenaCollider2D;
    [SerializeField] private Renderer _arenaRenderer;

    [Header("Настройки спавна")]
    [SerializeField] private float _spawnInterval = 2f;

    public event Action EnemyDie;

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enabled)
        {
            if (PoolSize > CountActiveObjects)
            {
                SpawnRandom();
            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    public void SpawnRandom()
    {
        if (TryGetArenaBounds(out Bounds bounds))
        {
            float x = UnityEngine.Random.Range(bounds.min.x, bounds.max.x);
            float y = UnityEngine.Random.Range(bounds.min.y, bounds.max.y);
            
            SpawnAt(new Vector3(x, y, 0f));
        }
    }

    public void SpawnAt(Vector3 position)
    {
        if (Prefab == null) return;

        Enemy enemy = Pool.Get();
        enemy.transform.position = position;

        enemy.Die += ReturnObject;
    }

    protected void ReturnObject(Enemy enemy)
    {
        if (enemy == null) return;

        if (ActiveObjects.Contains(enemy) == false) return; 

        enemy.Die -= ReturnObject;
        Pool.Release(enemy);

        EnemyDie?.Invoke();
    }

    private bool TryGetArenaBounds(out Bounds bounds)
    {
        if (_arenaCollider2D != null)
        {
            bounds = _arenaCollider2D.bounds;

            return true;
        }
        
        if (_arenaRenderer != null)
        {
            bounds = _arenaRenderer.bounds;

            return true;
        }

        bounds = default;
        return false;
    }

    public override void Reset()
    {
        foreach (Enemy enemy in ActiveObjects.ToArray())
        {
            enemy.Reset();

            ReturnObject(enemy);
        }
        
        if (ActiveObjects.Count > 0) { Reset(); }
    }
}
