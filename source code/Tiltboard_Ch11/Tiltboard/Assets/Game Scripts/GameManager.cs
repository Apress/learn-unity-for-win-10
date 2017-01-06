using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
	public Slider slider; // access for the slider
	Persistent persistent; // object that carries the difficulty values throughout levels

	public Canvas creditsCanvas;
	public Button closeCredits;

	// Use this for initialization
	void Start () {
		persistent = GameObject.FindWithTag("Holder").GetComponent<Persistent>();
		slider.value = persistent.difficulty;
		CloseCredits ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadGameLevel () {
		SceneManager.LoadScene ("Board Level");
	}

	public void UpdateSliderValue () {
		// get the slider value and update difficulty in the Persistant script
		if(persistent) {
			persistent.difficulty = (int)slider.value;
		}
	}

	public void OpenCredits () {
		creditsCanvas.enabled = true;
	}
	public void CloseCredits () {
		creditsCanvas.enabled = false;
	}

	public void EndGame (){
		Application.Quit(); // end the application
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	public void LoadStoreLevel () {
		SceneManager.LoadScene("Store Level");
	}
}
