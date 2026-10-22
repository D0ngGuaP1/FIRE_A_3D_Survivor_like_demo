using UnityEngine;
[CreateAssetMenu(menuName = "Enemy/EnemyInfo")]
public class EnemyInfoSO : ScriptableObject
{
    public float maxHealth;
    public float xpValue;

    public float attackDamage;
    public float armor;
    public float velocity;
}

public struct EnemyInfo
{
    public float maxHealth;
    public float xpValue;
    
    public float attackDamage;
    public float armor;
    public float velocity;

    public EnemyInfo(EnemyInfoSO info)
    {
        this.maxHealth = info.maxHealth;
        this.xpValue = info.xpValue;
        this.attackDamage = info.attackDamage;
        this.armor = info.armor;
        this.velocity = info.velocity;
    }
}
