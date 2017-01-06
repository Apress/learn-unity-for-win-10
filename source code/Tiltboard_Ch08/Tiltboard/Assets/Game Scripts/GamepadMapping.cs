using UnityEngine;
using System.Collections;

public class GamepadMapping : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Test Axes") != 0) print(Input.GetAxis("Test Axes"));

		if (Input.GetKeyDown(KeyCode.Joystick1Button0)) print("A");
		if (Input.GetKeyDown(KeyCode.Joystick1Button1)) print("B");
		if (Input.GetKeyDown(KeyCode.Joystick1Button2)) print("X");
		if (Input.GetKeyDown(KeyCode.Joystick1Button3)) print("Y");
		if (Input.GetKeyDown(KeyCode.Joystick1Button4)) print("LB");
		if (Input.GetKeyDown(KeyCode.Joystick1Button5)) print("RB");
		if (Input.GetKeyDown(KeyCode.Joystick1Button6)) print("Back");		
		if (Input.GetKeyDown(KeyCode.Joystick1Button7)) print("Start");	
	}
}
