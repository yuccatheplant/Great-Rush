using UnityEngine;
using UnityEngine.UI;

public class hotbar_controler : MonoBehaviour {
	public static hotbar_controler instance;


	int current_slot = 0;

	Weapon hands;

	inventory Inventory;

	Image icon;
	Text info;

	Animator animator_melee;
	Animator animator_ranged;



	void Awake () {
		if (hotbar_controler.instance != null) {
			Destroy (this);
		}
		hotbar_controler.instance = this;
	}

	void Start () {
		Inventory = inventory.instance;

		icon = gameObject.transform.Find ("Image").GetComponent<Image> ();
		info = gameObject.transform.Find ("Text").GetComponent<Text> ();

		animator_melee = GameObject.Find ("player_melee_slot").GetComponent<Animator> ();
		animator_ranged = GameObject.Find ("player_ranged_slot").GetComponent<Animator> ();

		hands = Resources.Load<Weapon> ("Inventory/Weapons/_hands");

		Inventory.on_item_changed_call_back += update_hotbar;
		update_hotbar ();
	}

	public void set_hotbar (int state) {
		if (state != current_slot) {
			current_slot = state;
			update_hotbar();
		}

	}

	void update_hotbar () {

		player_controller.instance.force_animation_reset = true;

		switch (current_slot) {
		case 1:
			animator_melee.SetBool ("Equiped", true);
			animator_ranged.SetBool ("Equiped", false);

			set_weapon_ui (Inventory.melee_weapon);
			break;
		case 2:	
			animator_melee.SetBool ("Equiped", false);
			animator_ranged.SetBool ("Equiped", true);

			set_weapon_ui (Inventory.ranged_weapon);
			break;
		default: 
			player_controller.instance.force_animation_reset = false;
			animator_melee.SetBool ("Equiped", false);
			animator_ranged.SetBool ("Equiped", false);

			set_weapon_ui (hands);
			break;	
		}
	}

	void set_weapon_ui (Weapon weapon) {
		if (weapon != null) {
			icon.sprite = weapon.icon;
			icon.enabled = true;


			switch (Settings.instance.language) {
			case 1:
				info.text = weapon.name_cze;
				break;
			default:
				info.text = weapon.name_eng;
				break;
			}


		} else {
			//Recursive call with forced hands in case, player does not have weapon on selected slot
			set_hotbar (0);

		}
	}
		
}
