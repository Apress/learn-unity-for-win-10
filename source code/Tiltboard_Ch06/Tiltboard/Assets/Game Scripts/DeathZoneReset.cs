using UnityEngine;
using System.Collections;

public class DeathZoneReset : MonoBehaviour {
	public GameObject marble;  // the marble object

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit () {
		//print ("got one!");
		marble.GetComponent<MarbleManager>().StartDelay (3f); // set the delay going using 3 seconds

	}

}
