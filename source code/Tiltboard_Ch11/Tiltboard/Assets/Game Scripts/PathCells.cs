using UnityEngine;
using System.Collections;

public class PathCells : MonoBehaviour {
	public Renderer activatedCell;
	public Renderer dormantCell;
	PathCellManager pathCellManager;
	public int pathElement; // the element number of this object in the path array
	public int baseElement; // the element number of the cell it will replace

	void Awake () {
		// make temp array of children
		Renderer[] child = gameObject.GetComponentsInChildren<Renderer>();
		//identify and assign the children
		activatedCell = child[0];
		dormantCell = child[1];
	}


	// Use this for initialization
	void Start () {
		pathCellManager = transform.parent.GetComponent<PathCellManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

//	//FIXME this function is for path design only *************
//	void OnMouseDown () {
//		pathCellManager.RemovePathCell(baseElement); 
//	}

	void OnTriggerEnter (Collider collider) {
		if (collider.tag != "Player") return;
		pathCellManager.ToggleTileState(this,pathElement);
	}

}
