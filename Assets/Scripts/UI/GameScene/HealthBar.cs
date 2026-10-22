using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image delayHealthBar;

    [SerializeField] private float currentLerpSpeed;
    [SerializeField] private float delayLerpSpeed;

    private float healthRatio = 1f;

    private float currentFillAmount;
    private float delayFillAmount;

    private float currentT;
    private float delayT;
    
    [SerializeField] private PlayerTakeDamageChannel takeDamageChannel;

    // Update is called once per frame
    void Update()
    {
        if(currentFillAmount == healthRatio && delayFillAmount == healthRatio) return;

        if (currentFillAmount != healthRatio)
        {
            currentT = 1f - Mathf.Exp(-currentLerpSpeed * Time.deltaTime);
            currentFillAmount = Mathf.Lerp(currentFillAmount, healthRatio, currentT);
            if (Mathf.Abs(currentFillAmount - healthRatio) <= 0.001f)
            {
                currentHealthBar.fillAmount = healthRatio;
            }
            currentHealthBar.fillAmount = currentFillAmount;
        }
        
        delayT =  1f - Mathf.Exp(-delayLerpSpeed * Time.deltaTime);
        delayFillAmount =  Mathf.Lerp(delayFillAmount, healthRatio, delayT);
        if (Mathf.Abs(delayFillAmount - healthRatio) <= 0.001f)
        {
            delayHealthBar.fillAmount = healthRatio;
        }
        delayHealthBar.fillAmount = delayFillAmount;
    }

    private void OnEnable()
    {
        currentFillAmount = 1f;
        delayFillAmount = 1f;
        takeDamageChannel.OnRaised += UpdateHealthBar;
    }

    private void OnDisable()
    {
        takeDamageChannel.OnRaised -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthRatio = currentHealth / maxHealth;
    }
}
