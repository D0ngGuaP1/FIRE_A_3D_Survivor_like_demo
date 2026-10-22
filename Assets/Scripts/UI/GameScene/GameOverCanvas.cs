using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverCanvas : MonoBehaviour
{
    public int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button backButton;
    [SerializeField] private TMP_InputField  nameInputField;
    void Start()
    {
        backButton.onClick.AddListener(() =>
        {
            if (nameInputField.text == "")
            {
                SceneManager.LoadScene("StartScene");
                return;
            }
#if !UNITY_WEBGL || UNITY_EDITOR
            var scores = SaveManager.Load();
            var scoreEntity = new ScoreEntity(
                score, 
                nameInputField.text
            );
            scores.scores.Add(scoreEntity);
            SaveManager.Save(scores);
            SceneManager.LoadScene("StartScene");
#endif   
        });
    }
    void OnEnable()
    {
        scoreText.text = score.ToString();
    }
}
