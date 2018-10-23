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



	Text objective_label;
	Text objective_text;
	Settings settings;
	void Start () {
		objective_label = GameObject.Find ("objective_label").GetComponent<Text> ();
		objective_text = GameObject.Find ("objective_text").GetComponent<Text> ();
		settings = GameObject.Find ("Settings").GetComponent<Settings> ();
		//Load savegame slot will not be performed in this case, because it is tutorial scenario.

		StartCoroutine ( initialize() );
	}
	
	IEnumerator initialize () {
		yield return null;// Wait one frame so all other objects can initialize

		GameObject.Find ("gate01_trigger").GetComponent<gate_trigger> ().open_close_gate (gate_t_openned);
		GameObject.Find("fence02").GetComponent<Animator>().SetBool ("repaired",fence_t_repaired);

		update_current_objective (current_objective_eng,current_objective_cze);

	}


	public void update_current_objective (string new_objective_eng, string new_objective_cze) {
		
		current_objective_eng = new_objective_eng;
		current_objective_cze = new_objective_cze;


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
