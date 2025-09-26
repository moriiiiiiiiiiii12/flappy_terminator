using UnityEngine;

public class SpawnerBullet : Spawner<Bullet>
{
    public void Spawn(Vector3 position, Vector2 direction)
    {
        if (Prefab == null) return;

        Bullet bullet = Pool.Get();
        bullet.ResetParameters();
        bullet.transform.position = position;
        bullet.transform.right = direction;

        bullet.Finished += DestroyObject;
        bullet.Launch(direction);
    }

    protected override void DestroyObject(Bullet bullet)
    {
        if (bullet == null) return;

        bullet.Finished -= DestroyObject;

        if (bullet.gameObject.activeSelf == false)
            return; 

        bullet.ResetParameters();
        Pool.Release(bullet);
    }

    public override void Reset()
    {
        foreach (Bullet bullet in AllObjects)
            if (bullet.gameObject.activeSelf)
                DestroyObject(bullet);
    }
}
