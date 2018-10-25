using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective_Manager : MonoBehaviour {

	public static Objective_Manager instance;

	Settings settings;

	Text objective_label;
	Text objective_text;

	void Awake () {
		if (instance != null) {
			Destroy (this);
		}
		instance = this;
	}

	void Start() {
		settings = Settings.instance;

		objective_label = GameObject.Find ("objective_label").GetComponent<Text> ();
		objective_text = GameObject.Find ("objective_text").GetComponent<Text> ();
	}

	public void update_current_objective (string new_objective_eng, string new_objective_cze) {

		switch (settings.language) {
		case 1:
			objective_label.text = "Momentální ukol:";
			objective_text.text = new_objective_cze;
			break;
		default:
			objective_label.text = "Current objective:";
			objective_text.text = new_objective_eng;
			break;
		}
	}
}
