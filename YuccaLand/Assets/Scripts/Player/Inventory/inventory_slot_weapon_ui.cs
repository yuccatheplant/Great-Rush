using UnityEngine;
using UnityEngine.UI;

public class inventory_slot_weapon_ui : MonoBehaviour {
	Weapon weapon;

	public Image icon;
	public Text label;

	Transform pickables;

	void Start () {
		pickables = GameObject.Find ("Pickables").transform;
	}

	public void add_weapon_ui (Weapon new_weapon) {
		if (new_weapon != null) {
			weapon = new_weapon;

			icon.sprite = weapon.icon;
			icon.enabled = true;

			label.text = weapon.name;
		}
	}

	public void remove_weapon_ui () {
		weapon = null;

		icon.sprite = null;
		icon.enabled = false;

		label.text = "";
	}

	public void drop_weapon () {
		if (weapon != null) {
			GameObject dropped_object = new GameObject (weapon.name);
			dropped_object.transform.SetParent (pickables);
			GameObject player = GameObject.Find ("player");
			dropped_object.transform.position = new Vector3 ( player.transform.position.x, player.transform.position.y-0.3f , player.transform.position.z );

			SpriteRenderer dropped_object_renderer = dropped_object.AddComponent<SpriteRenderer> ();
			dropped_object_renderer.sprite = weapon.icon;
			dropped_object_renderer.sortingLayerName = "Pickable";

			BoxCollider2D dropped_object_collider = dropped_object.AddComponent<BoxCollider2D> ();
			dropped_object_collider.isTrigger = true;
			dropped_object_collider.size = new Vector2 (0.5f, 0.5f);
			dropped_object_collider.offset = new Vector2 (0f, 0f);

			weapon_picker dropped_object_trigger = dropped_object.AddComponent<weapon_picker> ();
			dropped_object_trigger.weapon = weapon;

			inventory.instance.weapon_remove (weapon);
		}
	}

}
