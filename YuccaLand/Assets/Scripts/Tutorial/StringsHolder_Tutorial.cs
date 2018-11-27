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

	}

}
