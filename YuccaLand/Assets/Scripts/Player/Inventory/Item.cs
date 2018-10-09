using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
	new public string name = "New Item";
	public Sprite icon = null;
	public int ID = 0;



	public string inspect_text = "This is surely an object...";
	public float inspect_time = 2f;
}
	