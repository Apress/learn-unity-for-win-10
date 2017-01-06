using UnityEngine;
using System.Collections;

public class Persistent : MonoBehaviour {
	
	private static Persistent instanceRef;

	// make this data available to all levels
	internal int difficulty = 3; // the level of difficulty value 

	// keeping track of:
	internal float cash = 0f; // cash in pocket
	internal int wheelbarrows = 0; // usable banana movers
	internal int candyCrushers = 0; // usable peppermint crushers
	// rewards
	internal float winCash = 10f; // default win cash
	internal float timeBonus = 5f; // bonus for fast time
	private float timeThreshold = 100; // maximum seconds allowed for speed bonus
	// prices
	internal float wbPrice = 10f; // price per wheelbarrow/banana mover
	internal float crusherPrice = 75; // price per peppermint crushed
	internal float coinPrice = 0.99f; // the purchase price of tipCoins
	internal int coinAmount = 50; // how many coins you get

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

	public void CalculateWinCash (float winTime) {
		// a game has been won
		float bonusCash = 0;
		//print (winTime + " < ? " + timeThreshold);
		if (winTime < timeThreshold) bonusCash += timeBonus;
		cash = cash + winCash + bonusCash;
		//print ("cash in account = " + cash);
	}
}
