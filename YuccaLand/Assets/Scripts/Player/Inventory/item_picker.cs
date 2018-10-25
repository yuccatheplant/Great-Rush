using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_picker : MonoBehaviour {

	public Item item; 

	inventory Inventory;

	player_controller player_controller;
	Settings settings;
	dialog_system dialog;

	bool triggered = false;
	Animator player_head;

	public string special_inspect_text = "";
	public string special_inspect_text_cze = "";
	public float special_inspect_time = 0f;

	public bool initialize = true;

	// Use this for initialization
	void Start () {
		player_controller = GameObject.Find ("player").GetComponent<player_controller>();
		settings = GameObject.Find("Settings").GetComponent<Settings>();

		Inventory = inventory.instance;

		player_head = GameObject.Find ("player_head").GetComponent<Animator> ();

		dialog = GameObject.Find ("subtitles_canvas").GetComponent<dialog_system>();


		if (initialize) {
			item_initialize ();
		}
	}

	void item_initialize() {
		//sprite renderrer
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if (renderer == null) {
			renderer = gameObject.AddComponent<SpriteRenderer> ();
		}
		renderer.sortingLayerName = "Pickable";
		renderer.sprite = item.icon;

		//box collider2D
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
		if (collider == null) {
			collider = gameObject.AddComponent<BoxCollider2D> ();
		}
		collider.isTrigger = true;
		collider.size = new Vector2 (0.5f, 0.5f);
	}

	void Update () {
		if (triggered) {
			if (player_controller.bool_action_pressed) {
				//StartCoroutine (primary_action ());
				StartCoroutine(item_inspect ());


			} else
				if (player_controller.bool_secaction_pressed) {
					//secondary_action_picker ();
					StartCoroutine(item_pickup ());
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




	IEnumerator item_inspect () {

		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player_controller.bool_roam_cutscene = true;

		if (special_inspect_text == "" || special_inspect_text_cze == "") { 

			StartCoroutine (dialog.say_something ( dialog.player_name , item.inspect_text_eng, item.inspect_text_cze, item.inspect_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head ));

			float wanted_time = item.inspect_time;
			float current_time = 0f;
			while (current_time < wanted_time) {
				current_time += Time.deltaTime;

				if (settings.cutscene_skip) {
					break;
				}

				yield return null;
			}
		} else {
			StartCoroutine (dialog.say_something ( dialog.player_name , special_inspect_text, special_inspect_text_cze, special_inspect_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head ));

			float wanted_time = special_inspect_time;
			float current_time = 0f;
			while (current_time < wanted_time) {
				current_time += Time.deltaTime;

				if (settings.cutscene_skip) {
					break;
				}

				yield return null;
			}
		}


		player_controller.bool_roam_cutscene = false;
		settings.already_interacting = false;
	}

	IEnumerator item_pickup () {



		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player_controller.bool_cutscene = true;
		player_controller.bool_player_moves = false;
		player_controller.float_movexaxis = 0f;
		player_controller.float_moveyaxis = 0f;


		yield return new WaitForSeconds(1f);

		//Add item to inventory

		if (Inventory.item_add (item)) {
			Destroy (gameObject);
		} else {
			StartCoroutine (say_inventory_full ());
		}
		player_controller.bool_cutscene = false;

		settings.already_interacting = false;
	}

	IEnumerator say_inventory_full () {

		player_controller.bool_roam_cutscene = true;
		float wanted_time = 2f;
		float current_time = 0f;

		StartCoroutine ( dialog.say_something( dialog.player_name, "I can carry nothing more.", "Neunesu už nic víc.", wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );


		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_controller.bool_roam_cutscene = false;
	}



}
