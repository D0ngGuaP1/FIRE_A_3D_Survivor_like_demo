using UnityEngine;

public class OnHitContext
{
    public float damageValue { get; set; } = 0f;
    public bool isCrit { get; set; } = false;
    
    public Vector3 hitPosition { get; set; } = Vector3.zero;
    public Vector3 hitDirection {get; set;} =  Vector3.zero;
    
    public GameObject target { get; set; } = null;
    public Health health { get; set; } = null;
    
    public StatusHandler statusHandler { get; set; } = null;
    public GameObject source { get; set; } = null;
    
    
    public void Clear()
    {
        damageValue = 0f;
        isCrit = false;
        hitPosition = Vector3.zero;
        hitDirection = Vector3.zero;
        source = null;
        health = null;
        statusHandler = null;
        target = null;
    }
}
