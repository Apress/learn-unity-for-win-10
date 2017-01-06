using UnityEngine;
using System.Collections;

public class BaseCells : MonoBehaviour {
	public PathCellManager pathCellManager; // the component on the Path Manager object
	public int elementNum; // the element number assigned to this cell object

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//FIXME this function is for path design only *************
	void OnMouseDown () {
		pathCellManager.AddPathCell(elementNum, transform); 
		//gameObject.SetActive(false);
	}

}
