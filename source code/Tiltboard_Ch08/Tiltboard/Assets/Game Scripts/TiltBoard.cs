using UnityEngine;
using System.Collections;

public class TiltBoard : MonoBehaviour {
	
	public float factor = 0.6f; // adjustment
	Vector3 v3StartPos; // mouse location
	Vector3 v3StartRot; // board orientation

	public float speed = 1500f;
	bool gamepad = false; // is there a gamepad active on the system
	bool useGamepad; // flag to bypass update code


	// Use this for initialization
	void Start () {
		if (Input.GetJoystickNames().Length > 0) gamepad = true;;
	}
	
	// Update is called once per frame
	void Update () {
//		float vert = Input.GetAxis("Vertical") * speed * Time.deltaTime; 
//		float hor = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
//		transform.Rotate(vert, 0, hor,Space.World); 

		float xRotation = Input.GetAxis("Vertical")  * speed * Time.deltaTime;
		float zRotation = -Input.GetAxis("Horizontal")   * speed * Time.deltaTime;
		if (xRotation > 0.5f || zRotation > 0.5f) useGamepad = true; 

		if (gamepad && useGamepad) {
			transform.eulerAngles = new Vector3(xRotation , 0, zRotation);
		}
	}

	void OnMouseDown(){
		useGamepad = false;
		v3StartPos = Input.mousePosition; // starting location of the cursor
		v3StartRot = transform.eulerAngles; // current orientation of board
	}

	void OnMouseDrag(){
		Vector3 v3T  = Input.mousePosition; // get the current cursor position
		transform.eulerAngles = v3StartRot + (Vector3.left * (v3StartPos - v3T).y * factor) + (Vector3.forward * (v3StartPos - v3T).x * factor);
	}



}
