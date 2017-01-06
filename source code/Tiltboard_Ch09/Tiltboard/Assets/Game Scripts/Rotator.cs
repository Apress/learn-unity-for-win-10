using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.Rotate(0f,Random.Range(0f,360f),0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
