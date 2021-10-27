using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManagement : MonoBehaviour
{
    float rotateSpeed = 0.5f;
    Vector3 previousPosition = Vector3.zero;
    Vector3 deltaPosition = Vector3.zero;
    Touch touch;
    float rotationRate = 0.08f;
    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            RotateObject();
        } 
    }

    void RotateObject()
    {
        touch = Input.GetTouch(0);

        ObjectManager.CurrentModelObject.transform.Rotate(touch.deltaPosition.y * rotationRate, 
                                 -touch.deltaPosition.x * rotationRate, 0, Space.World);
    }
}
