using UnityEngine;
using UnityEngine.UI;

public class hotbar_controler : MonoBehaviour {

	public int current_slot = 0;

	Weapon hands;

	inventory Inventory;

	Image icon;
	Text info;

	Animator animator_melee;
	Animator animator_ranged;

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

	public void update_hotbar () {
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
			info.text = weapon.name;


		} else {
			icon.enabled = false;
			info.text = "<Empty>";
		}
	}
		
}
