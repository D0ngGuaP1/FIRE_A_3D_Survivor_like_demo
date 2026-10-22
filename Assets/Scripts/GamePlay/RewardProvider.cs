using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardProvider : MonoBehaviour
{
    [SerializeField] private List<RewardInfoSO> rewards_rare;
    [SerializeField] private List<RewardInfoSO> rewards_epic;

    public List<RewardInfoSO> rewardToProvide;
    private int rareRate = 80;
    // Start is called before the first frame update
    void Awake()
    {
        rewardToProvide = new List<RewardInfoSO>();
    }

    public void OnLevelUp(int currentLevel)
    {
        bool isRare = false;
        rewardToProvide.Clear();
        if (currentLevel % 5 == 0) rareRate -= 10;
        if(rareRate <= 0) rareRate = 0;
        for (int i = 0; i < 3; i++)
        {
            isRare = Random.Range(0, 100) < rareRate ? true : false;
            if (isRare)
            {
                int index = Random.Range(0, rewards_rare.Count);
                rewardToProvide.Add(rewards_rare[index]);
            }
            else
            {
                int index = Random.Range(0, rewards_epic.Count);
                rewardToProvide.Add(rewards_epic[index]);
            }
        }
    }
}
