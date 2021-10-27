using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] public Camera currentCamera;    

    private static CameraManager instance;
    public static CameraManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CameraManager>();
            }
            return instance;
        }   
    }
    
    private void Awake()
    {
        currentCamera.transform.localPosition = CameraConfig.cameraOriginalPosition;
        currentCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
        currentCamera.transform.localScale = Vector3.one;
    }

}
