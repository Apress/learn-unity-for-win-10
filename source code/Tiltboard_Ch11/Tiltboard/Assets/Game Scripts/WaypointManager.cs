using UnityEngine;
using System.Collections;

public class WaypointManager : MonoBehaviour {
	public Transform[] allWayPoints;
	int wayLength; // length of the array;
	float minDist = 10f; // closest distance to get a new target to

	void Awake () {
		wayLength = transform.childCount;
		// size the array
		allWayPoints = new Transform [wayLength];
		// fill the array
		allWayPoints = gameObject.GetComponentsInChildren<Transform>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetNewTarget(Traveler requestedBy)  {
		int num = Random.Range(1,wayLength); // get a random element number, exclude parent, 0
		Transform tempWayPoint = allWayPoints[num]; // get the object at that element number
		while (Vector3.Distance(requestedBy.transform.position,tempWayPoint.transform.position) < minDist) {
			num = Random.Range(1,wayLength);
			tempWayPoint = allWayPoints[num];
		}
		requestedBy.currentTarget = tempWayPoint.transform;
	}

}
