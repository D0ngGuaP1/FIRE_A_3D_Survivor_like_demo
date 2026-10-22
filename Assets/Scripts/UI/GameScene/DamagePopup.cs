using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePopup : MonoBehaviour
{
    [Header("配置")]
    public float moveSpeed = 1.5f;      // 向上飘的速度
    public float fadeDuration = 0.5f;  // 淡出耗时
    public Vector3 moveDirection = Vector3.up; // 飘动方向
    
    private TextMeshPro textMesh;
    private Color originalColor;

    private System.Action releaseAction;

    void Awake()
    {
        // 获取 TMP 组件并保存原始颜色
        textMesh = GetComponent<TextMeshPro>();
        originalColor = textMesh.color;
    }

    // 外部调用此方法来初始化飘字
    public void Setup(string damageText, System.Action onRelease)
    {
        textMesh.text = damageText;
        
        originalColor = textMesh.color;
        releaseAction = onRelease;
        textMesh.text = damageText;
        
        // 生成时添加一点随机水平偏移，避免多个飘字完全重叠
        float randomX = Random.Range(-1f, 1f) + 1.5f;
        float randomZ = Random.Range(-1f, 1f) + 1.5f;
        Vector3 randomOffset = new Vector3(randomX, 0, randomZ);
        
        // 启动动画协程
        StartCoroutine(AnimateRoutine(randomOffset));
    }

    private IEnumerator AnimateRoutine(Vector3 initialOffset)
    {
        // 1. 初始位置加上随机偏移
        Vector3 startPos = transform.position + initialOffset;
        transform.position = startPos;

        // 2. 同时进行：向上移动 + 淡出
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            // 移动
            transform.position += moveDirection * (moveSpeed * Time.deltaTime);
            
            // 计算透明度 (从 1 到 0)
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            
            elapsed += Time.deltaTime;
            yield return null; // 等待下一帧
        }

        releaseAction?.Invoke();
    }
    
    void LateUpdate()
    {
        if (Camera.main != null)
            transform.forward = Camera.main.transform.forward;
    }
}