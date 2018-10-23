using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log_trigger : MonoBehaviour {

	GameObject player;
	Settings settings;

	string said_text;
	bool triggered = false;


	Animator player_head;

	dialog_system dialog;


	bool log_interacting = false;

	void Start () {
		player = GameObject.Find ("player");
		settings = GameObject.Find("Settings").GetComponent<Settings>();

		player_head = GameObject.Find ("player_head").GetComponent<Animator> ();

		dialog = GameObject.Find ("subtitles_canvas").GetComponent<dialog_system>();


	}

	void Update () {
		if (triggered) {
			if (player.GetComponent<player_controller> ().bool_action_pressed && !log_interacting ) {
				StartCoroutine (primary_action ());

			} 				
		}
	}

	void OnTriggerEnter2D ( Collider2D other) {
		if (other.tag == "action_receiver") {
			triggered = true;
		}
	}

	void OnTriggerExit2D ( Collider2D other ) {
		if (other.tag == "action_receiver") {
			triggered = false;
		}
	}
		

	IEnumerator primary_action () {

		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player.GetComponent<player_controller> ().bool_roam_cutscene = true;

		said_text = "This is wooden log that blocks the path.";
		float wanted_time = 4f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );

		wanted_time = 4.5f;
		float current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		player.GetComponent<player_controller> ().bool_roam_cutscene = false;


		settings.already_interacting = false;
	}


}
