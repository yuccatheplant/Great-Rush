using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_loader : MonoBehaviour {

	public delegate void after_menu();
	public after_menu on_after_menu_call_back;

	Settings settings;

	public static menu_loader instance;



	void Awake(){
		if (menu_loader.instance != null) {
			Destroy (this);
		}
		instance = this;
	}

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
			if (Input.GetKey (KeyCode.Escape)) {
				if (!SceneManager.GetSceneByName ("Menu").isLoaded) {
					//pause

					game_pause ();
			
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
