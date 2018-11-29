using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringsHolder_Tutorial : MonoBehaviour {
	public static StringsHolder_Tutorial instance;

	public Strings_tutorial strings;

	//public Strings_tutorial cze;
	//public Strings_tutorial eng;

	Settings settings;
	menu_loader Menu_Loader;
	gamemanager_tutorial game_manager;
	Objective_Manager objective_manager;

	void Awake () {
		if (instance != null) {
			Destroy (this);
		}
		instance = this;

	}

	void Start () {
		settings = Settings.instance;

		Menu_Loader = menu_loader.instance;
		Menu_Loader.on_after_menu_call_back += reload_language;
		game_manager = gamemanager_tutorial.instance;
		objective_manager = Objective_Manager.instance;

		reload_language ();
	}


	public void reload_language() {

		switch (settings.language) {
		case 1:
			strings = Resources.Load<Strings_tutorial>("Strings/Cze/tutorial_cze");
			break;
		default:
			strings = Resources.Load<Strings_tutorial>("Strings/Eng/tutorial_eng");
			break;
		}

		objetive_update ();
	}

	public void objetive_update() {
		string objective_head = strings.obj_header;
		string objective_body;

		switch (game_manager.objective_status) {
		case 1:
			objective_body = strings.obj01_p1 + settings.Key_To_String (settings.cont_objective) + strings.obj01_p2;
			break;
		default: 
			objective_body = strings.obj_null;
			break;
		}

		objective_manager.set_objective (objective_head, objective_body);
	}

}
