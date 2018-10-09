using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pine01_trigger : MonoBehaviour {

	player_controller playercontroller;
	Settings settings;
	inventory_ui Inventory_ui;
	inventory Inventory;

	bool triggered = false;

	//Animator fence_animation;

	int wanted_id = 1;

	gamemanager_tutorial game_manager;

	Animator player_head;

	dialog_system dialog;

	public Item item;

	void Start () {
		playercontroller = GameObject.Find ("player").GetComponent<player_controller> ();
		settings = GameObject.Find ("Settings").GetComponent<Settings> ();
		//fence_animation = gameObject.GetComponent<Animator> ();
		Inventory_ui = GameObject.Find ("inventory_canvas").GetComponent<inventory_ui> ();
		Inventory = inventory.instance;

		player_head = GameObject.Find ("player_head").GetComponent<Animator> ();

		dialog = GameObject.Find ("subtitles_canvas").GetComponent<dialog_system>();

		game_manager = GameObject.Find ("gamemanager_tutorial").GetComponent<gamemanager_tutorial> ();

	}

	void Update () {
		if (triggered) {
			if (playercontroller.bool_action_pressed  ) {//&& game_manager.pine01_t_unlocked
				if (!game_manager.pine01_t_interactiondone) {
					StartCoroutine (inspect ());
				} else {
					StartCoroutine (inspect_done ());
				}

			} else
				if (playercontroller.bool_secaction_pressed && game_manager.pine01_t_unlocked) {
					if (!game_manager.pine01_t_interactiondone) {
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

		string said_text = "Just a pine tree.";
		float wanted_time = 1f;

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

		string said_text = "Still just a pine tree.";
		float wanted_time = 1.5f;

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



			say_this = "I need a branch from this tree.";
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
					
					got_id = wanted_id;
				}
				break;
			case 11:
				got_id = -1;
				break;
			default:
				if (Inventory.items.Count > Inventory_ui.selected_slot) {
					got_id = Inventory.items [Inventory_ui.selected_slot].ID;
				}
				break;
			}
		}
		if (got_id == wanted_id) {

			yield return new WaitForSeconds (2f);

			if (Inventory.item_add (item)) {
				say_this = "I sucessfuly cut a branch.";
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
			} else {
				
				GameObject dropped_object = new GameObject (item.name);
				GameObject player = playercontroller.gameObject; //GameObject.Find ("player");
				dropped_object.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y - 0.3f, player.transform.position.z);
				dropped_object.transform.SetParent (GameObject.Find ("Pickables").transform);

				SpriteRenderer dropped_object_renderer = dropped_object.AddComponent<SpriteRenderer> ();
				dropped_object_renderer.sprite = item.icon;
				dropped_object_renderer.sortingLayerName = "Pickable";

				BoxCollider2D dropped_object_collider = dropped_object.AddComponent<BoxCollider2D> ();
				dropped_object_collider.isTrigger = true;
				dropped_object_collider.size = new Vector2 (0.5f, 0.5f);
				dropped_object_collider.offset = new Vector2 (0f, 0f);

				item_picker dropped_object_trigger = dropped_object.AddComponent<item_picker> ();
				dropped_object_trigger.item = item;

				say_this = "I sucessfuly cut a branch, but I can't carry it now.";
				want_time = 3f;

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



			game_manager.pine01_t_interactiondone = true;
			GameObject.Find ("cutscene_object").GetComponent<cutscene01> ().cutscene3_prepare ();

		} else {
			yield return new WaitForSeconds (0.5f);

			say_this = "I need something else to cut that branch.";
			want_time = 2.5f;

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

		string said_text = "I don't think I should torture this tree more than is necessary";
		float wanted_time = 3f;

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
