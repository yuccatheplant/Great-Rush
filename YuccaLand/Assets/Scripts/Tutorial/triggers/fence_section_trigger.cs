using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fence_section_trigger : MonoBehaviour {

	player_controller playercontroller;
	Settings settings;
	inventory_ui Inventory_ui;
	inventory Inventory;

	bool triggered = false;

	Animator fence_animation;

	bool first_interaction = true;

	int wanted_id = 1001;

	Animator player_head;

	dialog_system dialog;

	gamemanager_tutorial game_manager;

	void Start () {
		playercontroller = GameObject.Find ("player").GetComponent<player_controller> ();
		settings = GameObject.Find ("Settings").GetComponent<Settings> ();
		fence_animation = gameObject.GetComponent<Animator> ();
		Inventory_ui = GameObject.Find ("inventory_canvas").GetComponent<inventory_ui> ();
		Inventory = inventory.instance;

		player_head = GameObject.Find ("player_head").GetComponent<Animator> ();

		dialog = GameObject.Find ("subtitles_canvas").GetComponent<dialog_system>();

		game_manager = GameObject.Find ("gamemanager_tutorial").GetComponent<gamemanager_tutorial> ();

	}

	void Update () {
		if (triggered) {
			if (playercontroller.bool_action_pressed  ) {//&& game_manager.fence_t_unlocked
				if (!game_manager.fence_t_repaired) {
					StartCoroutine (inspect ());
				} else {
					StartCoroutine (inspect_done ());
				}

			} else
				if (playercontroller.bool_secaction_pressed && game_manager.fence_t_unlocked) {
					if (!game_manager.fence_t_repaired) {
						StartCoroutine (interact ());
					} else {
						StartCoroutine (interact_done ());
					}
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

	IEnumerator inspect () {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		playercontroller.bool_roam_cutscene = true;

		string said_text = "This fence is broken.";
		float wanted_time = 2f;

		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );


		float current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}


		playercontroller.bool_roam_cutscene = false;


		settings.already_interacting = false;
	}

	IEnumerator inspect_done () {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		playercontroller.bool_roam_cutscene = true;

		string said_text = "This fence is freshly repaired.";
		float wanted_time = 2.2f;

		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );

		float current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}


		playercontroller.bool_roam_cutscene = false;


		settings.already_interacting = false;
	}

	IEnumerator interact () {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		playercontroller.bool_cutscene = true;

		playercontroller.bool_player_moves = false;
		playercontroller.float_movexaxis = 0f;
		playercontroller.float_moveyaxis = 0f;

		string say_this;
		float want_time;
		float current_time;

		if (first_interaction) {
			first_interaction = false;

			say_this = "I need something to repair this fence.";
			want_time = 2f;

			StartCoroutine ( dialog.say_something( dialog.player_name, say_this, want_time, dialog.player_neutral, player_head )  );

			want_time += 0.1f;
			current_time = 0f;

			while (current_time < want_time) {
				current_time += Time.deltaTime;

				if (settings.GetComponent<Settings>().cutscene_skip) {
					break;
				}

				yield return null;
			}
			yield return null;
		}

		Inventory_ui.interactive_open = true;
		if (!settings.inventory_opened) {
			Inventory_ui.open_close_inventory();
		}

		yield return new WaitForSeconds (0.01f);

		int got_id = -1;
		if (Inventory_ui.interactive_open) {
			switch (Inventory_ui.selected_slot) {
			case 10:
				if (Inventory.melee_weapon != null) {
					got_id = Inventory.melee_weapon.ID;
				}
				break;
			case 11:
				if (Inventory.ranged_weapon != null) {
					got_id = Inventory.ranged_weapon.ID;
				}
				break;
			default:
				if (Inventory.items.Count > Inventory_ui.selected_slot) {
					got_id = Inventory.items [Inventory_ui.selected_slot].ID;
				}
				break;
			}
		}
		if (got_id == wanted_id) {

			Inventory.item_remove (Inventory.items [Inventory_ui.selected_slot]);

			yield return new WaitForSeconds (2f);

			fence_animation.SetBool ("repaired",true);

			say_this = "Fence is now repaired!";
			want_time = 2f;

			StartCoroutine ( dialog.say_something( dialog.player_name, say_this, want_time, dialog.player_neutral, player_head )  );

			current_time = 0f;

			while (current_time < want_time) {
				current_time += Time.deltaTime;

				if (settings.GetComponent<Settings>().cutscene_skip) {
					break;
				}

				yield return null;
			}
			yield return null;


			game_manager.fence_t_repaired = true;
			game_manager.instructor_trigger_secaction_mode = 6;
			GameObject.Find ("cutscene_object").GetComponent<cutscene01> ().cutscene5_prepare ();
		} else if ( got_id >= 1 && got_id <= 500 ) {
			yield return new WaitForSeconds (0.5f);

			say_this = "Why would I want to destroy that fence? I am supposed to repair it!";
			want_time = 4f;

			StartCoroutine ( dialog.say_something( dialog.player_name, say_this, want_time, dialog.player_neutral, player_head )  );

			current_time = 0f;

			while (current_time < want_time) {
				current_time += Time.deltaTime;

				if (settings.GetComponent<Settings>().cutscene_skip) {
					break;
				}

				yield return null;
			}
			yield return null;

		} else {
			yield return new WaitForSeconds (0.5f);

			say_this = "This is not working...";
			want_time = 2f;

			StartCoroutine ( dialog.say_something( dialog.player_name, say_this, want_time, dialog.player_neutral, player_head )  );

			current_time = 0f;

			while (current_time < want_time) {
				current_time += Time.deltaTime;

				if (settings.GetComponent<Settings>().cutscene_skip) {
					break;
				}

				yield return null;
			}
			yield return null;
		}

		yield return null;


		playercontroller.bool_cutscene = false;

		settings.already_interacting = false;

	}


	IEnumerator interact_done () {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		playercontroller.bool_cutscene = true;
		playercontroller.bool_player_moves = false;
		playercontroller.float_movexaxis = 0f;
		playercontroller.float_moveyaxis = 0f;

		string said_text = "No more work is needed here.";
		float wanted_time = 2.5f;

		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );

		float current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}


		playercontroller.bool_cutscene = false;


		settings.already_interacting = false;
	}

}
