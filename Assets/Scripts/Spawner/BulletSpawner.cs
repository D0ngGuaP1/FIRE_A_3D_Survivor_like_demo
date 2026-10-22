using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    //[SerializeField] private Transform bulletParent;
    [SerializeField] private Bullet bulletPrefab;
    private UnityEngine.Pool.ObjectPool<Bullet> bulletPool;
    
    void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(bulletPrefab),
            actionOnGet: b => b.gameObject.SetActive(true),
            actionOnRelease: b => b.gameObject.SetActive(false),
            collectionCheck: true,
            defaultCapacity: 64,
            maxSize: 512
        );
    }

    public void Fire(List<OnHitEffect> effects, Transform spawnPosition, DamageInfo damageInfo, int hitCount)
    {
        Bullet bullet = bulletPool.Get();
        bullet.Launch(effects, spawnPosition.position, spawnPosition.rotation, damageInfo,
            ()=> bulletPool.Release(bullet), hitCount);
    }
}
