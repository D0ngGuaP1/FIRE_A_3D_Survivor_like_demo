using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private LevelUpChannel levelUpChannel;
    [SerializeField] private AcquireRewardChannel acquireRewardChannel;
    [SerializeField] private EnemyDeathChannel enemyDeathChannel;
    [SerializeField] private PlayerDeathChannel playerDeathChannel;
    
    [SerializeField] private Canvas rewardCanvas;
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas HealthBarCanvas;
    private RewardProvider rewardProvider;
    
    [SerializeField] private List<Button> Buttons;
    private List<RewardButton> rewardButtons = new List<RewardButton>();

    private int score = 0;

    void Awake()
    {
        rewardProvider = GetComponent<RewardProvider>();
        foreach (var button in Buttons)
        {
            var script = button.GetComponent<RewardButton>();
            rewardButtons.Add(script);
        }
    }
    
    private void ShowRewardUI(int level)
    {
        rewardProvider.OnLevelUp(level);
        for (int i = 0; i < rewardButtons.Count; i++)
        {
            rewardButtons[i].reward = rewardProvider.rewardToProvide[i];
        }
        rewardCanvas.gameObject.SetActive(true);
    }

    private void HideRewardUI(RewardInfoSO reward)
    {
        rewardCanvas.gameObject.SetActive(false);
    }
    
    private void OnEnable(){
        levelUpChannel.OnRaised += ShowRewardUI;
        acquireRewardChannel.OnRaised += HideRewardUI;
        enemyDeathChannel.OnRaised += CountScore;
        playerDeathChannel.OnRaised += OnPlayerDeath;
        score = 0;
    }
    private void OnDisable()
    {
        levelUpChannel.OnRaised -= ShowRewardUI;
        acquireRewardChannel.OnRaised -= HideRewardUI;
        enemyDeathChannel.OnRaised -= CountScore;
        playerDeathChannel.OnRaised -= OnPlayerDeath;
    }

    private void CountScore(EnemyDeathInfo enemyDeathInfo)
    {
        score += Mathf.RoundToInt(enemyDeathInfo.info.xpValue);
    }

    private void OnPlayerDeath()
    {
        var script = gameOverCanvas.GetComponent<GameOverCanvas>();
        script.score = score;        
        gameOverCanvas.gameObject.SetActive(true);
        HealthBarCanvas.gameObject.SetActive(false);
    }
}
