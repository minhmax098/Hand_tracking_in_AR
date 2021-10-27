using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManagement : MonoBehaviour
{
	float perspectiveZoomSpeed = 0.03f;        
	float orthoZoomSpeed = 0.03f;

    Touch touchZero;
    Touch touchOne;
	Vector2 touchZeroPrevPos;
	Vector2 touchOnePrevPos;
	float prevTouchDeltaMag;
	float touchDeltaMag;
	float deltaMagnitudeDiff;

	void Start()
	{
		
	}
	
	void Update()
	{
        if (Input.touchCount == 2)
        {
		    ScaleObjectWithTouch();
        }
	}

	private void ScaleObjectWithTouch()
	{
		touchZero = Input.GetTouch(0);
		touchOne = Input.GetTouch(1);

		touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

		prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

		// Find the difference in the distances between each frame.
		deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
		// If the camera is orthographic...
		if (CameraManager.Instance.currentCamera.orthographic)
		{
			// ... change the orthographic size based on the change in distance between the touches.
			CameraManager.Instance.currentCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

			// Make sure the orthographic size never drops below zero.
			CameraManager.Instance.currentCamera.orthographicSize = Mathf.Max(CameraManager.Instance.currentCamera.orthographicSize, 10f);
		}
		else
		{
			// Otherwise change the field of view based on the change in distance between the touches.
			CameraManager.Instance.currentCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

			// Clamp the field of view to make sure it's between 0 and 90.
			CameraManager.Instance.currentCamera.fieldOfView = Mathf.Clamp(CameraManager.Instance.currentCamera.fieldOfView, 10f, 90f);
		}
	}
}