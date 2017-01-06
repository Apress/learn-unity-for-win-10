using UnityEngine;
using System.Collections;
using System.Collections.Generic; // need for lists

public class PathCellManager : MonoBehaviour {
	public PathCells[] allPathCells;
	public int nextInLine; // next available element number for the path objects pool
	public Material startMaterial;
	public GridManager gridManager;
	public PathLister pathLister;
	public PathLister[] paths; // array of prefabs with path lists
	int pathLength; // length of the chosen path
	internal int lastActivated = -1; // the last sequential tile visited

	internal GameObject[] allGamePieces; // store the game pieces for easy processing
	public MarbleManager marbleManager; // where input is turned off
	public BoardManager boardManager; // where the board is re-leveled

	void Awake () {
		// set the size of the array using the number of children
		allPathCells = new PathCells[transform.childCount];
		// find all the children containing the PathCells component & put them into the array
		allPathCells = gameObject.GetComponentsInChildren<PathCells>();
		//store the game piece objects
		allGamePieces = GameObject.FindGameObjectsWithTag("Game Piece");

	}


	// Use this for initialization
	void Start () {
		// deactivate the Cell Path objects
		int x = 0; // the counter
		foreach (PathCells cells in allPathCells) {
			allPathCells[x].pathElement = x++; // assign its element number to it, then increment x
			cells.gameObject.SetActive(false);
		}
		allPathCells[0].dormantCell.material = startMaterial;// assign the start tile material

		// load a premade path, comment this line out to design more paths
		LoadPath(); // comment out this line when designing paths

	}
	
	// Update is called once per frame
	void Update () {
//		// this is just for testing ***************
//		if(Input.GetKeyDown("up")) PathAdjuster(4);
//		if(Input.GetKeyDown("down")) PathAdjuster(-4);

	}

	public void AddPathCell(int elementNum, Transform location) {
		if (nextInLine >= allPathCells.Length) return;
		//activate the next path cell
		PathCells cell = allPathCells[nextInLine]; // assign it to a temp variable for easier handling
		cell.gameObject.SetActive(true);
		//transform it
		cell.transform.position = location.position;
		// store the base cell element's number in this path cell
		cell.baseElement = elementNum;
		// increment the nextInLine
		nextInLine++;
		// deactivate the Cell Base
		location.gameObject.SetActive(false);
		//FIXME add to the path list, design only **************
		pathLister.AddToList (elementNum);


	}

	// FIXME this function used only for path design *********
	public void RemovePathCell(int elementNum) {
		// deactivate the current path cell
		PathCells cell = allPathCells[nextInLine - 1];
		cell.gameObject.SetActive(false);

		// reactivate the base cell at this location using the element number argument
		gridManager.allBaseCells[elementNum].gameObject.SetActive(true);

		// decrement the nextInLine
		nextInLine--;
		//FIXME remove from path list, design only **************
		pathLister.RemoveFromList ();

	}

	void LoadPath () {
		// pick one of the paths
		int num = Random.Range(0, paths.Length);
		// iterate through the list and send each Cell Path object for processing
		foreach(int x in paths[num].pathList){
			AddPathCell(x,gridManager.allBaseCells[x].transform);
		}
		pathLength = paths[num].lastIn;
	}

	public void ToggleTileState (PathCells cell, int pathPosition) {
		//check for a valid position before processing
		if (pathPosition != (lastActivated + 1)) return;

		//print (pathPosition);
		// get the renderer component's current state
		if (cell.activatedCell.GetComponent<Renderer>().enabled) {
			cell.activatedCell.GetComponent<Renderer>().enabled = false;
			cell.dormantCell.GetComponent<Renderer>().enabled = true;
		}
		else {
			cell.activatedCell.GetComponent<Renderer>().enabled = true;
			cell.dormantCell.GetComponent<Renderer>().enabled = false;
		}
		lastActivated++; // increment the position

		// check for winner
		if(lastActivated >= pathLength - 1){
			ProcessWinner();
		}
	}

	void ProcessWinner () {
		print("Winner");
		// cue the FX

		//stop input
		marbleManager.repressInput = true;
		// level board
		boardManager.StartBoardReset (4);
		// stop game pieces
		foreach (GameObject gp in allGamePieces) {
			//gp.SetActive(false);
			gp.SendMessage("Freeze", SendMessageOptions.DontRequireReceiver);
		} 
		// hide marble
		marbleManager.gameObject.SetActive(false); 
		// show menu

	} // close the function



	public void PathAdjuster (int tiles) {
		if(lastActivated == -1) return;
		// randomize the base amount
		int newAdj = Random.Range(tiles - 1, tiles + 1);
		for (int x = 0; x < Mathf.Abs(newAdj); x++) {
			// forward, if it was a positive number
			if(newAdj >= 0) {
				if(lastActivated > pathLength-3) return;
				lastActivated++;
				// get the new tile to activate
				PathCells cell = allPathCells[lastActivated];
				cell.activatedCell.GetComponent<Renderer>().enabled = true;
				cell.dormantCell.GetComponent<Renderer>().enabled = false;
			}
			else { // backwards, if it was a negative number
				if(lastActivated < 1) return;
				// get the last activated tile
				PathCells cell = allPathCells[lastActivated];
				cell.activatedCell.GetComponent<Renderer>().enabled = false;
				cell.dormantCell.GetComponent<Renderer>().enabled = true;
				lastActivated--;
			}

		}
	}

	public Transform GetCurrentTile () {
		return allPathCells[lastActivated].transform;
	}

}


