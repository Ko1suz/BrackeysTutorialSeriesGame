using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    public int rotatinOffset =90;

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuUI.GameIsPaused)
        {
             Vector3 diffirance = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
             diffirance.Normalize();

            float rotZ = Mathf.Atan2(diffirance.y,diffirance.x)*Mathf.Rad2Deg;
            transform.rotation= Quaternion.Euler(0f,0f,rotZ+rotatinOffset);
        }
    }
}
