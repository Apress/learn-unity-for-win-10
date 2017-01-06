using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomPointer : MonoBehaviour {

	public float speed = 200f;

	public Image pointer;
	public Image pointerWorking;
	public Toggle wheelbarrows;
	bool isActive = false;
	internal bool usingGamepad = false; // no gamepad available

	public PlayerMovable playerMovable;

	// Use this for initialization
	void Start () {
		if (Input.GetJoystickNames().Length > 0)	usingGamepad = true;
		SetPointerState(0); // turn off both pointer images
	}
	
	// Update is called once per frame
	void Update () {
		if (!usingGamepad || !isActive) return;
//		transform.Translate (Vector2.right * speed * Time.unscaledDeltaTime * Input.GetAxis
//			("Horizontal2"));
//		transform.Translate (Vector2.up * speed * Time.unscaledDeltaTime * Input.GetAxis
//			("Vertical2"));

		float deadzone = 0.25f; // this will override the 0 value set in the Input 	Manager
		Vector2 stickInput = new Vector2(Input.GetAxis("Horizontal2"),	Input.GetAxis("Vertical2"));
		if(stickInput.magnitude < deadzone)
			stickInput = Vector2.zero;
		else
			stickInput = stickInput.normalized * ((stickInput.magnitude -	deadzone) / (1 - deadzone));
			transform.Translate (Vector2.right * speed * Time.unscaledDeltaTime	* stickInput.x);
			transform.Translate (Vector2.up * speed * Time.unscaledDeltaTime *	stickInput.y);
	}

	public void SetPointerState (int state) {
		switch (state) {
		case 0: // neither image rendered
			pointer.enabled = false;
			pointerWorking.enabled = false;
			break;
		case 1: // pointer rendered
			pointer.enabled = true;
			pointerWorking.enabled = false;
			break;
		case 2: // pointerWorking rendered
			pointer.enabled = false;
			pointerWorking.enabled = true;
			break;
		}
	}

	public void ActivatePointer (){
		if(!playerMovable.usingGamepad) return;
		isActive = true;
		SetPointerState(1); //temporary
	}

	public void ResetPointer(){
		isActive = false;
		SetPointerState(0); // turn off both pointer images
		Vector3 tempPos = new Vector3(transform.position.x, transform.position.y,
			transform.position.z);
		tempPos.x = Screen.width/2;
		tempPos.y = Screen.height/2;
		transform.position = tempPos;
	}
}
