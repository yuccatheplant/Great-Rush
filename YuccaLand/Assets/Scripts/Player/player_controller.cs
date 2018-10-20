using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour {

	public bool bool_cutscene = false;
	public bool bool_roam_cutscene = false;

	public byte byte_game_mode = 0;
	public float float_move_speed;

	public float float_basic_stand_speed;
	public float float_basic_crouching_speed;

	private Rigidbody2D rigidbody_player;
	private Animator animator_player;
	private Animator animator_head;
	private Animator animator_handR;
	private Animator animator_handL;
	private Animator animator_melee;
	private Animator animator_ranged;

	public float float_movexaxis = 0f;
	public float float_moveyaxis = 0f;
	public bool bool_player_moves = false;
	public float float_standX=0f;
	public float float_standY=-1f;

	public bool bool_crouching = false;
	bool bool_was_crouching = false;

	public bool bool_action_pressed = false;
	public bool bool_secaction_pressed = false;

	BoxCollider2D player_action_receiver;

	Settings settings;

	inventory_ui inventory_setter;

	hotbar_controler hotbar;

	float float_mouse_wheel;

	float blink = 0f;
	float next_blink;

	float PreviousX = 0f;
	float PreviousY = 0f;

	Vector3 v3_handR;
	Vector3 v3_handL;
	Vector3 v3_weapon_melee;
	Vector3 v3_weapon_ranged;

	Canvas objective_canvas;
	Canvas hotbar_canvas;


	// Use this for initialization
	void Start () {
		rigidbody_player = GetComponent<Rigidbody2D>();
		animator_player = gameObject.GetComponent<Animator> ();
		animator_head = GameObject.Find ("player_head").GetComponent<Animator> ();
		animator_handR = GameObject.Find ("player_handR").GetComponent<Animator> ();
		animator_handL = GameObject.Find ("player_handL").GetComponent<Animator> ();

		animator_melee = GameObject.Find ("player_melee_slot").GetComponent<Animator> ();

		animator_ranged = GameObject.Find ("player_ranged_slot").GetComponent<Animator> ();


		player_action_receiver = GameObject.Find ("player_action_receiver").GetComponent<BoxCollider2D> ();
		settings = GameObject.Find ("Settings").GetComponent<Settings>();
		inventory_setter = GameObject.Find ("inventory_canvas").GetComponent<inventory_ui> ();
		hotbar = GameObject.Find ("hotbar_slot").GetComponent<hotbar_controler> ();

		next_blink= Random.Range (3f, 7f);

		objective_canvas = GameObject.Find ("objective_canvas").GetComponent<Canvas> ();
		hotbar_canvas = GameObject.Find ("hotbar_canvas").GetComponent<Canvas> ();

		StartCoroutine (init_close_objective ());
	}
		
	
	void Update () {
		if (settings.game_paused) {
			return;
		}

		if (settings.already_interacting || bool_cutscene || settings.inventory_opened) {
			settings.objective_opened = open_close_objectives (false);
		}

		float_mouse_wheel = Input.GetAxisRaw ("Mouse ScrollWheel");

		if (!settings.inventory_opened) {

			settings.cutscene_skip = false;


			if (!bool_cutscene) {
				float_movexaxis = 0f;
				float_moveyaxis = 0f;
				bool_player_moves = false;
			}

			if (!settings.hotbar_hidden) {
				hotbar_canvas.enabled = !settings.objective_opened;
			} else {
				hotbar_canvas.enabled = false;
			}


			bool_action_pressed = false;
			bool_secaction_pressed = false;


			if (!bool_cutscene && is_pressed (settings.cont_crouch_hold, float_mouse_wheel, false)) {
				bool_crouching = true;
				bool_was_crouching = true;
			} else {
				if (bool_was_crouching) {
					bool_crouching = false;
					bool_was_crouching = false;
				}
			}

			if (Input.anyKey || float_mouse_wheel != 0f) {


				if (is_pressed (settings.cont_action, float_mouse_wheel, true)) {
					if (bool_cutscene || bool_roam_cutscene) {
						settings.cutscene_skip = true;
					} else {
						bool_action_pressed = true;
					}
				}

				if (!bool_cutscene) {



					if (is_pressed (settings.cont_moveup, float_mouse_wheel, false)) {
						float_moveyaxis = float_moveyaxis + 1f;
					}
					if (is_pressed (settings.cont_movedown, float_mouse_wheel, false)) {
						float_moveyaxis = float_moveyaxis - 1f;
					}
					if (is_pressed (settings.cont_moveleft, float_mouse_wheel, false)) {
						float_movexaxis = float_movexaxis - 1f;
					}
					if (is_pressed (settings.cont_moveright, float_mouse_wheel, false)) {
						float_movexaxis = float_movexaxis + 1f;
					}
	
				
					if (is_pressed (settings.cont_crouch_toggle, float_mouse_wheel, true)) {
						bool_crouching = !bool_crouching;
					}

					if (is_pressed (settings.cont_hotbar1, float_mouse_wheel, true)) {
						hotbar.current_slot = 0;
						hotbar.update_hotbar ();
					}
					if (is_pressed (settings.cont_hotbar2, float_mouse_wheel, true)) {
						hotbar.current_slot = 1;
						hotbar.update_hotbar ();
					}
					if (is_pressed (settings.cont_hotbar3, float_mouse_wheel, true)) {
						hotbar.current_slot = 2;
						hotbar.update_hotbar ();
					}



					if (!bool_roam_cutscene) {
					
						if (is_pressed (settings.cont_objective, float_mouse_wheel, true)) {
							if (!settings.already_interacting) {
								settings.objective_opened = open_close_objectives (!settings.objective_opened);
							}
						}

						if (is_pressed (settings.cont_secaction, float_mouse_wheel, true)) {
							bool_secaction_pressed = true;
						}

						if (is_pressed (settings.cont_inventory, float_mouse_wheel, true)) {
							inventory_setter.interactive_open = false;
							inventory_setter.open_close_inventory ();
						}
					}
				}
				
			}
				
			if (float_movexaxis != 0f || float_moveyaxis != 0f) {
				bool_player_moves = true;
				float_standX = float_movexaxis;
				float_standY = float_moveyaxis;
			}
				
			player_action_receiver.offset = new Vector2 (float_standX * 0.3f, float_standY * 0.3f);



			if (float_movexaxis != 0f && float_moveyaxis != 0f) {
				float_movexaxis = float_movexaxis * 0.7f;
				float_moveyaxis = float_moveyaxis * 0.7f;
			}

			rigidbody_player.velocity = new Vector2 (float_movexaxis * float_move_speed, float_moveyaxis * float_move_speed);
			animator_player.SetFloat ("MoveX", float_movexaxis);
			animator_player.SetFloat ("MoveY", float_moveyaxis);
			animator_player.SetFloat ("LastX", float_standX);
			animator_player.SetFloat ("LastY", float_standY);
			animator_player.SetBool ("Moving", bool_player_moves);

			animator_head.SetFloat ("LastX", float_standX);
			animator_head.SetFloat ("LastY", float_standY);
			animator_head.SetBool ("blink",false);

			animator_handR.SetFloat ("MoveX", float_movexaxis);
			animator_handR.SetFloat ("MoveY", float_moveyaxis);
			animator_handR.SetFloat ("LastX", float_standX);
			animator_handR.SetFloat ("LastY", float_standY);
			animator_handR.SetBool ("Moving", bool_player_moves);

			animator_handL.SetFloat ("MoveX", float_movexaxis);
			animator_handL.SetFloat ("MoveY", float_moveyaxis);
			animator_handL.SetFloat ("LastX", float_standX);
			animator_handL.SetFloat ("LastY", float_standY);
			animator_handL.SetBool ("Moving", bool_player_moves);

			animator_melee.SetFloat ("MoveX", float_movexaxis);
			animator_melee.SetFloat ("MoveY", float_moveyaxis);
			animator_melee.SetFloat ("LastX", float_standX);
			animator_melee.SetFloat ("LastY", float_standY);
			animator_melee.SetBool ("Moving", bool_player_moves);

			animator_ranged.SetFloat ("MoveX", float_movexaxis);
			animator_ranged.SetFloat ("MoveY", float_moveyaxis);
			animator_ranged.SetFloat ("LastX", float_standX);
			animator_ranged.SetFloat ("LastY", float_standY);
			animator_ranged.SetBool ("Moving", bool_player_moves);

			if (PreviousX != float_standX || PreviousY != float_standX) {
				v3_handR = new Vector3 ( animator_handR.gameObject.transform.localPosition.x , animator_handR.gameObject.transform.localPosition.y,  0f );
				v3_handL = new Vector3 ( animator_handL.gameObject.transform.localPosition.x, animator_handL.gameObject.transform.localPosition.y, 0f );
				v3_weapon_melee = new Vector3 ( animator_melee.gameObject.transform.localPosition.x, animator_melee.gameObject.transform.localPosition.y, -0.01f );
				v3_weapon_ranged = new Vector3 ( animator_ranged.gameObject.transform.localPosition.x, animator_ranged.gameObject.transform.localPosition.y, -0.01f );
			
				if (float_standY == 0f) {
				
					if (float_standX > 0f) {
						if (animator_melee.GetBool ("Equiped") == false) {
							v3_weapon_melee = new Vector3 ( v3_weapon_melee.x, v3_weapon_melee.y, 0.01f );
						}
						v3_handR = new Vector3 ( v3_handR.x, v3_handR.y, -0.02f );
						v3_handL = new Vector3 ( v3_handL.x, v3_handL.y, 0.02f );
					}

					if (float_standX < 0f) {
						if (animator_ranged.GetBool ("Equiped") == false) {
							v3_weapon_ranged = new Vector3 ( 0f, 0f, 0.01f );
						}
						v3_handR = new Vector3 ( v3_handR.x, v3_handR.y, 0.02f );
						v3_handL = new Vector3 ( v3_handL.x, v3_handL.y, -0.02f );
					}
				}

				animator_handR.gameObject.transform.localPosition = v3_handR;
				animator_handL.gameObject.transform.localPosition = v3_handL;
				animator_melee.gameObject.transform.localPosition = v3_weapon_melee;
				animator_ranged.gameObject.transform.localPosition = v3_weapon_ranged;
			}
		
		} else {
			//Inventory is opened
			if (Input.anyKey || float_mouse_wheel != 0f) {
				//Key pressed. Only INVENTORY, UP/DOWN/LEFT/RIGHT and ACTION/SECACTION buttons will be detected!

				if (is_pressed (settings.cont_inventory, float_mouse_wheel, true)) {
					inventory_setter.interactive_open = false;
					inventory_setter.open_close_inventory ();
					return;
				}
				if (is_pressed (settings.cont_moveup, float_mouse_wheel, true)) {
					inventory_setter.move_up_in_inventory ();
					//return;
				}
				if (is_pressed (settings.cont_movedown, float_mouse_wheel, true)) {
					inventory_setter.move_down_in_inventory ();
					//return;
				}
				if (is_pressed (settings.cont_moveleft, float_mouse_wheel, true)) {
					inventory_setter.move_left_in_inventory ();
					//return;
				}
				if (is_pressed (settings.cont_moveright, float_mouse_wheel, true)) {
					inventory_setter.move_right_in_inventory ();
					//return;
				}
				if (is_pressed (settings.cont_secaction, float_mouse_wheel, true)) {
					inventory_setter.select_secondary_action ();
					return;
				}
				if (is_pressed (settings.cont_action, float_mouse_wheel, true)) {
					inventory_setter.select_action ();
					return;
				}


			}



		}


		if (bool_crouching) {
			gameObject.transform.Find ("player_head").gameObject.transform.localPosition = new Vector3 (0f, -0.16f, 0.01f);
			animator_handR.gameObject.transform.localPosition = new Vector3 (0f, -0.16f, animator_handR.gameObject.transform.localPosition.z);
			animator_handL.gameObject.transform.localPosition = new Vector3 (0f, -0.16f, animator_handL.gameObject.transform.localPosition.z);

			animator_player.SetBool ("Crouching",true);

			if (!bool_cutscene) {
				float_move_speed = float_basic_crouching_speed;
			}
		} else {
			gameObject.transform.Find ("player_head").gameObject.transform.localPosition = new Vector3 (0f, 0f, 0.01f);
			animator_handR.gameObject.transform.localPosition = new Vector3 (0f, 0f, animator_handR.gameObject.transform.localPosition.z);
			animator_handL.gameObject.transform.localPosition = new Vector3 (0f, 0f, animator_handL.gameObject.transform.localPosition.z);
		
			animator_player.SetBool ("Crouching",false);

			if (!bool_cutscene) {
				float_move_speed = float_basic_stand_speed;
			}
		}

		blink += Time.deltaTime;
		if (blink > next_blink) {
			blink = 0f;
			next_blink= Random.Range (2f , 8f);
			animator_head.SetBool ("blink",true);
		}

		PreviousX = float_standX;
		PreviousY = float_standY;

	}



	bool is_pressed (string key, float scroll , bool getkeydown) {

		if (key == "ScrollUp" || key == "ScrollDown") {
			if ((key == "ScrollUp" && scroll > 0) || (key == "ScrollDown" && scroll < 0)) {
				return true;
			}

		} else {
			if (getkeydown) {
				if (Input.GetKeyDown ((KeyCode)System.Enum.Parse (typeof(KeyCode), key))) {
					return true;
				}
			} else {
				if (Input.GetKey ((KeyCode)System.Enum.Parse (typeof(KeyCode), key))) {
					return true;
				}
			}

		}
		return false;
	}

	IEnumerator init_close_objective () {
		yield return null;
		settings.objective_opened = open_close_objectives (false);
	}

	public bool open_close_objectives (bool enabled) {
		objective_canvas.enabled = enabled;
		hotbar_canvas.enabled = !enabled;

		return enabled;
	}

}
