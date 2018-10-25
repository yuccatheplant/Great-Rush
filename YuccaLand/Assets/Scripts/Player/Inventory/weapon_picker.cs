using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weapon_picker : MonoBehaviour {

	public Weapon weapon; 

	inventory Inventory;

	player_controller player_controller;
	Settings settings;
	dialog_system dialog;

	bool triggered = false;


	public string special_inspect_text = "";
	public string special_inspect_text_cze = "";
	public float special_inspect_time = 0f;

	Animator player_head;

	public bool initialize = true;

	void Start () {
		player_controller = GameObject.Find ("player").GetComponent<player_controller>();
		settings = GameObject.Find("Settings").GetComponent<Settings>();

		dialog = GameObject.Find ("subtitles_canvas").GetComponent<dialog_system>();

		player_head = GameObject.Find ("player_head").GetComponent<Animator> ();

		Inventory = inventory.instance;


		if (initialize) {
			weapon_initialize ();
		}
	}

	void Update () {
		if (triggered) {
			if (player_controller.bool_action_pressed) {
				//StartCoroutine (primary_action ());
				StartCoroutine(weapon_inspect ());


			} else
				if (player_controller.bool_secaction_pressed) {
					//secondary_action_picker ();
					StartCoroutine(weapon_pickup ());
				} 
		}
	}


	void weapon_initialize() {
		//sprite renderrer
		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer> ();
		if (renderer == null) {
			renderer = gameObject.AddComponent<SpriteRenderer> ();
		}
		renderer.sortingLayerName = "Pickable";
		renderer.sprite = weapon.icon;

		//box collider2D
		BoxCollider2D collider = gameObject.GetComponent<BoxCollider2D>();
		if (collider == null) {
			collider = gameObject.AddComponent<BoxCollider2D> ();
		}
		collider.isTrigger = true;
		collider.size = new Vector2 (0.5f, 0.5f);
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




	IEnumerator weapon_inspect () {

		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player_controller.bool_roam_cutscene = true;

		if (special_inspect_text == "" || special_inspect_text_cze == "") { 
			StartCoroutine (dialog.say_something ( dialog.player_name, weapon.inspect_text_eng, weapon.inspect_text_cze, weapon.inspect_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head ));

			float wanted_time = weapon.inspect_time;
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

	IEnumerator weapon_pickup () {

		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player_controller.bool_cutscene = true;
		player_controller.bool_player_moves = false;
		player_controller.float_movexaxis = 0f;
		player_controller.float_moveyaxis = 0f;


		yield return new WaitForSeconds(1f);

		Inventory.weapon_add (weapon);
		Destroy (gameObject);

		player_controller.bool_cutscene = false;

		settings.already_interacting = false;
	}




}