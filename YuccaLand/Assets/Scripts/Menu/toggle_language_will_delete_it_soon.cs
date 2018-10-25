using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toggle_language_will_delete_it_soon : MonoBehaviour {


	void Start () {

		if (Settings.instance.language == 1) {
			gameObject.GetComponent<Toggle> ().isOn = true;
		} else {
			gameObject.GetComponent<Toggle> ().isOn = false;
		}
	
		Destroy (this);
	}
}
