using UnityEngine;

[CreateAssetMenu (fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : ScriptableObject {
	public string name_eng = "New Weapon";
	public string name_cze = "Nová zbraň";
	public Sprite icon = null;
	public int ID = 0;

	public RuntimeAnimatorController animations;

	public bool is_ranged = false;
	public int damage = 0;
	public int range = 0;
	public int ammo = 0;
	public bool is_loaded = true;
	public bool loses_damage_over_distance = false;

	public string inspect_text_eng = "This is surely a weapon...";
	public string inspect_text_cze = "Tohle je určitě zbraň...";
	public float inspect_time = 2f;
}
