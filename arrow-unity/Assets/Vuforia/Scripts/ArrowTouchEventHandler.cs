using UnityEngine;
using System.Collections;

public class ArrowTouchEventHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)){ // if left button pressed...
			Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)){
				if(hit.collider.tag == "embassy_court")
				{
					GameObject obj=GameObject.FindGameObjectWithTag("embassy_court");
					//obj.GetComponent<Animation>().Play("cube");
					Debug.Log("screen touch detected");
				}
			}
		}
	}
}
