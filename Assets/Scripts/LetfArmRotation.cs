using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetfArmRotation : MonoBehaviour
{
    public int rotatinOffset =180;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diffirance = Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position;
        diffirance.Normalize();

        float rotZ = Mathf.Atan2(diffirance.y,diffirance.x)*Mathf.Rad2Deg;
        transform.rotation= Quaternion.Euler(0f,0f,rotZ+rotatinOffset);
    }
}
