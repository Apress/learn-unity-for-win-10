using UnityEngine;
using System.Collections;

public class PortalHopper : MonoBehaviour {
	public MarbleManager marbleManager; // the MarbleManager script
	public Transform[] destination; // the possible target location
	internal float delay = 0.0f; // the time it will take to move the marble
	Collider marbleCollider; // the marble's collider
	public PathCellManager pathCellManager;

	public enum HealthType {
		Good = 0, // adds health points and/or path tiles
		Bad = 1 // removes health points and/or path tiles
	}
	// The enum to use
	public HealthType healthType = HealthType.Good;

	public int healthPoints = 2; // amount of points to take or give
	internal int adjustment = 4; // average path adjustment


	// Use this for initialization
	void Start () {
		marbleCollider = marbleManager.gameObject.GetComponent<Collider>();

		//  set up health points & path adjustment 
		if (healthType == HealthType.Good) { //good game piece
			if (healthPoints > 4) adjustment = 5;
			else if (healthPoints < 2) adjustment = 3;
		}
		else { // else its a bad game piece, adjust path adjustment accordingly
			if (healthPoints > 4) adjustment = -3;
			else if (healthPoints < 2) adjustment = -5;
			else adjustment = -4;
			healthPoints = healthPoints * -1; // bad health points are negative 
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider theCollider) {
		// if the collider didn't belong to the Marble object, leave the function
		if(theCollider != marbleCollider) return;

		int health = marbleManager.health;
		int currentTile = pathCellManager.lastActivated; // current tile
		if (healthPoints > 0) { // health points are positive
			// marble is stong enough to gain path tiles
			if (health >= 12 && currentTile > 0){ // marble is stong enough to gain path tiles
				// increment path
				pathCellManager.PathAdjuster(adjustment);
				// jump marble to the new current tile;
				marbleManager.PortalJump (delay,pathCellManager.GetCurrentTile());
			}
			else { // not strong enough for path boost, just increment health
				// increment health to no greater than 20
				health += healthPoints; //increment the health value
				if (health > 20) health = 20; 
			}
		}
		else { // health points are negative
			// marble is very weak, takes a hit
			if (health < 6 && currentTile > 0){
				// jump to one of the spawn places
				marbleManager.PortalJump (delay,destination[Random.Range (0,destination.Length)]);
				// decrement the path
				pathCellManager.PathAdjuster(adjustment);
			}
			else { // strong enough to resist path hit
				// decrement health to no less than 0
				health += healthPoints; //decrement the health value
				if (health < 0) health = 0;
			}

		}
		marbleManager.health = health; // assign the adjusted value to the marble's health
		print (marbleManager.health);

		// force time is up if this is a popper
		gameObject.SendMessage("ForceOff",SendMessageOptions.DontRequireReceiver);

//		int num = Random.Range (0,destination.Length);
//		Transform tempDestination = destination[num];
//
//		//print (theCollider.gameObject.name);
//		marbleManager.PortalJump (delay,tempDestination);

	}


}
