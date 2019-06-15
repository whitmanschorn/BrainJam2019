﻿using UnityEngine;
 using System.Collections;

 public class DamagedFlash : MonoBehaviour {
     private Texture2D pixel;
     public Color color = Color.red;
     public float maxAlpha=1.0f;
     public float rampUpTime=0.1f;
     public float holdTime=0.1f;
     public float rampDownTime=1.0f;
 
     enum FLASHSTATE {OFF,DOWN}
     Timer timer;
     FLASHSTATE state = FLASHSTATE.OFF;
 
     
     // Use this for initialization
     void Start(){
         pixel = new Texture2D(1,1);
         color.a = 0f;
         pixel.SetPixel(0,0,color);
         pixel.Apply();
     }
 
     public void Update() {
         switch(state){
             case FLASHSTATE.DOWN:
                 if (timer.UpdateAndTest()){
                     state =FLASHSTATE.OFF;
                     timer = null;
                     SetPixelAlpha(0);
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
            case FLASHSTATE.DOWN:
                SetPixelAlpha(Mathf.Lerp(maxAlpha, 0, timer.Elapsed));
                break;
         }
         GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), pixel);
     }
 
     public void TookDamage(){
        if (state == FLASHSTATE.OFF)
         GetComponent<PlayerHealth>().ApplyDamage(1);
         timer = new Timer(rampUpTime);
         state = FLASHSTATE.DOWN;
     }
 
 }
