using UnityEngine;
using System.Collections;

public class Traveler : MonoBehaviour {
	
	public WaypointManager waypointManager;
	internal Transform currentTarget;
	int speed = 8;

	Persistent persistent; // holds difficulty settings 

	// Use this for initialization
	void Start () {
		persistent = GameObject.FindWithTag("Holder").GetComponent<Persistent>();
		speed = 3 * persistent.difficulty + Random.Range(0,3); 

		// get first target
		waypointManager.GetNewTarget(this); // request a valid target for this object
		transform.LookAt(currentTarget); // turn this object to face the new target
		speed = Random.Range(speed-2, speed); // randomize the speed so all are slightly different
	}
	
	// Update is called once per frame
	void Update () {
		// move the object towards its target
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, step);
		// check to see how close it is to the target
		float dist = Vector3.Distance(currentTarget.position, transform.position);
		//print(dist);
		// if it is too close, request a new destination
		if(dist < 0.5f) waypointManager.GetNewTarget(this);

	}

	public void Freeze () {
		this.enabled = false; // disable the script
	}

}
