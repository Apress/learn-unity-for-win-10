using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovable : MonoBehaviour {

	private bool isDragging = false;
	private Camera theCamera;
	private Transform currentBananaPeel;

	public Collider boardSurfaceCollider;
	internal bool isPaused = false;

	private Persistent persistent;
	private Text barrowCount;

	private Toggle barrowToggle;

	public bool usingGamepad = false;
	public Transform pointer;
	private Vector2 pointerPosition;

	GameHUD gameHUD;

	bool empty = false; // flag for turning off pointer if no more Wheelbarrows

	// Use this for initialization
	void Start () {
		theCamera = Camera.main;
		boardSurfaceCollider = GameObject.Find("Board Surface").GetComponent<Collider>();
		persistent = GameObject.Find("Holder").GetComponent<Persistent>();
		barrowCount = GameObject.Find ("Count B").GetComponent<Text>();
		barrowToggle = GameObject.Find("Wheelbarrows").GetComponent<Toggle>();
		gameHUD = GameObject.Find("HUD Manager").GetComponent<GameHUD>();
	}
	
	// Update is called once per frame
	void Update () {
		if(!isPaused) return;

		if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f) {
			usingGamepad = true; //
		}

		// Handle native touch events
		foreach (Touch touch in Input.touches) {
			HandleTouch(touch.fingerId, touch.position, touch.phase);
		}

		// Simulate touch events from mouse events
		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown(0) ) {
				HandleTouch(10, Input.mousePosition, TouchPhase.Began);
			}
			if (Input.GetMouseButton(0) ) {
				HandleTouch(10, Input.mousePosition, TouchPhase.Moved);
			}
			if (Input.GetMouseButtonUp(0) ) {
				HandleTouch(10, Input.mousePosition, TouchPhase.Ended);
			}	
		}

		if (usingGamepad) {
			pointerPosition = new Vector2 (pointer.position.x,pointer.position.y);
			if (isDragging) HandleTouch(10, pointerPosition, TouchPhase.Moved);
			if(Input.GetButtonDown("Submit")) {
				HandleTouch(10, pointerPosition, TouchPhase.Began);
				pointer.GetComponent<CustomPointer>().SetPointerState(2);
				// turn on working pointer
			}

			if(Input.GetButtonUp("Submit")) {
				HandleTouch(10, pointerPosition, TouchPhase.Ended);
				if(empty) return;
				pointer.GetComponent<CustomPointer>().SetPointerState (1);
				// turn on regular pointer
			}

			if(Input.GetButtonDown("Cancel")) {
				StopMovable ();
			}
		} // close usingGamepad conditional
	}

	private void HandleTouch(int touchFingerId, Vector2 touchPosition, TouchPhase touchPhase) {

		Ray ray = theCamera.ScreenPointToRay(touchPosition);
		RaycastHit[] hits;
		int i = 0;

		switch (touchPhase) {
		case TouchPhase.Began:
			// mouse down or finger touch
			//print("touch down");
			hits = Physics.RaycastAll(ray);
			while (i < hits.Length) {
				RaycastHit hit = hits[i];
				if (hit.collider.name == "Banana Peel") {
					if (persistent.wheelbarrows == 1){
						barrowToggle.interactable = false; // disable the toggle
						empty = true;
						return;
					}
					isDragging = true;
					persistent.wheelbarrows --; // decrement the perks
					barrowCount.text = persistent.wheelbarrows.ToString();// update the UI
					currentBananaPeel = hit.collider.transform;
					if (usingGamepad) pointer. GetComponent<CustomPointer>().SetPointerState(2); // turn on working pointer
					if (persistent.wheelbarrows == 0) barrowToggle.interactable = false;	// disable the toggle
					return;
				}
				i++;
			}
			break;

		case TouchPhase.Moved:
			if(!isDragging) return;
			hits = Physics.RaycastAll(ray);
			while (i < hits.Length) {
				RaycastHit hit = hits[i];
				if (hit.collider == boardSurfaceCollider) {
					Vector3 temp = new Vector3 (currentBananaPeel.position.x,currentBananaPeel.position.y, currentBananaPeel.position.z);
					temp = hits[i].point;
					currentBananaPeel.position = temp;
				}
				i++;
			}
			break;

		case TouchPhase.Ended:
			isDragging = false;
			// mouse or finger up
			//print("mouse up");
			if (empty) pointer.GetComponent<CustomPointer>().ResetPointer();
			break;
		}
	}

	void StopMovable () {
		// turn off and reset pointer
		pointer.GetComponent<CustomPointer>().ResetPointer();
		// reset GUI focus
		gameHUD.ResetFocus();
		usingGamepad = false;
	}
}
