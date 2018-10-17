using System.Collections;
using UnityEngine;

public class gamemanager_tutorial : MonoBehaviour {

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


	void Start () {
		//Load savegame slot will not be performed in this case, because it is tutorial scenario.

		StartCoroutine ( initialize() );
	}
	
	IEnumerator initialize () {
		yield return null; // Wait one frame so all other objects can initialize

		GameObject.Find ("gate01_trigger").GetComponent<gate_trigger> ().open_close_gate (gate_t_openned);
		GameObject.Find("fence02").GetComponent<Animator>().SetBool ("repaired",fence_t_repaired);
	}


}
