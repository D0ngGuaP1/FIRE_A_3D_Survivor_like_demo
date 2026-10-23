using System;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class GlobalPopupSpawner : MonoBehaviour
{
    private ObjectPool<DamagePopup> regularDamageTextPool;
    private ObjectPool<DamagePopup> criticalDamageTextPool;
    
    [SerializeField] private TextMeshPro regularDamageText;
    [SerializeField] private TextMeshPro criticalDamageText;
    
    [SerializeField] private TakeDamageChannel takeDamageChannel;

    // private Action onRegularRelease;
    // private Action onCriticalRelease;

    private void Awake()
    {
        regularDamageTextPool = new ObjectPool<DamagePopup>(
            createFunc: () => Instantiate(regularDamageText).GetComponent<DamagePopup>(),
            actionOnGet: text => text.gameObject.SetActive(true),
            actionOnRelease: text => text.gameObject.SetActive(false),
            collectionCheck: true,
            defaultCapacity: 64,
            maxSize: 512
        );
        criticalDamageTextPool = new ObjectPool<DamagePopup>(
            createFunc:() => Instantiate(criticalDamageText).GetComponent<DamagePopup>(),
            actionOnGet: (text) => text.gameObject.SetActive(true),
            actionOnRelease: text => text.gameObject.SetActive(false),
            collectionCheck: true,
            defaultCapacity: 64,
            maxSize: 512
        );
    }

    private void OnEnable()
    {
        takeDamageChannel.OnRaised += ShowPopup;
    }
    private void OnDisable() => takeDamageChannel.OnRaised -= ShowPopup;

    private void ShowPopup(OnHitContext ctx)
    {
        bool isCrit = ctx.isCrit;
        var pool = isCrit ? criticalDamageTextPool : regularDamageTextPool;
        DamagePopup p =  pool.Get();
        p.transform.position = ctx.hitPosition + Vector3.up * 2;
        string damageText = ctx.damageValue.ToString();
        if (isCrit) damageText += "!";
        p.Setup(
            damageText,
            () => pool.Release(p)
            );
    }
}
