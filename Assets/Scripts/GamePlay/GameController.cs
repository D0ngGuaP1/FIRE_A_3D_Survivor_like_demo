using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GlobalEnemySpawner ges;
    private MainCharacter player;

    [SerializeField] private float targetingInterval = 0.1f;
    private float targetingTimer;
    
    [SerializeField] private LevelUpChannel levelUpChannel;
    [SerializeField] private AcquireRewardChannel acquireRewardChannel;
    [SerializeField] private PlayerDeathChannel playerDeathChannel;

    private int currentMaxEnemy;
    [SerializeField] private int maxEnemy = 10;
    
    void OnEnable()
    {
        currentMaxEnemy = maxEnemy;
        levelUpChannel.OnRaised += HandleLevelUp;
        acquireRewardChannel.OnRaised += HandleAcquireReward;
        playerDeathChannel.OnRaised += OnPlayerDeath;
        Time.timeScale = 1f;
    }
    void OnDisable()
    {
        levelUpChannel.OnRaised -= HandleLevelUp;
        acquireRewardChannel.OnRaised -= HandleAcquireReward;
        playerDeathChannel.OnRaised -= OnPlayerDeath;
    }
    
    void Awake()
    {
        ges = GetComponent<GlobalEnemySpawner>();
        player = FindObjectOfType<MainCharacter>();
    }

    void Update()
    {
        while (ges.activeEnemyCount < currentMaxEnemy && ges.isPoolInitialized)
        {
            ges.SpawnEnemy(player);
        }

        targetingTimer += Time.deltaTime;
        if (targetingTimer >= targetingInterval && ges.isPoolInitialized)
        {
            targetingTimer = 0;
            player.target = ProvideEnemy().gameObject;
        }
    }

    private Enemy ProvideEnemy()
    {
        if (ges.activeEnemy.Count == 0) return null;
        Enemy result = ges.activeEnemy[0];
        float minDistance = float.MaxValue;
        foreach (var enemy in ges.activeEnemy)
        {
            float previous = minDistance;
            minDistance = Mathf.Min(minDistance, (enemy.transform.position - player.transform.position).sqrMagnitude);
            if (previous > minDistance) result = enemy;
        }
        return result;
    }

    private void HandleLevelUp(int level)
    {
        if (level % 2 == 0)
        {
            currentMaxEnemy++;
        }
        ges.PowerUp();
        Time.timeScale = 0;
    }

    private void HandleAcquireReward(RewardInfoSO reward)
    {
        reward.OnAcquire(player);
        Time.timeScale = 1;
    }

    private void OnPlayerDeath()
    {
        Time.timeScale = 0;
    }
}
