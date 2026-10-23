using System.Collections.Generic;
using UnityEngine;

public class StatusHandler : MonoBehaviour
{
    private List<StatusInstance> instances;
    private Health health;
    
    private bool isDead = false;

    private void Awake()
    {
        instances = new List<StatusInstance>();
        health = GetComponent<Health>();
    }

    void OnEnable()
    {
        isDead = false;
        instances.Clear();
        health.OnDied += OnDied;
    }

    void OnDisable()
    {
        isDead = true;
        health.OnDied -= OnDied;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        
        for (int i = instances.Count - 1; i >= 0; i--)
        {
            if(isDead) return;
            
            var status = instances[i];
            status.Tick(Time.deltaTime);
            
            if (status.isExpire)
            {
                if(isDead) return;
                status.Expire();
                instances.Remove(status);
            }
        }
    }
    public void ReceiveStatus(StatusInstance status)
    {
        foreach (var instance in instances)
        {
            if (status.GetType() == instance.GetType())
            {
                if(status.canStack && instance.canStack)
                {
                    instance.duration =  status.duration;
                    instance.stackCount += status.stackCount;
                    instance.stackLimit = status.stackLimit;
                    if(instance.stackCount > instance.stackLimit) instance.stackCount = instance.stackLimit;
                }
                else instance.duration = status.duration;
                return;
            }
        }
        status.Apply();
        instances.Add(status);
    }

    protected void OnDied()
    {
        isDead = true;
        foreach (var instance in instances)
        {
            instance.Expire();
        }
        instances.Clear();
    }
}
