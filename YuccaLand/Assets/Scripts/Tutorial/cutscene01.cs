using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cutscene01 : MonoBehaviour {

	camera_controler main_camera;
	Settings settings;
	player_controller player;

	NPC_movement instructor_move;

	private bool instructor_move_up = false;

	private string said_text;

	private Animator player_head;
	private Animator instructor_head;


	dialog_system dialog;

	public Sprite instructor_neutral;

	public string instructor_name = "Instructor";

	//instructor_trigger instructor_trig;

	gamemanager_tutorial game_manager;

	void Start () {
		main_camera = GameObject.Find("Main Camera").GetComponent<camera_controler>();
		settings = GameObject.Find("Settings").GetComponent<Settings>();
		player = GameObject.Find ("player").GetComponent<player_controller>();

		instructor_move = GameObject.Find ("instructor").GetComponent<NPC_movement> ();

		player_head = GameObject.Find ("player_head").GetComponent<Animator> ();

		dialog = GameObject.Find ("subtitles_canvas").GetComponent<dialog_system>();

		instructor_head = instructor_move.transform.Find ("npc_head").gameObject.GetComponent<Animator>();

		//instructor_trig = instructor_move.gameObject.GetComponent<instructor_trigger> ();

		game_manager = GameObject.Find ("gamemanager_tutorial").GetComponent<gamemanager_tutorial> ();

		if (!game_manager.cutscene01_invoked) {
			game_manager.cutscene01_invoked = true;
			start_cutscene ();
		} else {
			not_start_cutscene ();
		}
	}

	void start_cutscene() {
		StartCoroutine(cutscene1());
	}

	void not_start_cutscene() {
		main_camera.change_target( GameObject.Find ("player"), 10f, true);
	}



	void Update() {
		if (instructor_move_up) {
			GameObject.Find("instructor").transform.Translate ( new Vector3 (0, 4f * Time.deltaTime, 0) );
		}
	}


	IEnumerator cutscene1()
	{
		main_camera.change_target( GameObject.Find ("cam_tar_01"), 1f, true);

		float wanted_time;
		float current_time;

		player.transform.position = new Vector3 ( -3.5f, -1f, player.transform.position.z);

		instructor_move.walkX = 0f;
		instructor_move.walkY = 0f;
		instructor_move.standX = 0f;
		instructor_move.standY = -1f;
		instructor_move.walking = false;


		player.bool_cutscene = true;
		player.bool_roam_cutscene = true;
		settings.hotbar_hidden = true;

		player.float_standX = 1f;
		player.float_standY = 0f;
		player.float_movexaxis = 0f;
		player.float_moveyaxis = 0f;
		player.bool_player_moves = false;

		player.float_standX = 1f;
		player.float_standY = 0f;
		player.float_movexaxis = 0f;
		player.float_moveyaxis = 0f;
		player.bool_player_moves = false;

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

	
		StartCoroutine ( player_walk_01() );


		yield return new WaitForSeconds(4.3f);

		main_camera.change_target( player.gameObject, 10f, false);

		wanted_time = 3f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		instructor_move_up = true;
		yield return new WaitForSeconds(0.1f);

		instructor_move_up = false;
		wanted_time = 2f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}

		said_text = "You stuck?";
		wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}

		player.float_standX = 0f;
		player.float_standY = -1f;
		wanted_time = 1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",2);

		yield return null;




		said_text = "Why can't I move?!";
		wanted_time = 2.5f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		instructor_move.walkX = 0f;
		instructor_move.walkY = 0f;
		instructor_move.standX = 0f;
		instructor_move.standY = 1f;
		instructor_move.walking = false;
		instructor_move.npc_watch_target = player.gameObject;

		said_text = "Maybe you can't, because you never tried it before...";
		wanted_time = 3f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3.2f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		player_head.SetInteger ("emotion",0);

		said_text = "...so let's try it now.";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.2f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		//GameObject Settings = GameObject.Find ("Settings");

		said_text = "You can walk by pressing "+ settings.Key_To_String(settings.cont_moveup) + ", "+ settings.Key_To_String(settings.cont_movedown) + ", "+ settings.Key_To_String(settings.cont_moveleft) + ", "+ settings.Key_To_String(settings.cont_moveright) + " buttons.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.2f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "You should try it right now.";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head)  );

		player.bool_cutscene = false;

		wanted_time = 2.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}


		yield return new WaitUntil ( () =>  player.bool_player_moves );

		wanted_time = 0.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}


		said_text = "Well done!";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;


		said_text = "Let's try something more complicated now.";
		wanted_time = 2.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "You can interact with nearby people or objects via actions.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Primary action causes inspection of an object.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Which is useful when you need to get information about something.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Secondary action is for interacting with objects.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "You use this when you want to talk with someone or pick up something.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Do you understand?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "I think I do.";
		wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Good.";
		wanted_time = 1f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 1.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Now, step close to me and use " + settings.Key_To_String(settings.cont_secaction) + " button to talk with me.";
		wanted_time = 7f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 7.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		game_manager.instructor_trigger_secaction_mode = 1;
		player.bool_roam_cutscene = false;

		settings.hotbar_hidden = false;
	}


	IEnumerator player_walk_01 () {
		player.float_movexaxis = 0.5f;
		player.float_moveyaxis = 0f;
		player.bool_player_moves = true;

		yield return new WaitUntil ( () =>  player.transform.position.x >= 0f );

		player.float_movexaxis = 0f;
		player.float_moveyaxis = 0f;
		player.bool_player_moves = false;
	}

	public IEnumerator cutscene2()
	{
		settings.already_interacting = true;
		player.bool_roam_cutscene = true;
		settings.hotbar_hidden = true;

		float wanted_time;
		float current_time;

		yield return new WaitForSeconds ( 1f );

		instructor_move.standX = 0f;
		instructor_move.standY = -1f;
		instructor_move.walkX = 0f;
		instructor_move.walkY = -1f;
		instructor_move.walking = true;

		yield return new WaitUntil (() => instructor_move.gameObject.transform.position.y <= -12f);

		instructor_move.standX = 0f;
		instructor_move.standY = -1f;
		instructor_move.walkX = 0f;
		instructor_move.walkY = 0f;
		instructor_move.walking = false;

		yield return new WaitForSeconds ( 0.25f );

		instructor_move.npc_watch_target = player.gameObject;

		yield return new WaitForSeconds ( 0.5f );

		said_text = "So as you can see, The fence got hole in it.";
		wanted_time = 2.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.6f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "I want you to repair it.";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",2);
		yield return null;
		yield return null;


		said_text = "Why me?";
		wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );
		wanted_time = 2f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "And who else is here to do it?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",0);
		yield return null;
		yield return null;

		instructor_move.npc_watch_target = null;
		instructor_move.standX = -1f;
		instructor_move.standY = 0f;

		said_text = "There is an Axe on that tree stump.";
		wanted_time = 3f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Pick it up into your inventory by using " + settings.Key_To_String(settings.cont_secaction) + " button while nearby.";
		wanted_time = 4f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 4.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		instructor_move.npc_watch_target = null;
		instructor_move.standX = 0f;
		instructor_move.standY = -1f;

		said_text = "Then go to the tree down here and use " + settings.Key_To_String(settings.cont_secaction) + " button to interact with it.";
		wanted_time = 4f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 4.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Once you do this, your inventory will pop up.";
		wanted_time = 3f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 3.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Navigate through it by movement buttons and once you select Axe, use " + settings.Key_To_String(settings.cont_action) + " button to use it on the tree.";
		wanted_time = 5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 5.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		player_head.SetInteger ("emotion",0);

		settings.hotbar_hidden = false;
		player.bool_roam_cutscene = false;
		settings.already_interacting = false;

		game_manager.instructor_trigger_secaction_mode = 4;
		game_manager.pine01_t_unlocked = true;

		yield return null;


	}

	public void cutscene3_prepare () {
		if (!game_manager.cutscene03_invoked) {
			game_manager.cutscene03_invoked = true;

			StartCoroutine ( cutscene3() );
		}	
	}

	IEnumerator cutscene3()
	{
		while (settings.already_interacting) {
			yield return null;
		}
		settings.already_interacting = true;
		player.bool_roam_cutscene = true;

		said_text = "Well done!";
		float wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 1.6f;
		float current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "So now you have a branch! Do me a favour and repair the fence with it.\nI believe you can do it without any hint now.";
		wanted_time = 6f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 6.1f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		game_manager.fence_t_unlocked = true;
		game_manager.instructor_trigger_secaction_mode = 5;

		player.bool_roam_cutscene = false;
		settings.already_interacting = false;
		yield return null;
	}

	public void cutscene5_prepare () {
		if (!game_manager.cutscene05_invoked) {
			game_manager.cutscene05_invoked = true;

			StartCoroutine ( cutscene5() );
		}	
	}

	IEnumerator cutscene5() {
		while (settings.already_interacting) {
			yield return null;
		}
		settings.already_interacting = true;
		player.bool_roam_cutscene = true;
		settings.hotbar_hidden = true;

		said_text = "So it is done.";
		float wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );
		wanted_time = 2.5f;
		float current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		instructor_move.npc_watch_target = player.gameObject;

		said_text = "Alright.";
		wanted_time = 1.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "That's it?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "I believe so.\nYes.";
		wanted_time = 2.5f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 4f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "So can I continue in my journey then?";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Not really...";
		wanted_time = 1f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 1.01f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}

		player_head.SetInteger ("emotion",2);

		yield return null;
		yield return null;
		yield return null;

		said_text = "So what is the problem now?!";
		wanted_time = 2.5f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );
		wanted_time = 3f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;


		StartCoroutine (instructor_walk_right06 ());



		said_text = "The main problem here is that the log here must be moved away from the way...";
		wanted_time = 4f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 4.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Let's do this then.";
		wanted_time = 2f;
		StartCoroutine ( dialog.say_something( dialog.player_name, said_text, wanted_time, dialog.player_portrait[player_head.GetInteger("emotion")], player_head )  );
		wanted_time = 2.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		said_text = "Alright. You can push the log away from the path. Just step close to it and then walk to it to push it away.";
		wanted_time = 6f;
		StartCoroutine ( dialog.say_something( instructor_name, said_text, wanted_time, instructor_neutral, instructor_head )  );
		wanted_time = 6.5f;
		current_time = 0f;
		while (current_time < wanted_time) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}
		yield return null;

		player_head.SetInteger ("emotion",0);

		Destroy ( GameObject.Find("log_stopper") );
		game_manager.instructor_trigger_secaction_mode = 6;

		settings.hotbar_hidden = false;
		player.bool_roam_cutscene = false;
		settings.already_interacting = false;
		yield return null;
	}

	IEnumerator instructor_walk_right06 () {
	
		instructor_move.gameObject.transform.position = new Vector3 (2f, -12f, instructor_move.gameObject.transform.position.z);

		instructor_move.npc_watch_target = null;
		instructor_move.standX = 1f;
		instructor_move.standY = 0f;
		instructor_move.walkX = 1f;
		instructor_move.walkY = 0f;
		instructor_move.walking = true;

		yield return new WaitUntil (() => instructor_move.gameObject.transform.position.x >= 5f);

		instructor_move.npc_watch_target = null;
		instructor_move.standX = 1f;
		instructor_move.standY = 0f;
		instructor_move.walkX = 0f;
		instructor_move.walkY = 0f;
		instructor_move.walking = false;


		yield return null;
	}

}
