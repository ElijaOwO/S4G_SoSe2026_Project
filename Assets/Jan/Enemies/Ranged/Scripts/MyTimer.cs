using UnityEngine;

public class MyTimer
{
   public float maxTime = 1;
   
   private float currentTime = 0;
   public float CurrentTime { get { return currentTime; } }

   // Update is called once per frame
   public void Update(float deltaTime)
   {
       currentTime += deltaTime;
   }

   public bool TimeOut()
   {
       if (currentTime > maxTime)
       {
           return true;
       }
       
       return false;
   }

   public void Reset()
   {
       currentTime = 0;
   }
}
