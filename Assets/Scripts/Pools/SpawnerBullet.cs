using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerBullet : Spawner<Bullet>
{
    public void Spawn(Vector3 position, Vector2 direction)
    {
        if (PoolSize <= CountActiveObjects)
            return;

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

        if (ActiveObjects.Contains(bullet) == false) return; 

        Pool.Release(bullet);
        
        bullet.Finished -= ReturnObject;

        bullet.ResetParameters();
    }

    public override void Reset()
    {
        foreach (Bullet bullet in ActiveObjects.ToArray())
            ReturnObject(bullet);
            
        if (ActiveObjects.Count > 0) { Reset(); }
    }
}
