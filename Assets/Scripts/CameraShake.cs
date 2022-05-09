using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Camera MainCam;
    float shakeAmount = 0;


    private void Awake() {
        if (MainCam==null)
        {
            MainCam = Camera.main;
        }
    }
    
    private void Update() {
        if (!PauseMenuUI.GameIsPaused)
        {
             if (Input.GetMouseButton(0))
        {
            Shake(0.07f,0.08f);
        }
        }
    }

    public void Shake(float amount, float lenght){
        shakeAmount = amount;
        InvokeRepeating("BeginShake",0,0.01f);
        Invoke("StopShake",lenght);
    }

    void BeginShake(){
        if (shakeAmount>0)
        {
            Vector3 camPos = MainCam.transform.position;

            float offsetX = Random.value *shakeAmount*2 -shakeAmount;
            float offsetY = Random.value *shakeAmount*2 -shakeAmount;
            camPos.x += offsetX;
            camPos.y += offsetY;

            MainCam.transform.position = camPos;
        }
    }
    void StopShake(){
        CancelInvoke("BeginShake");
        MainCam.transform.localPosition = Vector3.zero;
    }
}
