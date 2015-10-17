using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Vuforia;

public class ArrowButtoneventHandler : MonoBehaviour, IVirtualButtonEventHandler
{
	
	
	private GameObject _modelBuilding;
	private GameObject _modelRoom;
	private GameObject _modelCourt;
	public float perspectiveZoomSpeed = 0.5f;
	public float orthoZoomSpeed = 0.5f; 
	
	private int viewIndex;
	void Start() {
		viewIndex = 0;
		VirtualButtonBehaviour[] vb = GetComponentsInChildren<VirtualButtonBehaviour>();
		for (int i = 0; i < vb.Length; ++i) {
			Debug.Log("Registered event listeners fine");
			vb[i].RegisterEventHandler(this);

		}
		Debug.Log("running fine");
		_modelBuilding = transform.FindChild("building").gameObject;
		_modelRoom = transform.FindChild("room").gameObject;
		_modelCourt = transform.FindChild("embassy_court").gameObject;
		_modelRoom.SetActive(false);
		_modelBuilding.SetActive (false);
		_modelCourt.SetActive (true);

	}
	
	void Update() {
		if (Input.GetMouseButtonDown (0) && Touch.tapCount < 2) {
			this.updateAugmentedRealityView ();
			Debug.Log ("input button detect");
		}

		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			// If the camera is orthographic...
			if (camera.isOrthoGraphic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				
				// Make sure the orthographic size never drops below zero.
				camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
			}
			else
			{
				// Otherwise change the field of view based on the change in distance between the touches.
				camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
			}
		}

	}


	
	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb) {
		Debug.Log("OnButtonPressed for " + vb.name );
		this.updateAugmentedRealityView ();
	}

	public void updateAugmentedRealityView() {
		viewIndex++;
		viewIndex = viewIndex % 3;
		switch(viewIndex) {
		case 0:
			_modelBuilding.SetActive(false);
			_modelRoom.SetActive(false);
			_modelCourt.SetActive(true);
			break;
		case 1:
			_modelBuilding.SetActive(true);
			_modelRoom.SetActive(false);
			_modelCourt.SetActive(false);
			break;
		case 2:
			_modelBuilding.SetActive(false);
			_modelRoom.SetActive(true);
			_modelCourt.SetActive(false);
			break;
		default:
			throw new UnityException("Button not supported: ");
			break;
		}
	}
	
	/// <summary>
	/// Called when the virtual button has just been released:
	/// </summary>
	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) { }
}