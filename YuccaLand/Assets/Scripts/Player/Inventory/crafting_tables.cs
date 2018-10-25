using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crafting_tables : MonoBehaviour {
	player_controller player;
	Transform pickables;
	inventory Inventory;
	dialog_system dialog;
	Settings settings;
	Animator player_head;
	void Start () {
		player = GameObject.Find ("player").GetComponent<player_controller>();
		pickables = GameObject.Find ("Pickables").transform;
		Inventory = inventory.instance;
		dialog = GameObject.Find("subtitles_canvas").GetComponent<dialog_system>();
		settings = GameObject.Find ("Settings").GetComponent<Settings> ();
		player_head = GameObject.Find ("player_head").GetComponent<Animator> ();
	}

	IEnumerator crafting_response (string said_text, float wanted_time, float freeze_time) {
		while (settings.already_interacting) {
			yield return null;
		}
		settings.already_interacting = true;
		player.bool_roam_cutscene = true;

		player.bool_cutscene = true;
		float current_time = 0f;
		while (current_time < freeze_time) {
			current_time += Time.deltaTime;

			yield return null;
		}
		player.bool_cutscene = false;


		string said_text_cze = "";

		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, said_text_cze, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head));

		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		player.bool_roam_cutscene = false;
		settings.already_interacting = false;


	}


//Function that compares first ID
	public void find_recipe1 (int slot1, int slot2, int id1, int id2) {
		bool result = false;


		switch (id1) {
		case 1: 
			result = find_recipe2_id_1 (slot1, slot2, id2);
			break;
		}
			

		if (!result) {
			string said_text = "It is not working...";
			float wanted_time = 2f;
			StartCoroutine (crafting_response(said_text, wanted_time, 0f));
		}
	}

//Functions that finds crafting function after combining is done
	bool find_recipe2_id_1 (int slot1, int slot2, int id2) {
		bool result = false;

		switch (id2) {
		case 1002:
			result = craft_id1_id1001 (slot2);
			break;
		}

		slot1 = slot2;
		slot2 = slot1;

		return result;
	}
		

//Functions that contains exact crafting
	bool craft_id1_id1001(int slot2) {
		Inventory.item_remove (Inventory.items [slot2]);

		if (Inventory.items.Count < Inventory.inventory_space) {
			Inventory.item_add ( Resources.Load<Item>("Inventory/Items/Timber") );
		} else {
			drop_item ( Resources.Load<Item>("Inventory/Items/Timber") );
		}


		string said_text = "I've managed to create Timber stick.";
		float wanted_time = 3f;
		StartCoroutine ( crafting_response(said_text, wanted_time, 2f) );

		return true;
	}





	void drop_item (Item item) {
		
			GameObject dropped_object = new GameObject (item.name);
			dropped_object.transform.SetParent (pickables);
			dropped_object.transform.position = new Vector3 ( player.transform.position.x, player.transform.position.y-0.3f , player.transform.position.z );

			SpriteRenderer dropped_object_renderer = dropped_object.AddComponent<SpriteRenderer> ();
			dropped_object_renderer.sprite = item.icon;
			dropped_object_renderer.sortingLayerName = "Pickable";

			BoxCollider2D dropped_object_collider = dropped_object.AddComponent<BoxCollider2D> ();
			dropped_object_collider.isTrigger = true;
			dropped_object_collider.size = new Vector2 (0.5f, 0.5f);
			dropped_object_collider.offset = new Vector2 (0f, 0f);

			item_picker dropped_object_trigger = dropped_object.AddComponent<item_picker> ();
			dropped_object_trigger.item = item;	

	}
}
