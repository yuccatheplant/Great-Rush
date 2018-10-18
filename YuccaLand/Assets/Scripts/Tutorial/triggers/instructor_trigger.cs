using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instructor_trigger : MonoBehaviour {

	GameObject player;
	NPC_movement instructor;
	Settings settings;

	string said_text;
	bool triggered = false;

	bool first_inspection = true;

	Animator player_head;

	Animator instructor_head;

	dialog_system dialog;

	public Sprite instructor_neutral;

	public string instructor_name = "Instructor";

	NPC_movement instructor_controller;

	gamemanager_tutorial game_manager;



	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
		instructor = GameObject.Find("instructor").GetComponent<NPC_movement>();
		settings = GameObject.Find("Settings").GetComponent<Settings>();

		player_head = GameObject.Find ("player_head").GetComponent<Animator>();

		dialog = GameObject.Find ("subtitles_canvas").GetComponent<dialog_system>();

		instructor_head = gameObject.transform.Find ("npc_head").gameObject.GetComponent<Animator>();

		instructor_controller = gameObject.GetComponent<NPC_movement> ();
		game_manager = GameObject.Find ("gamemanager_tutorial").GetComponent<gamemanager_tutorial> ();
	}

	void Update () {
		if (triggered) {
			if (player.GetComponent<player_controller> ().bool_action_pressed && game_manager.instructor_trigger_secaction_mode > 1) {
				StartCoroutine (primary_action ());

			} else
				if (player.GetComponent<player_controller> ().bool_secaction_pressed && game_manager.instructor_trigger_secaction_mode > 0) {
					secondary_action_picker ();
				} 
		}
	}

	void OnTriggerEnter2D ( Collider2D other) {
		if (other.tag == "action_receiver") {
			triggered = true;
		}
	}

	void OnTriggerExit2D ( Collider2D other ) {
		if (other.tag == "action_receiver") {
			triggered = false;
		}
	}


	IEnumerator primary_action () {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		if (first_inspection) {
			player.GetComponent<player_controller> ().bool_cutscene = true;

			player.GetComponent<player_controller> ().float_movexaxis = 0f;
			player.GetComponent<player_controller> ().float_moveyaxis = 0f;
			player.GetComponent<player_controller> ().bool_player_moves = false;
		}
		player.GetComponent<player_controller> ().bool_roam_cutscene = true;

		said_text = "This man is helping me, but I don't like him anyway.";
		float wanted_time = 3.5f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );

		float current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}



		if (first_inspection) {
			first_inspection = false;
			StartCoroutine (instructor_talk_inspection ());
		} else {
			player.GetComponent<player_controller> ().bool_roam_cutscene = false;
		}


		settings.already_interacting = false;
	}



	void secondary_action_picker () {
		

		switch (game_manager.instructor_trigger_secaction_mode) {
		case 1:
			StartCoroutine( instructor_talk_01() );
			break;
		case 3:
			StartCoroutine( instructor_talk_03() );
			break;
		case 4:
			StartCoroutine( instructor_talk_04() );
			break;
		case 5:
			StartCoroutine( instructor_talk_05() );
			break;
		case 6:
			StartCoroutine( instructor_talk_06() );
			break;
		}
			
	}


	IEnumerator instructor_talk_01() {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;



		float wanted_time;
		float current_time;

		player.GetComponent<player_controller> ().bool_cutscene = true;
		player.GetComponent<player_controller> ().bool_roam_cutscene = true;
	
		player.GetComponent<player_controller> ().float_movexaxis = 0f;
		player.GetComponent<player_controller> ().float_moveyaxis = 0f;
		player.GetComponent<player_controller> ().bool_player_moves = false;

		wanted_time = 0.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		said_text = "So...";
		wanted_time = 1f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );
		wanted_time = 0.7f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}


		instructor.GetComponent<NPC_movement> ().standX = player.transform.position.x - instructor.transform.position.x;
		instructor.GetComponent<NPC_movement> ().standY = player.transform.position.y - instructor.transform.position.y;
		wanted_time = 0.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "How are you?";
		wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );
		wanted_time = 1.8f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "I'm fine, thanks for asking.";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Alright...";
		wanted_time = 3f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );
		wanted_time = 3f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "So you are able to talk to people now.";
		wanted_time = 2.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.7f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "That's good, because you will talk a lot in this game.";
		wanted_time = 3f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",1);

		yield return null;
		yield return null;

		said_text = "As long as there won't be any motivational speeches, I am fine.";
		wanted_time = 4f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_happy, player_head )  );
		wanted_time = 5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Well...";
		wanted_time = 1f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 1.2f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",0);
		yield return null;
		yield return null;

		said_text = "Don't say, there will be motivational speeches.";
		wanted_time = 3f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );
		wanted_time = 3.2f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Just please don't.";
		wanted_time = 2.5f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );
		wanted_time = 3.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		yield return null;

		said_text = "There might be some...";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",3);

		yield return null;
		yield return null;

		said_text = "Oh great...";
		wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_sad, player_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;


		said_text = "Back to business.";
		wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 1.6f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",0);

		yield return null;

		said_text = "As I said before, you can inspect things or people.";
		wanted_time = 3.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3.6f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "You can try it on me now.";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Step close to me as before and use " + settings.GetComponent<Settings>().Key_To_String(settings.GetComponent<Settings>().cont_action) + " button to inspect me.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		game_manager.instructor_trigger_secaction_mode = 2;

		player.GetComponent<player_controller> ().bool_cutscene = false;
		player.GetComponent<player_controller> ().bool_roam_cutscene = false;


		settings.already_interacting = false;
	}





	IEnumerator instructor_talk_inspection() {
		float wanted_time;
		float current_time;

		player.GetComponent<player_controller> ().bool_cutscene = true;
		player.GetComponent<player_controller> ().bool_roam_cutscene = true;

		player.GetComponent<player_controller> ().float_movexaxis = 0f;
		player.GetComponent<player_controller> ().float_moveyaxis = 0f;
		player.GetComponent<player_controller> ().bool_player_moves = false;

		wanted_time = 1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		instructor.GetComponent<NPC_movement> ().standX = player.transform.position.x - instructor.transform.position.x;
		instructor.GetComponent<NPC_movement> ().standY = player.transform.position.y - instructor.transform.position.y;

		said_text = "Good job!";
		wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );


		wanted_time = 1.6f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;


		said_text = "I just have one friendly advice.";
		wanted_time = 3f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Next time, say it in your head and not out loud!";
		wanted_time = 4f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 4.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",3);

		yield return null;
		yield return null;

		said_text = "Damn, sorry...";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_sad, player_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "No harm done.";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Talking and inspecting is good.";
		wanted_time = 2.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.6f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		player_head.SetInteger ("emotion",0);

		yield return null;
		yield return null;

		said_text = "But now you need to do something with an object.";
		wanted_time = 3f;
		StartCoroutine ( dialog.say_something(instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Do you see that fence here?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 1.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		player.GetComponent<player_controller> ().float_standX = 0f;
		player.GetComponent<player_controller> ().float_standY = -1f;

		instructor.npc_watch_target = null;

		instructor.GetComponent<NPC_movement> ().standX = 0f;
		instructor.GetComponent<NPC_movement> ().standY = -1f;


		wanted_time = 0.6f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "There is a gate in it.";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Open it by using " + settings.GetComponent<Settings>().Key_To_String(settings.GetComponent<Settings>().cont_secaction) + " button.";
		wanted_time = 4f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 4.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		game_manager.gate_t_unlocked = true;

		player.GetComponent<player_controller> ().bool_roam_cutscene = false;
		player.GetComponent<player_controller> ().bool_cutscene = false;

		player.GetComponent<player_controller> ().bool_roam_cutscene = false;

		game_manager.instructor_trigger_secaction_mode = 3;
	}




	IEnumerator instructor_talk_03() {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player.GetComponent<player_controller> ().bool_cutscene = true;
		player.GetComponent<player_controller> ().bool_roam_cutscene = true;

		player.GetComponent<player_controller> ().float_movexaxis = 0f;
		player.GetComponent<player_controller> ().float_moveyaxis = 0f;
		player.GetComponent<player_controller> ().bool_player_moves = false;

		float wanted_time;
		float current_time;


		said_text = "Can you repeat it for me?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );

		wanted_time = 2.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		instructor_controller.npc_watch_target = player;

		wanted_time = 1.7f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;


		said_text = "Open the fence gate by Secondary Action, which is assigned to " + settings.Key_To_String( settings.cont_secaction ) + " button.";
		wanted_time = 4f;
		StartCoroutine ( dialog.say_something(instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 4.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;



		instructor_controller.npc_watch_target = null;
		instructor_controller.standX = 0f;
		instructor_controller.standY = -1f;

		player.GetComponent<player_controller> ().bool_cutscene = false;
		player.GetComponent<player_controller> ().bool_roam_cutscene = false;

		settings.already_interacting = false;
		yield return null;
	}




	IEnumerator instructor_talk_04() {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player.GetComponent<player_controller> ().bool_cutscene = true;
		player.GetComponent<player_controller> ().bool_roam_cutscene = true;

		player.GetComponent<player_controller> ().float_movexaxis = 0f;
		player.GetComponent<player_controller> ().float_moveyaxis = 0f;
		player.GetComponent<player_controller> ().bool_player_moves = false;

		float wanted_time;
		float current_time;


		said_text = "Can you repeat it for me?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );

		wanted_time = 2.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}

		instructor_controller.npc_watch_target = player;

		wanted_time = 1.7f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Go to that axe on the left and pick it up using Secondary Action, which is assigned to " + settings.Key_To_String( settings.cont_secaction ) + " button.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something(instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Then move to the tree down here and once again use Secondary Action to interact with it.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something(instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Your inventory will be shown, where you use movement buttons to navigate inside.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something(instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Select Axe in the inventory by Primary Action which is assigned to " + settings.Key_To_String( settings.cont_action ) + " button while the correct slot is highlited.";
		wanted_time = 6f;
		StartCoroutine ( dialog.say_something(instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 6.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		instructor_controller.npc_watch_target = null;
		instructor_controller.standX = 0f;
		instructor_controller.standY = -1f;

		player.GetComponent<player_controller> ().bool_cutscene = false;
		player.GetComponent<player_controller> ().bool_roam_cutscene = false;

		settings.already_interacting = false;
		yield return null;
	}



	IEnumerator instructor_talk_05() {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player.GetComponent<player_controller> ().bool_cutscene = true;
		player.GetComponent<player_controller> ().bool_roam_cutscene = true;

		player.GetComponent<player_controller> ().float_movexaxis = 0f;
		player.GetComponent<player_controller> ().float_moveyaxis = 0f;
		player.GetComponent<player_controller> ().bool_player_moves = false;

		float wanted_time;
		float current_time;



		said_text = "Can you repeat it for me?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );

		wanted_time = 2.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		instructor_controller.npc_watch_target = player;

		said_text = "Since you have a branch now, you just need to step to the fence, use Secondary action to interact with it and select Branch by Primary action when in Inventory.";
		wanted_time = 8f;
		StartCoroutine ( dialog.say_something(instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 8.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;


		instructor_controller.npc_watch_target = null;
		instructor_controller.standX = 0f;
		instructor_controller.standY = -1f;

		settings.already_interacting = false;
		player.GetComponent<player_controller> ().bool_cutscene = false;
		player.GetComponent<player_controller> ().bool_roam_cutscene = false;

		yield return null;
	}


	IEnumerator instructor_talk_06() {
		if (settings.already_interacting) {
			yield break;
		}
		settings.already_interacting = true;

		player.GetComponent<player_controller> ().bool_cutscene = true;
		player.GetComponent<player_controller> ().bool_roam_cutscene = true;

		player.GetComponent<player_controller> ().float_movexaxis = 0f;
		player.GetComponent<player_controller> ().float_moveyaxis = 0f;
		player.GetComponent<player_controller> ().bool_player_moves = false;

		float wanted_time;
		float current_time;


		said_text = "Can you repeat it for me?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_neutral, player_head )  );

		wanted_time = 2.25f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		instructor_controller.npc_watch_target = player;


		said_text = "Just walk towards the log to push it away.";
		wanted_time = 6f;
		StartCoroutine ( dialog.say_something(instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 6.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.GetComponent<Settings>().cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;




		instructor_controller.npc_watch_target = null;
		instructor_controller.standX = 1f;
		instructor_controller.standY = 0f;

		settings.already_interacting = false;
		player.GetComponent<player_controller> ().bool_cutscene = false;
		player.GetComponent<player_controller> ().bool_roam_cutscene = false;

		yield return null;
	}
}
