using UnityEngine;
using UnityEngine.UI;

public class inventory_ui : MonoBehaviour {

	inventory Inventory;

	public Transform items_parrent;
	inventory_slot_weapon_ui melee_slot_ui;
	inventory_slot_weapon_ui ranged_slot_ui;

	inventory_slot_ui[] slots;

	Canvas inventory_canvas;

	Settings settings;

	public int selected_slot = 0;

	Color default_color;
	Color selected_color;
	Color crafting_color;

	public bool interactive_open = false;

	Animator animator_melee;
	SpriteRenderer renderer_melee;

	Animator animator_ranged;
	SpriteRenderer renderer_ranged;

	int crafting_slot_selected= -1;

	void Start () {
		selected_color = new Vector4 (0f , 1f, 0f, 1f);//Green
		crafting_color = new Vector4 (0f , 1f, 1f, 1f);//Cyan

		Inventory = inventory.instance;

		Inventory.on_item_changed_call_back += update_ui;

		melee_slot_ui = GameObject.Find ("melee_slot").GetComponent<inventory_slot_weapon_ui> ();
		ranged_slot_ui = GameObject.Find ("ranged_slot").GetComponent<inventory_slot_weapon_ui> ();
		slots = items_parrent.GetComponentsInChildren<inventory_slot_ui>();

		default_color = slots [selected_slot].GetComponent<Image> ().color;
		slots [selected_slot].GetComponent<Image> ().color = selected_color;

		inventory_canvas = gameObject.GetComponent<Canvas> ();

		settings = GameObject.Find ("Settings").GetComponent<Settings> ();
		inventory_canvas.enabled = settings.inventory_opened;

		animator_melee = GameObject.Find ("player_melee_slot").GetComponent<Animator> ();
		renderer_melee = animator_melee.gameObject.GetComponent<SpriteRenderer> ();

		animator_ranged = GameObject.Find ("player_ranged_slot").GetComponent<Animator> ();
		renderer_ranged = animator_ranged.gameObject.GetComponent<SpriteRenderer> ();

		if (Inventory.on_item_changed_call_back != null) {
			Inventory.on_item_changed_call_back.Invoke ();
		}
	}

	public void open_close_inventory () {
		settings.inventory_opened = !settings.inventory_opened;
		inventory_canvas.enabled = settings.inventory_opened;
		if (settings.inventory_opened) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	void update_ui () {
		
		for (int i = 0 ; i<Inventory.inventory_space ; i++) {
			
			if (i < Inventory.items.Count) {
				slots [i].add_item_ui (Inventory.items [i]);
			} else {
				slots [i].remove_item_ui ();
			}

		}

		melee_slot_ui.add_weapon_ui (Inventory.melee_weapon);
		ranged_slot_ui.add_weapon_ui (Inventory.ranged_weapon);

		if (Inventory.melee_weapon != null) {
			animator_melee.runtimeAnimatorController = Inventory.melee_weapon.animations;
			renderer_melee.enabled = true;
		} else {
			renderer_melee.enabled = false;
		}

		if (Inventory.ranged_weapon != null) {
			animator_ranged.runtimeAnimatorController = Inventory.ranged_weapon.animations;
			renderer_ranged.enabled = true;
		} else {
			renderer_ranged.enabled = false;
		}
	}



	public void move_up_in_inventory () {
		paint_slot ( default_color );

		switch (selected_slot) {
		case 0:
		case 1:
		case 10:
			break;
		case 11:
			selected_slot = 10;
			break;
		default:
			selected_slot -= 2;
			break;
		}

		paint_slot ( selected_color );

	}

	public void move_down_in_inventory () {
		paint_slot ( default_color );

		switch (selected_slot) {
		case 8:
		case 9:
		case 11:
			break;
		case 10:
			selected_slot = 11;
			break;
		default:
			selected_slot += 2;
			break;
		}

		paint_slot ( selected_color );
	}

	public void move_left_in_inventory () {
		paint_slot ( default_color );

		switch (selected_slot) {
		case 0:
		case 2:
			selected_slot = 10;
			break;
		case 4:
		case 6:
		case 8:
			selected_slot = 11;
			break;
		case 10:
		case 11:
			break;
		default:
			selected_slot--;
			break;
		}

		paint_slot ( selected_color );
	}

	public void move_right_in_inventory () {
		paint_slot ( default_color );

		switch (selected_slot) {
		case 1:
		case 3:
		case 5:
		case 7:
		case 9:
			break;
		case 10:
			selected_slot = 0;
			break;
		case 11:
			selected_slot = 4;
			break;
		default:
			selected_slot++;
			break;
		}

		paint_slot ( selected_color );
	}


	void paint_slot( Color color ) {

		if (selected_slot == crafting_slot_selected) {
			return;
		}

		switch (selected_slot) {
		case 10:
			melee_slot_ui.GetComponent<Image> ().color = color;
			break;
		case 11:
			ranged_slot_ui.GetComponent<Image> ().color = color;
			break;
		default:
			slots [selected_slot].GetComponent<Image> ().color = color;
			break;
		}
	}

	public void select_drop_slot () {
		if (selected_slot < 10) {
			slots [selected_slot].drop_item ();
		}
	} 


	public void select_action () {
		if (interactive_open) {
			interactive_open = true;
			open_close_inventory ();
		} else {
			//crafting
			if (crafting_slot_selected < 0) {
				//First item
				switch (selected_slot) {
				case 10:
					if (Inventory.melee_weapon != null) {
						crafting_slot_selected = selected_slot;
						melee_slot_ui.GetComponent<Image>().color = crafting_color;
					}
					break;
				case 11:
					if (Inventory.ranged_weapon != null) {
						crafting_slot_selected = selected_slot;
						ranged_slot_ui.GetComponent<Image>().color = crafting_color;
					}
					break;
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					if (Inventory.items.Count > selected_slot) {
						crafting_slot_selected = selected_slot;
						slots [crafting_slot_selected].GetComponent<Image> ().color = crafting_color;
					}
					break;
				}


			} else {
				//Second item
			}
		}
	}


}
