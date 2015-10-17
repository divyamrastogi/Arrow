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
		if (Input.GetMouseButtonDown (0)) {
			this.updateAugmentedRealityView ();
			//_modelCourt.transform.localScale +=  new Vector3(0.4f, 0.4f, 0.4f);
			Debug.Log ("input button detect");
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