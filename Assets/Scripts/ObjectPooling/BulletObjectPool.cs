using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletObjectPool : PersistentSingleton<BulletObjectPool>
{
    [SerializeField] private Bullet bulletPrefab;
    private Queue<Bullet> pool = new Queue<Bullet>();

    // Getting a bullet out of the pool
    public Bullet Get()
    {
        if (pool.Count == 0)
        {
            AddBullet(1);
        }
        return pool.Dequeue();
    }

    // Adding a new bullet into the pool
    private void AddBullet(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var prefab = Instantiate(bulletPrefab);
            prefab.gameObject.SetActive(false);
            pool.Enqueue(prefab);
        }
    }

    // Returning the bullet to the pool
    public void ReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }
}
