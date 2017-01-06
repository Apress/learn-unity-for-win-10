using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour {

	Persistent persistent;
	//GUI
	public Text CurrentCoins;
	public Text CurrentWheelbarrows;
	public Text CurrentCandyCrushers;
	// purchase prices
	private float wbPrice;
	private float crusherPrice;
	private float coinPrice;
	private float coinAmount;
	// price labels
	public Text wbPriceLabel;
	public Text crusherPriceLabel;
	public Text coinPriceLabel;
	public Text coinAmountLabel;

	//transaction messages
	public Image insufficientFunds;
	public Image transactionComplete;
	AudioSource theAudio;
	public AudioClip buzzer;
	public AudioClip dinger;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		persistent = GameObject.Find("Holder").GetComponent<Persistent>();

		//update prices
		wbPrice = persistent.wbPrice;
		crusherPrice = persistent.crusherPrice;
		coinPrice = persistent.coinPrice;
		coinAmount = persistent.coinAmount;

		//load into price labels
		wbPriceLabel.text = wbPrice.ToString() + " each";
		crusherPriceLabel.text = crusherPrice.ToString() + " each";
		coinPriceLabel.text = coinAmount.ToString() + " for $" + coinPrice.ToString();

		// get current values and update them in the UI
		UpdateTipCash ();
		UpdateBarrow ();
		UpdateCrusher ();

		theAudio= GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadStartMenu () {
		// go to the main menu
		SceneManager.LoadScene("Start Menu");
	}

	public void LoadBoardLevel () {
		// play the game
		SceneManager.LoadScene("Board Level");
	}

	public void UpdateTipCash () {
		CurrentCoins.text = persistent.cash.ToString();
	}
	public void UpdateBarrow () {
		CurrentWheelbarrows.text = persistent.wheelbarrows.ToString();
	}
	public void UpdateCrusher () {
		CurrentCandyCrushers.text = persistent.candyCrushers.ToString();
	}

	public void BuyCoins () {
		// do the real purchase here...
		//add the coins
		persistent.cash += coinAmount;
		// update the UI
		UpdateTipCash ();
		TransactionComplete();
	}

	public void BuyWheelbarrow () {
		// check for sufficient funds
		if(persistent.cash < wbPrice) {
			InsufficientFunds();
			return;
		}
		//add the coins
		persistent.wheelbarrows += 1;
		//take the money
		persistent.cash -= wbPrice;
		// update the UI
		UpdateBarrow ();
		UpdateTipCash ();
		TransactionComplete();
	}

	public void BuyCrusher () {
		// check for sufficient funds
		if(persistent.cash < crusherPrice) {
			InsufficientFunds();
			return;
		}
		//add the coins
		persistent.candyCrushers += 1;
		//take the money
		persistent.cash -= crusherPrice;
		// update the UI
		UpdateCrusher ();
		UpdateTipCash ();
		TransactionComplete();
	}

	IEnumerator TurnOffMessage (Image currentSibling,float pause) {
		yield return new WaitForSeconds(pause);
		currentSibling.transform.SetAsFirstSibling();
	}

	void InsufficientFunds() {
		insufficientFunds.transform.SetAsLastSibling();
		theAudio.PlayOneShot(buzzer);
		StartCoroutine(TurnOffMessage(insufficientFunds, 0.5f));
	}
	void TransactionComplete () {
		transactionComplete.transform.SetAsLastSibling();
		theAudio.PlayOneShot(dinger);
		StartCoroutine(TurnOffMessage(transactionComplete, 0.2f));
	}
}
