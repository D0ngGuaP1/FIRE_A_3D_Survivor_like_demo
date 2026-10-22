using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    private Button self;
    [SerializeField] AcquireRewardChannel acquireRewardChannel;
    
    public RewardInfoSO reward;

    [SerializeField] private TextMeshProUGUI rewardName;
    [SerializeField] private TextMeshProUGUI rewardDescription;
    
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<Button>();
        self.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            Debug.Log(reward);
#endif
            acquireRewardChannel.Raise(reward);
        });
    }

    private void OnEnable()
    {
        rewardName.text = reward.displayName;
        rewardDescription.text = reward.description;
    }

    void OnDestroy() => self.onClick.RemoveAllListeners();

}
