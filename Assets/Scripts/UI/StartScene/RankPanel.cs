
using System;
using TMPro;
using UnityEngine;

public class RankPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI playerScore;

    private void Awake()
    {
        // playerName = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        // playerScore = transform.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(string playerName, string score)
    {
        this.playerName.text = playerName;
        this.playerScore.text = score;
        if(score == "0") this.playerScore.text = "00";
    }
}
