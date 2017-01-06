using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Neutralizer : MonoBehaviour {

	private Persistent persistent;
	private Text crusherCount;
	private Button crusherButton;
	private AudioSource theAudio;
	public AudioClip crushFX;

	// Use this for initialization
	void Start () {
		persistent = GameObject.Find("Holder").GetComponent<Persistent>();
		crusherCount = GameObject.Find("Count C").GetComponent<Text>();
		crusherButton = GameObject.Find("Crushers").GetComponent< Button>();
		theAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		NukeIt();
	}

	public void NukeIt () {
		if(persistent.candyCrushers == 0 || crusherButton.
			interactable == false) return; // no more perks or not activated
		theAudio.PlayOneShot(crushFX);
		//disable the collider and renderer
		GetComponent<Collider>().enabled = false;
		GetComponent<Renderer>().enabled = false;
		persistent.candyCrushers --; // decrement the perks
		crusherCount.text = persistent.candyCrushers.ToString();
		// update the UI
	}
}
