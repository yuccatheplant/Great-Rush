using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_loader : MonoBehaviour {

	Settings settings;

	void Start () {
		settings = GameObject.Find ("Settings").GetComponent<Settings> ();
	}

	void Update () {

		if (!SceneManager.GetSceneByName ("Menu").isLoaded) {
			settings.game_paused = false;
		} else {
			settings.game_paused = true;
		}

		if (Input.anyKeyDown) {
			if (Input.GetKey ( KeyCode.Escape )) {
				if (!SceneManager.GetSceneByName ("Menu").isLoaded) {
					//pause

					game_pause ();

				} else {

				}
			}

		}
	}

	void game_pause () {
		SceneManager.LoadScene ("Menu", LoadSceneMode.Additive);
		//SceneManager.SetActiveScene (SceneManager.GetSceneByName ("Menu"));

		Time.timeScale = 0;
	}
}
