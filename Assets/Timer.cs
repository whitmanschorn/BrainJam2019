 /***
  * This class provides an in game loop timer mechanism
  ***/
 
 public class Timer  {
     float _timeElapsed;
     float _totalTime;
 
     public Timer(float timeToCountInSec){
         _totalTime = timeToCountInSec;
     }
 
     public bool UpdateAndTest(){
         _timeElapsed+= UnityEngine.Time.deltaTime;
         return _timeElapsed>=_totalTime;
     }
 
     public float Elapsed{
         get {return UnityEngine.Mathf.Clamp(_timeElapsed/_totalTime,0,1);}
     }
 }