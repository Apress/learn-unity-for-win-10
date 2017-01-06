using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crusher : MonoBehaviour {

	public Transform[] targets; // the peppermints
	internal int maxCrushers = 3; // the max number of crushers per game
	int currentTarget = 0; // index number of current target
	Transform target; // location of the current target

	internal bool hunting = false;
	Animator animator;
	public Transform theBoard;  // board centre

	Persistent persistent;
	Button crusherButton;

	public GameHUD gameHUD;

	//To toggle on/off
	public Renderer guardHouse;
	public Renderer guard;
	public Renderer club;
	public Renderer Shield;
	public CharacterController guardCollider;
	public Collider gHWallLeft;
	public Collider gHWallRight;

	// Use this for initialization
	void Start () {
		target = targets[0]; // set the first target
		animator = GetComponent<Animator>();
		persistent = GameObject.FindWithTag("Holder").GetComponent<Persistent>();
		crusherButton = GameObject.Find("Crushers").GetComponent<Button>();
		if(persistent.candyCrushers > 0) ToggleGuardStuff(true);
		else ToggleGuardStuff(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (hunting) {
			transform.LookAt(target);
		}
	}

	void OnTriggerEnter (Collider collider) {
		if(!hunting) return;
		if (collider.transform == target && currentTarget == 3) {
			hunting = false;
			animator.CrossFade("Guard Narrow Idle",.5f);
			transform.LookAt(theBoard);
			return;
		}
		if (collider.transform == target){
			animator.CrossFade("Guard Quick Block",0.1f);
			collider.GetComponent<Neutralizer>().NukeIt();
			GetNextTarget();
		}
	}

	void GetNextTarget() {
		currentTarget ++;
		if (currentTarget < maxCrushers) {
			target = targets[currentTarget];
		}
		else {
			currentTarget = 3;
			target = targets[currentTarget];
			animator.CrossFade("Guard Quick Block 0",0.1f);
			crusherButton.interactable = false;
		}
	}

	public void Activate() {
		if (hunting) return;
		gameHUD.ResetFocus();
		// Get max crushers available
		maxCrushers = persistent.candyCrushers;
		if (maxCrushers > 3) maxCrushers = 3;
		if (maxCrushers == 0) return;
		hunting = true;
		animator.CrossFade("Guard Run",.2f);
	}

	void ToggleGuardStuff(bool state) {
		crusherButton.interactable = state;
		guardHouse.enabled = state;
		guard.enabled = state;
		club.enabled = state;
		Shield.enabled = state;
		guardCollider.enabled = state;
		gHWallLeft.enabled = state;
		gHWallRight.enabled = state;
	}
}
