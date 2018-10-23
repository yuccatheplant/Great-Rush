using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate_trigger : MonoBehaviour {

	GameObject player;
	Settings settings;
	public GameObject gate;

	string said_text;
	bool triggered = false;

	Animator gate_animation;

	Animator player_head;

	dialog_system dialog;

	gamemanager_tutorial game_manager;



	void Start () {
		player = GameObject.Find ("player");
		settings = GameObject.Find("Settings").GetComponent<Settings>();

		player_head = GameObject.Find ("player_head").GetComponent<Animator> ();

		dialog = GameObject.Find ("subtitles_canvas").GetComponent<dialog_system>();

		game_manager = GameObject.Find ("gamemanager_tutorial").GetComponent<gamemanager_tutorial> ();

		gate_animation = GameObject.Find ("gate_01").GetComponent<Animator> ();
	}

	void Update () {
		if (triggered) {
			if (player.GetComponent<player_controller> ().bool_action_pressed ) {//&& game_manager.gate_t_unlocked
				StartCoroutine (primary_action ());

			} else
				if (player.GetComponent<player_controller> ().bool_secaction_pressed && game_manager.gate_t_unlocked) {
					StartCoroutine (secondary_action ());
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

	public void open_close_gate (bool open) {
		gate.SetActive (!open);
		game_manager.gate_t_openned = open;
		gate_animation.SetBool("Gate_Openned", open);
	}


	IEnumerator primary_action () {

		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player.GetComponent<player_controller> ().bool_roam_cutscene = true;

		said_text = "This is just an ordinary gate and I don't know why I am inspecting it.";
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

	IEnumerator secondary_action () {

		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player.GetComponent<player_controller> ().bool_cutscene = true;

		player.GetComponent<player_controller> ().float_movexaxis = 0f;
		player.GetComponent<player_controller> ().float_moveyaxis = 0f;
		player.GetComponent<player_controller> ().bool_player_moves = false;

		yield return new WaitForSeconds(0.5f);

		open_close_gate (!game_manager.gate_t_openned);

		player.GetComponent<player_controller> ().bool_cutscene = false;

		settings.already_interacting = false;

		if (!game_manager.gate_t_beenopened) {
			game_manager.gate_t_beenopened = true;

			StartCoroutine(GameObject.Find ("cutscene_object").GetComponent<cutscene01> ().cutscene2 ());

		}
	}

}
