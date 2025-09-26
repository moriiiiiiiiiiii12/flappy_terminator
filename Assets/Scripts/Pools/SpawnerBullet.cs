using System.Collections.Generic;
using System.Linq;
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

        bullet.Finished += ReturnObject;
        bullet.Launch(direction);
    }

    private void ReturnObject(Bullet bullet)
    {
        if (bullet == null) return;

        bullet.Finished -= ReturnObject;

        if (bullet.gameObject.activeSelf == false)
            return; 

        bullet.ResetParameters();
        Pool.Release(bullet);
    }

    public override void Reset()
    {
        List<Bullet> activeBullets = ActiveObjects.ToList();
        
        foreach (Bullet bullet in activeBullets)
            ReturnObject(bullet);
    }
}
