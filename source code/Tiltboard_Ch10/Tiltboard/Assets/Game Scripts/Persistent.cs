using UnityEngine;
using System.Collections;

public class Persistent : MonoBehaviour {
	
	private static Persistent instanceRef;

	// make this data available to all levels
	internal int difficulty = 3; // the level of difficulty value 


	void Awake () {
		if(instanceRef == null) {
			instanceRef = this;                 
			DontDestroyOnLoad(gameObject);             
		}
		else {                 
			DestroyImmediate(gameObject);             
		} 
	} 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
