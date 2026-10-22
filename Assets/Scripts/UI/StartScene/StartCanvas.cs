using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCanvas : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button rankButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Canvas rankCanvas; 

    // Start is called before the first frame update
    void Awake()
    {
        startButton.onClick.AddListener(() => { Addressables.LoadSceneAsync("GameScene"); });
        rankButton.onClick.AddListener(() =>
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            rankCanvas.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
#else
            Debug.Log("WebGL版本不支持此功能");
#endif
        });
        quitButton.onClick.AddListener(() => { Application.Quit(); });
    }
}
