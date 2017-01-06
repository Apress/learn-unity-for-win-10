using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;

public class GameHUD : MonoBehaviour {
	
	// timer variables
	float elapsedTime; // need for starting and stoping timer
	float startTime; // 
	bool ticking = false; // flag for timer active
	string formattedTime; // float converted to nice string for display
	public Text currentETime; // the elapsed time GUI

	//health variables
	internal int health = 10; // tracking health

	public RectTransform healthBar; // the Health Bar image's Rect Transform  
	public Text healthText; // The health value label
	public Image imageColor;
	public Gradient gradient; // the Health Bar image's color source

	// flag for pausing game
	bool isPaused; 
	public TiltBoard tiltBoard; 

	//GUI
	public Text cashValue;
	public Text barrowCount;
	public Text crusherCount;
	Persistent persistent;

	public Collider boardGroupCollider;
	public Collider liveZoneCollider;
	Collider boardSurfaceCollider;
	PlayerMovable playerMovable;

	private Toggle barrowToggle;

	public EventSystem theEventSystem; //
	public GameObject newFocus; // in case a UI element is disabled


	// Use this for initialization
	void Start () {
		ToggleTimer (true); // call the function & start the timer
		formattedTime = string.Format("{0:0}:{1:00}", 0, 0);
		UpdateHealthGUI(health); //initialize the health GUI
		persistent = GameObject.FindWithTag("Holder").GetComponent<Persistent>();
		UpdateTipCash();
		boardSurfaceCollider = GameObject.Find("Board Surface").GetComponent<Collider>();
		boardSurfaceCollider.enabled = false;
		playerMovable = GetComponent<PlayerMovable>();
		UpdateBarrow ();
		barrowToggle = GameObject.Find("Wheelbarrows").GetComponent<Toggle>();
		barrowToggle.interactable = false; // disable the toggle
		UpdateCrusher ();
	}
	
	// Update is called once per frame
	void Update () {
		if (ticking) {
			float timer = Time.timeSinceLevelLoad  - startTime;
			int minutes = Mathf.FloorToInt(timer / 60F);
			int seconds = Mathf.FloorToInt(timer - minutes * 60);
			formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
			currentETime.text = formattedTime; // update the GUI text
		}

	}

	public void ToggleTimer (bool state) {
		ticking = state;
		if (state) startTime = Time.timeSinceLevelLoad - elapsedTime;
		else elapsedTime = Time.timeSinceLevelLoad - startTime;
	}

	public void UpdateHealthGUI(int newHealth) {
		// health bar value   
		health = newHealth;
		Vector3 tempScale = new Vector3(healthBar.localScale.x,healthBar.localScale.y,healthBar.localScale.z);
		tempScale.y = health/20f;
		healthBar.localScale = tempScale;
		healthText.text = string.Format("{0}%", health);
		// Health bar color
		imageColor.color = gradient.Evaluate(tempScale.y);

	}

	public void PauseToggle () {
		if (isPaused) {
			boardGroupCollider.enabled = true;
			liveZoneCollider.enabled = true;
			//print("Unpaused");
			tiltBoard.allowTilt = true;
			Time.timeScale = 1.0f;
			isPaused = false;
			boardSurfaceCollider.enabled = false;
			playerMovable.isPaused = false;
			barrowToggle.interactable = false; // disable the toggle
		}
		else {
			//print("Paused");
			tiltBoard.allowTilt = false;
			Time.timeScale = 0.0f;
			isPaused = true;
			boardGroupCollider.enabled = false;
			liveZoneCollider.enabled = false;
			boardSurfaceCollider.enabled = true;
			playerMovable.isPaused = true;
			if (persistent && persistent.wheelbarrows != 0) barrowToggle.interactable = true; // enable the toggle if count not 0 and	persistent is in scene
		}
	}

	public void OnApplicationQuit() { 
		Time.timeScale=1; 
	}

	public void StartMenu () {
		// restart
		SceneManager.LoadScene ("Start Menu");
	}

	public void PlayAgain () {
		// restart
		SceneManager.LoadScene ("Board Level");
	}

	public float GetGameTime () {
		float endTime = Time.timeSinceLevelLoad - startTime;
		return endTime;
	}

	public void UpdateTipCash () {
		cashValue.text = persistent.cash.ToString();
	}

	public void UpdateBarrow () {
		barrowCount.text = persistent.wheelbarrows.ToString();
	}

	public void ResetFocus () {
		theEventSystem.SetSelectedGameObject(newFocus);
	}

	public void UpdateCrusher () {
		crusherCount.text = persistent.candyCrushers.ToString();
	}
}
