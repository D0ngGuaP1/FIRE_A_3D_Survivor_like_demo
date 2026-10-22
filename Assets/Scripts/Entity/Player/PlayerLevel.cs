using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] private float xpRequired;
    private float xpCurrent = 0f;
    private int currentLevel = 0;
    
    [SerializeField] private EnemyDeathChannel deathChannel;
    [SerializeField] private LevelUpChannel levelUpChannel;

    private void AddXP(EnemyDeathInfo info)
    {
        xpCurrent += info.info.xpValue;
        while (xpCurrent >= xpRequired)
        {
            currentLevel++;
            xpRequired += 10;
            if (currentLevel % 5 == 0) xpRequired += 10;
            xpCurrent -= xpRequired;
            levelUpChannel.Raise(currentLevel);
        }
    }

    private void Awake()
    {
        // xpRequired = 30f;
        xpCurrent = 0f;
    }

    private void OnEnable()
    {
        deathChannel.OnRaised += AddXP;
    }
    private void OnDisable() => deathChannel.OnRaised -= AddXP;
}
