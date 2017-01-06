using UnityEngine;
using System.Collections;

public class GridManager : MonoBehaviour {
	public BaseCells [] allBaseCells;
	public PathCellManager pathCellManager;

	void Awake () {
		// set the size of the array using the number of children
		allBaseCells = new BaseCells [transform.childCount];
		// find all the children & put them into the array
		allBaseCells = gameObject.GetComponentsInChildren< BaseCells >();
		//inform the Cell Base objects of their element numbers
		for (int x = 0; x < allBaseCells.Length; x++) {
			allBaseCells[x].elementNum = x; 
			allBaseCells[x].pathCellManager = pathCellManager; 
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 GetBaseLocation () {
		int cLength = allBaseCells.Length;
		Transform tempLoc = allBaseCells[Random.Range(0,cLength)].transform;
		while (!tempLoc.gameObject.activeSelf) {
			tempLoc = allBaseCells[Random.Range(0,cLength)].transform;
		}
		return tempLoc.transform.position;
	}

}
