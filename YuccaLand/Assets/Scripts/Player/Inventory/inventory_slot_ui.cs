using UnityEngine;
using UnityEngine.UI;

public class inventory_slot_ui : MonoBehaviour {
	Item item;

	public Image icon;
	public Text label;

	Transform pickables;

	void Start () {
		pickables = GameObject.Find ("Pickables").transform;
	}

	public void add_item_ui (Item new_item) {
		item = new_item;

		icon.sprite = item.icon;
		icon.enabled = true;

		label.text = item.name_eng;
	}

	public void remove_item_ui () {
		item = null;

		icon.sprite = null;
		icon.enabled = false;

		label.text = "";
	}

	public void drop_item () {
		if (item != null) {
			GameObject dropped_object = new GameObject (item.name);
			dropped_object.transform.SetParent (pickables);
			GameObject player = GameObject.Find ("player");
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
			//dropped_object_trigger.player_bubble = GameObject.Find ("player_bubble");


			inventory.instance.item_remove (item);
		}
	}

}
