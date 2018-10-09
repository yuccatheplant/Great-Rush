using UnityEngine;

[CreateAssetMenu (fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : ScriptableObject {
	new public string name = "New Weapon";
	public Sprite icon = null;
	public int ID = 0;

	public RuntimeAnimatorController animations;

	public bool is_ranged = false;
	public int damage = 0;
	public int range = 0;
	public int ammo = 0;
	public bool is_loaded = true;
	public bool loses_damage_over_distance = false;

	public string inspect_text = "This is surely a weapon...";
	public float inspect_time = 2f;
}
