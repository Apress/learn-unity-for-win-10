using UnityEngine;
using System.Collections;

public class HotSpotBooster : MonoBehaviour {
	public Booster booster; // the Booster script
	public AnimationClip[] aniClip; 
	internal Animator animator;


	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider theCollider) {
		// if the collider didn't belong to the Marble object, leave the function
		if(theCollider.gameObject.name != "Marble") return;
		if (animator) {
			int num = Random.Range(0,aniClip.Length);
			animator.Play(aniClip[num].name,0,0);
		}
		//trigger the boost
		booster.Boost(); // trigger the boost
	}

}
