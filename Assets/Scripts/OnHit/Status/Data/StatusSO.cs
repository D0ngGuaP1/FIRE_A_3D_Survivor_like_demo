using UnityEngine;

public class StatusSO : ScriptableObject
{
   public float duration;
   public float interval;
   public bool hasInterval;
   public bool canStack;
   public int stackCount;
   public int stackLimit;
}

public abstract class StatusInstance
{
   public float duration;
   public float interval;
   public bool hasInterval;
    
   public float timer;
   public bool isExpire = false;
    
   public bool canStack = false;
   public int stackLimit;
   public int stackCount;

   public void Tick(float deltaTime) //Not Unity Life cycle method, called by StatusHandler per frame
   {
      duration -= deltaTime;
      if (hasInterval)
      {
         timer += deltaTime;
         if (timer >= interval)
         {
            timer -= interval;
            OnUpdate();
         }
      }
      if (duration <= 0) isExpire = true;
   }
   protected abstract void OnUpdate();
   public abstract void Apply();
   public abstract void Expire();

}

public abstract class StatusSnapshot
{
   public float duration;
   public float interval;
   public bool hasInterval;
   public bool canStack;
   public int stackCount;
   public int stackLimit;

   public StatusSnapshot(StatusSO data)
   {
      duration = data.duration;
      interval = data.interval;
      hasInterval = data.hasInterval;
      canStack = data.canStack;
      stackCount = data.stackCount;
      stackLimit = data.stackLimit;
   }
}