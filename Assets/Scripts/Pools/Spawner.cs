using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("Необходимые компоненты: ")]
    [SerializeField] protected T Prefab;

    [Header("Настройки пула: ")]
    [SerializeField] protected int PoolSize = 5;

    protected List<T> ActiveObjects = new();
    protected ObjectPool<T> Pool;

    public int CountActiveObjects { get; private set; } = 0;

    protected void Awake()
    {
        Pool = new ObjectPool<T>
        (
            createFunc: () =>
            {
                T prefab = Instantiate(Prefab);
                prefab.gameObject.SetActive(false);

                return prefab;
            },
            actionOnGet: (prefab) =>
            {
                CountActiveObjects++;
                ActiveObjects.Add(prefab);

                ActionOnGet(prefab);
            },
            actionOnRelease: (prefab) =>
            {
                CountActiveObjects--;
                ActiveObjects.Remove(prefab);

                ActionOnRelease(prefab);
            },
            actionOnDestroy: (prefab) =>
            {
                Destroy(prefab.gameObject);
            },
            collectionCheck: true,
            defaultCapacity: PoolSize,
            maxSize: PoolSize
        );
    }

    protected void ActionOnGet(T prefab)
    {
        prefab.gameObject.SetActive(true);
    }

    protected void ActionOnRelease(T prefab)
    {
        prefab.gameObject.SetActive(false);
    }

    public virtual void Reset()
    {
        foreach (T prefab in ActiveObjects)
            Pool.Release(prefab);
    }
}