using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankCanvas : MonoBehaviour
{
    [SerializeField] private Canvas startCanvas;
    [SerializeField] private Button backButton;

    [SerializeField] private List<Image> rankPanels;
    
#if !UNITY_WEBGL || UNITY_EDITOR
    void Awake()
    {
        backButton.onClick.AddListener(() =>
        {
            startCanvas.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        });
    }
    private void OnEnable()
    {
        var scores = SaveManager.Load().scores;
        for (int i = 0; i < scores.Count; i++)
        {
            if(i>=3) break;
            if(i>=scores.Count)  break;
            var rankPnael = rankPanels[i].GetComponent<RankPanel>();
            var score = scores[i];
            rankPnael.SetScore(score.name, score.score.ToString());
        }
    }
#endif
}
