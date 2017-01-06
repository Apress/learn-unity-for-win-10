using UnityEngine;
using System.Collections;

public class PortalHopper : MonoBehaviour {
	public MarbleManager marbleManager; // the MarbleManager script
	public Transform[] destination; // the possible target location
	internal float delay = 0.0f; // the time it will take to move the marble
	Collider marbleCollider; // the marble's collider

	// Use this for initialization
	void Start () {
		marbleCollider = marbleManager.gameObject.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider theCollider) {
		// if the collider didn't belong to the Marble object, leave the function
		if(theCollider != marbleCollider) return;
		int num = Random.Range (0,destination.Length);
		Transform tempDestination = destination[num];

		//print (theCollider.gameObject.name);
		marbleManager.PortalJump (delay,tempDestination);
	}


}
