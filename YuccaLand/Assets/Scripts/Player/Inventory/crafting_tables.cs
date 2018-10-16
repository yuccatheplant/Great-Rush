using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crafting_tables : MonoBehaviour {
	public Item[] crafted_items;

	GameObject player;
	Transform pickables;
	inventory Inventory;
	void Start () {
		player = GameObject.Find ("player");
		pickables = GameObject.Find ("Pickables").transform;
		Inventory = inventory.instance;
	}

//Function that compares first ID
	public bool find_recipe1 (int slot1, int slot2, int id1, int id2) {
		bool result = false;

		switch (id1) {
		case 1: 
			result = find_recipe2_id_1 (slot1, slot2, id2);
			break;
		}

		return result;
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
			Inventory.item_add (crafted_items [0]);
		} else {
			drop_item (crafted_items [0]);
		}

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
