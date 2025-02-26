// Description : Init Game to prevent bug when the game came back from a track to the main menu 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitPage : MonoBehaviour {
	[Header("Duration before launching the next scene.")] 
	public float WaitDuration = 2; 
	public bool ForcePlayerPrefsInitialization = false;

	// Use this for initialization
	void Awake () {
		if (ForcePlayerPrefsInitialization)
			PlayerPrefs.DeleteAll ();

		//->PlayerPrefs.SetString(SceneName[i] + "_Lock", "Lock"); 
		//PlayerPrefs.SetString("Track_02_RockIsland" + "_Lock", "Lock");
        //-> Repeat the line for each track

		StartCoroutine ("F_WaitBeforeLaunchingGame");
	}

	IEnumerator F_WaitBeforeLaunchingGame() {
		yield return new WaitForSeconds (WaitDuration);		// Wait before launching the new scene

		PlayerPrefs.SetInt ("WeAreOnTrack",0);				// Init the Main Menu
		SceneManager.LoadScene (1);							// Load the main Menu
	}
}
