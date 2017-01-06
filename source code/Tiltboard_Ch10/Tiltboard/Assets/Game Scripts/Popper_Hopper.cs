using UnityEngine;
using System.Collections;

public class Popper_Hopper : MonoBehaviour {
	public GridManager gridManager; // to get the base tile locations
	internal float targetTime; // for the timer
	internal float onTime = 5f; // time the object is visible
	internal float offTime = 1f; // time it is not visible
	bool visible = true; // visibility flag
	Animator animator; // so the animation start times can be randomized

	Persistent persistent; // holds difficulty settings 

	// Use this for initialization
	void Start () {
		animator = gameObject.GetComponent<Animator>();
		targetTime = Time.time + Random.Range(0f,onTime);
		animator.Play("Squash",0,Random.Range(0f,1f));

		persistent = GameObject.FindWithTag("Holder").GetComponent<Persistent>();
		onTime = (6 - persistent.difficulty) + Random.Range(0,3); 

	}
	
	// Update is called once per frame
	void Update () {
		if (visible && Time.time > targetTime - offTime){
			visible = false;
			gameObject.GetComponent<Renderer>().enabled = false; //hide popper
			gameObject.GetComponent<Collider>().enabled = false; //disable collider
			targetTime = Time.time + Random.Range(.5f,offTime); //set a new time to make visible
		}

		if (Time.time > targetTime){
			MoveIt(gridManager.GetBaseLocation());    
			gameObject.GetComponent<Renderer>().enabled = true;//show popper
			gameObject.GetComponent<Collider>().enabled = true;//enable collider
			visible = true;
			targetTime = Time.time + Random.Range(0.5f,onTime) + offTime;// new time before hiding
		}

	}

	void MoveIt (Vector3 location) {
		//transform it
		Vector3 newPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		newPos = location;
		transform.position = newPos;
	}

	void ForceOff () {
		targetTime = Time.time; // this forces a timer restart
	}

	public void Freeze () {
		this.enabled = false; // disable the script
	}

}
