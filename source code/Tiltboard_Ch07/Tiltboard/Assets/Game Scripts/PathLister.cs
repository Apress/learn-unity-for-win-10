using UnityEngine;
using System.Collections;
using System.Collections.Generic; // required for lists

// This script is used for path design only *************** 


public class PathLister : MonoBehaviour {
	public List<int> pathList; 
	public int lastIn = 0;

	// optional texture image for this path 
	public Texture thumbnail;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddToList (int newNum) {
		pathList.Add(newNum);
		lastIn++;
	}

	public void RemoveFromList () {
		lastIn--;    
		pathList.RemoveAt(lastIn);
	}

}
