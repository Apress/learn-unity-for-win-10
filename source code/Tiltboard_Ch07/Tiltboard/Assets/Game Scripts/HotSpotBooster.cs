using UnityEngine;
using System.Collections;

public class HotSpotBooster : MonoBehaviour {
	public Booster booster; // the Booster script

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider theCollider) {
		// if the collider didn't belong to the Marble object, leave the function
		if(theCollider.gameObject.name != "Marble") return;
		//trigger the boost
		booster.Boost (); // trigger the boost
	}

}
