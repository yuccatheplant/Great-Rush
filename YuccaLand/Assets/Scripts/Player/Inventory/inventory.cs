using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour {

	#region Singleton

	public delegate void on_item_changed();
	public on_item_changed on_item_changed_call_back;

	public static inventory instance;

	public int inventory_space = 10;

	public Weapon melee_weapon;
	public Weapon ranged_weapon;
	public List<Item> items = new List<Item> ();

	inventory_slot_weapon_ui melee_slot;
	inventory_slot_weapon_ui ranged_slot;




	void Awake () {
		if (instance != null) {
			Debug.Log ("WARNING! More than one instance of inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

	void Start() {
		melee_slot = GameObject.Find ("melee_slot").GetComponent<inventory_slot_weapon_ui> ();
		ranged_slot = GameObject.Find ("ranged_slot").GetComponent<inventory_slot_weapon_ui> ();


	}

	public void weapon_add ( Weapon new_weapon ) {
		if (new_weapon.is_ranged) {
			weapon_add_ranged ( new_weapon );
		} else {
			weapon_add_melee ( new_weapon );
		}

	}

	void weapon_add_melee(Weapon new_weapon){
		if (melee_weapon != null) {
			melee_slot.drop_weapon ();
		}

		melee_weapon = new_weapon;

		if (on_item_changed_call_back != null) {
			on_item_changed_call_back.Invoke ();
		}
	}

	void weapon_add_ranged(Weapon new_weapon){
		if (ranged_weapon != null) {
			ranged_slot.drop_weapon ();
		} 
		ranged_weapon = new_weapon;

		if (on_item_changed_call_back != null) {
			on_item_changed_call_back.Invoke ();
		}
	}

	public void weapon_remove (Weapon weapon) {

		if (weapon.is_ranged) {
			ranged_weapon = null;
		} else {
			melee_weapon = null;
		}



		if (on_item_changed_call_back != null) {
			on_item_changed_call_back.Invoke ();
		}
	}



	public bool item_add (Item item) {
		
		if (items.Count >= inventory_space) {
			return false;
		}

		items.Add (item);

		if (on_item_changed_call_back != null) {
			on_item_changed_call_back.Invoke ();
		}

		return true;
	}

	public void item_remove (Item item) {
		
		items.Remove (item);

		if (on_item_changed_call_back != null) {
			on_item_changed_call_back.Invoke ();
		}
	}


}
