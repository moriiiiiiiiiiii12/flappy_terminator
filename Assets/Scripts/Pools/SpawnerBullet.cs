using UnityEngine;

public class SpawnerBullet : Spawner<Bullet>
{
    [Header("Стрельба")]
    [SerializeField] private bool _fireOnClick = true;

    public void Spawn(Vector3 position, Vector2 direction)
    {
        if (CountActiveObjects >= PoolSize || Prefab == null)
            return;

        Bullet bullet = Pool.Get();

        bullet.transform.position = position;
        bullet.Launch(direction);

        bullet.Finished += DestroyObject;
    }

    protected override void DestroyObject(Bullet bullet)
    {
        if (bullet == null) return;

        bullet.ResetParameters();
        bullet.Finished -= DestroyObject;

        Pool.Release(bullet);
    }
}
