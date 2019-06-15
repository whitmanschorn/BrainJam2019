﻿using UnityEngine;
 using System.Collections;

 public class DamagedFlash : MonoBehaviour {
     private Texture2D pixel;
     public Color color = Color.red;
     public float startAlpha=0.5f;
     public float maxAlpha=1.0f;
     public float rampUpTime=0.5f;
     public float holdTime=0.1f;
     public float rampDownTime=1.0f;
 
     enum FLASHSTATE {OFF,UP,HOLD,DOWN}
     Timer timer;
     FLASHSTATE state = FLASHSTATE.OFF;
 
     
     // Use this for initialization
     void Start(){
         pixel = new Texture2D(1,1);
         color.a = startAlpha;
         pixel.SetPixel(0,0,color);
         pixel.Apply();
         // for testing
         //TookDamage(new DamagePacket(1));
     }
 
     public void Update() {
         switch(state){
             case FLASHSTATE.UP:
                 if (timer.UpdateAndTest()){
                     state =FLASHSTATE.HOLD;
                     timer = new Timer(holdTime);
                 }
                 break;
             case FLASHSTATE.HOLD:
                 if (timer.UpdateAndTest()){
                     state =FLASHSTATE.DOWN;
                     GetComponent<PlayerHealth>().ApplyDamage(35);
                     timer = new Timer(rampDownTime);
                 }
             break;
             case FLASHSTATE.DOWN:
                 if (timer.UpdateAndTest()){
                     state =FLASHSTATE.OFF;
                     timer = null;
                 }
             break;
         }
     }
 
     private void SetPixelAlpha(float a){
         color.a = a;
         pixel.SetPixel(0,0,color);
         pixel.Apply();
     }
 
     public void OnGUI(){
         switch(state){
             case FLASHSTATE.UP:
                 SetPixelAlpha(Mathf.Lerp(startAlpha,maxAlpha,timer.Elapsed));
                 break;
             case FLASHSTATE.DOWN:
                 SetPixelAlpha(Mathf.Lerp(maxAlpha,startAlpha,timer.Elapsed));
                 break;
         }
         GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), pixel);
     }
 
     public void TookDamage(){
        if(state == FLASHSTATE.OFF)
         timer = new Timer(rampUpTime);
         state = FLASHSTATE.UP;
     }
 
 }
