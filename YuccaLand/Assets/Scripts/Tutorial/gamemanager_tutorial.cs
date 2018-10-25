using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager_tutorial : MonoBehaviour {
	public string current_objective_eng = "There is no active objective at the moment.";
	public string current_objective_cze = "Momentálně nemáš žádný aktivní úkol.";

	//init for opening cutscene
	public bool cutscene01_invoked = false;
	public bool cutscene03_invoked = false;
	public bool cutscene05_invoked = false;

	//init for instructor talking states
	public int instructor_trigger_secaction_mode = 0;

	//init for gate states
	public bool gate_t_unlocked = false;
	public bool gate_t_beenopened = false;
	public bool gate_t_openned = false;

	//init for Pine states
	public bool pine01_t_unlocked = false;
	public bool pine01_t_interactiondone = false;

	//init for fence states
	public bool fence_t_unlocked = false;
	public bool fence_t_repaired = false;




	menu_loader Menu_Loader;
	Objective_Manager objective_manager;
	void Start () {
		Menu_Loader = menu_loader.instance;
		Menu_Loader.on_after_menu_call_back += after_menu_init;
		objective_manager = Objective_Manager.instance;

		//Load savegame slot will not be performed in this case, because it is tutorial scenario.

		StartCoroutine ( initialize() );
	}
	
	IEnumerator initialize () {
		yield return null;// Wait one frame so all other objects can initialize

		GameObject.Find ("gate01_trigger").GetComponent<gate_trigger> ().open_close_gate (gate_t_openned);
		GameObject.Find("fence02").GetComponent<Animator>().SetBool ("repaired",fence_t_repaired);

		after_menu_init ();



	}

	void after_menu_init () {
		
		set_new_objective (null, null);


	}

	public void set_new_objective (string new_objective_eng, string new_objective_cze) {
		
		if (new_objective_eng != null && new_objective_cze != null) {
			current_objective_eng = new_objective_eng;
			current_objective_cze = new_objective_cze;
		}

		objective_manager.update_current_objective (current_objective_eng, current_objective_cze);
	}

		


}
