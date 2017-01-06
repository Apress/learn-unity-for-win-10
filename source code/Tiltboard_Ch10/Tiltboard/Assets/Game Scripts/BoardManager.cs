using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {
	internal bool resetBoard = false; // flag to run the reset
	Quaternion from; // the object's current rotation when the reset is called
	// time slicers
	internal float endTime = 0.0f ;
	internal float matchTime = 0.0f ; // seconds duration of the pause

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (resetBoard) {
			float t = ( Time.time - endTime + matchTime) / matchTime ; // Goes from 0 to 1
			//print (t); // so you can see that the numbers do what they are supposed to do
			transform.rotation = Quaternion.Slerp(from, Quaternion.identity, t);
			if (Time.time > endTime) resetBoard = false;// reset the flag 
		}
	}

	public void StartBoardReset (float pauseTime) {
		matchTime = pauseTime;
		endTime = Time.time + matchTime;
		from = transform.rotation;
		resetBoard = true;
	}

}
