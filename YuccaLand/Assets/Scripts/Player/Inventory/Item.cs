using UnityEngine;

[CreateAssetMenu (fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
	new public string name = "New Item";
	//public string name_eng = "New Item";
	//public string name_cze = "Nový Předmět";

	public Sprite icon = null;
	public int ID = 0;


	public string inspect_text = "This is surely an object...";
	//public string inspect_text_eng = "This is surely an object...";
	//public string inspect_text_cze = "Toto je určitě předmět...";
	public float inspect_time = 2f;
}
	