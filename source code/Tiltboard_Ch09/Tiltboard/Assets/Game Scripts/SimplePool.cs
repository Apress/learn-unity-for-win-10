using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimplePool : MonoBehaviour {
	public Stack<GameObject> availableGameObjects; // the objects that are available for use
	public Transform[] allGameObjects; // the objects that could be in the pool
	public float yOffset = 0.5f; //offset to make the particle system high enough over the parent

	void Awake () {
		// set the size of the array
		allGameObjects = new Transform [transform.childCount];
		// find all the children containing the Transform component & put them into the array
		allGameObjects = gameObject.GetComponentsInChildren< Transform >();
		availableGameObjects = new Stack<GameObject>();
		for (int x = 1; x < allGameObjects.Length; x++) {
			availableGameObjects.Push(allGameObjects[x].gameObject); // add the objects to the stack
			allGameObjects[x].gameObject.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// remove & use pool object
	public void PopAndUse (Transform newParent, Vector3 location) {
		GameObject stackObj = availableGameObjects.Pop ();
		//transform it
		Vector3 newPos = new Vector3(stackObj.transform.position.x, stackObj.transform.position.y,stackObj.transform.position.z);
		newPos = location;
		newPos.y += yOffset; // move it up a bit
		stackObj.transform.position = newPos;
		// parent the object
		stackObj.transform.parent = newParent; 
		stackObj.SetActive (true);
		// make arrangements to return the object
		StartCoroutine(ReturnWhenFinished (stackObj));
	} // close the PopAndUse function

	IEnumerator ReturnWhenFinished (GameObject toBeReturned) {
		float targetDelay = 3f;
		yield return new WaitForSeconds(targetDelay);
		toBeReturned.transform.parent = null;
		// put it back in the pool
		ReturnAndPush(toBeReturned);
	}

	// put it back in the pool
	void ReturnAndPush (GameObject returnedObj) {
		returnedObj.SetActive (false);
		availableGameObjects.Push (returnedObj);
	}

}
