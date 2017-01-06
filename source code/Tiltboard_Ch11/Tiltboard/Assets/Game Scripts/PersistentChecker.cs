using UnityEngine;
using System.Collections;

public class PersistentChecker : MonoBehaviour {
	
	public GameObject holder;

	void Awake () {     
		if (!GameObject.FindWithTag("Holder")) {         
			holder = Instantiate(holder);         
			holder.name = "Holder";     
		} 
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
