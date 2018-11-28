using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective_Manager : MonoBehaviour {

	public static Objective_Manager instance;


	Text objective_label;
	Text objective_text;

	void Awake () {
		if (instance != null) {
			Destroy (this);
		}
		instance = this;
	}

	void Start() {
		
		objective_label = GameObject.Find ("objective_label").GetComponent<Text> ();
		objective_text = GameObject.Find ("objective_text").GetComponent<Text> ();
	}


	public void set_objective (string objective_header, string objective) {

		objective_label.text = objective_header;
		objective_text.text = objective;

	}
}
