using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private DamageInfoSO damageInfoSO;
    public DamageInfo damageInfo;
    public float originalDamage {get; private set;}
    private BulletSpawner bulletSpawner;

    [SerializeField] private WeaponInfoSO weaponInfoSO;
    public WeaponInfo weaponInfo;

    private float fireTimer = 0f;
    private float burstTimer = 0f;
    
    private bool isBursting = false;
    private int burstLeftCount;

    private Transform firePosition;

    private void Awake()
    {
        damageInfo = new DamageInfo(damageInfoSO);
        weaponInfo = new WeaponInfo(weaponInfoSO);
        originalDamage = damageInfo.damageValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletSpawner = GetComponent<BulletSpawner>();
        burstLeftCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBursting)
        {
            burstTimer += Time.deltaTime;
            if (burstTimer >= weaponInfo.burstInterval && burstLeftCount > 0)
            {
                bulletSpawner.Fire(weaponInfo.onHitEffects, firePosition, damageInfo, weaponInfo.hitCount);
                burstTimer = 0f;
                burstLeftCount--;
            }
            if (burstLeftCount <= 0) isBursting = false;
        }
        else
        {
            fireTimer += Time.deltaTime;
        }
    }

    public void TryFire(Transform firePos)
    {
        if (isBursting) return;
        if (fireTimer < weaponInfo.fireInterval) return;
        
        fireTimer = 0f;
        burstTimer = weaponInfo.fireInterval;
        burstLeftCount = weaponInfo.burstCount;
        isBursting = true;
        firePosition = firePos;
        return; //  TODO:return boolean maybe
    }
}
