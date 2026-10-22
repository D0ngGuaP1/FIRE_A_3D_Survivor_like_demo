using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

public class GlobalEnemySpawner : MonoBehaviour
{

    private ObjectPool<Enemy> enemyPool;
    public List<Enemy> activeEnemy;

    private AsyncOperationHandle<GameObject> enemyHandle;
    private GameObject enemyPrefab;
    [SerializeField] private AssetReferenceGameObject enemyPrefabRef;
    
    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 15f;
    
    private float powerUpMultiplier = 1.0f;

    public bool isPoolInitialized { get; private set; }

    public int activeEnemyCount
    {
        get
        {
            return activeEnemy.Count;
        }
    }
    
    IEnumerator Start()
    {
        var initHandle = Addressables.InitializeAsync();
        yield return initHandle;
        
        enemyHandle = Addressables.LoadAssetAsync<GameObject>(enemyPrefabRef);
        yield return enemyHandle;
        
        if (enemyHandle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Enemy prefab load failed: {enemyHandle.OperationException}");
            yield break;
        }
        
        enemyPrefab = enemyHandle.Result;
        
        activeEnemy = new List<Enemy>();
        enemyPool = new ObjectPool<Enemy>(
            createFunc: () =>
            {
                var instance = Instantiate(enemyPrefab);
                var e = instance.GetComponent<Enemy>();
                e.gameObject.SetActive(false);
                return e;
            },
            actionOnGet: e => { activeEnemy.Add(e); },
            actionOnRelease: e => { e.gameObject.SetActive(false); activeEnemy.Remove(e); },
            collectionCheck: true,
            defaultCapacity: 32,
            maxSize: 256
        );
        isPoolInitialized =  true;
    }

    void OnDestroy()
    {
        enemyHandle.Release();
    }

    public void SpawnEnemy(MainCharacter target)
    {
        if ( !isPoolInitialized || enemyPool == null) return;
        
        Enemy e =  enemyPool.Get();
        e.ApplyPowerUp(powerUpMultiplier);
        
        Vector2 dir = Random.insideUnitCircle.normalized;
        float radius = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 offset = new Vector3(dir.x * radius, 0f, dir.y * radius);
        Vector3 spawnPos = target.transform.position + offset;
        spawnPos.y = 0f;
        e.transform.position = spawnPos;
        
        e.gameObject.SetActive(true); 
        e.Spawn(()=> enemyPool.Release(e), target);
    }

    public void PowerUp()
    {
        powerUpMultiplier+=0.1f;
#if UNITY_EDITOR
        Debug.Log("PowerUp: " + powerUpMultiplier);
#endif
    }
}
