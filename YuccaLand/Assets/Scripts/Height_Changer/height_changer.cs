using UnityEngine;

public class height_changer : MonoBehaviour {

	public int offset = 0;

	public bool is_static = true;

	SpriteRenderer render;

	void Start () {
		render = gameObject.GetComponent<SpriteRenderer> ();

		render.sortingOrder = ((Mathf.RoundToInt (gameObject.transform.position.y * 5 )) * -1)  + offset;

		if (is_static) {
			Destroy (this);
		}
	}
	
	void Update () {
		render.sortingOrder = ((Mathf.RoundToInt (gameObject.transform.position.y * 5 )) * -1)  + offset;
	}


}
