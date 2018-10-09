using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class input_manag : MonoBehaviour {


	//Controls labels
	Text textui_settings_controls_movement_moveup;
	Text textui_settings_controls_movement_movedown;
	Text textui_settings_controls_movement_moveleft;
	Text textui_settings_controls_movement_moveright;

	Text textui_settings_controls_actions_action;
	Text textui_settings_controls_actions_secaction;
	Text textui_settings_controls_actions_selectguy;
	Text textui_settings_controls_actions_selectdoc;
	Text textui_settings_controls_actions_selecttribe;

	Text textui_settings_controls_inventory_inventory;
	Text textui_settings_controls_inventory_drop;
	Text textui_settings_controls_inventory_next;
	Text textui_settings_controls_inventory_previous;
	Text textui_settings_controls_inventory_hotbar0;
	Text textui_settings_controls_inventory_hotbar1;
	Text textui_settings_controls_inventory_hotbar2;
	Text textui_settings_controls_inventory_hotbar3;
	Text textui_settings_controls_inventory_hotbar4;
	Text textui_settings_controls_inventory_hotbar5;
	Text textui_settings_controls_inventory_hotbar6;
	Text textui_settings_controls_inventory_hotbar7;
	Text textui_settings_controls_inventory_hotbar8;

	//Controls values

	string string_old_key;

	int int_key_wait = 0; //0=no;1=moveup;2=movedown;3=moveleft;4=moveright;...

	GameObject Settings;


	void Awake () {
		Settings = GameObject.Find ("Settings");
		
		textui_settings_controls_movement_moveup = GameObject.Find("Text_button_movement_moveup").GetComponent<Text>();
		textui_settings_controls_movement_movedown = GameObject.Find("Text_button_movement_movedown").GetComponent<Text>();
		textui_settings_controls_movement_moveleft = GameObject.Find("Text_button_movement_moveleft").GetComponent<Text>();
		textui_settings_controls_movement_moveright = GameObject.Find("Text_button_movement_moveright").GetComponent<Text>();

		textui_settings_controls_actions_action = GameObject.Find("Text_button_actions_action").GetComponent<Text>();
		textui_settings_controls_actions_secaction = GameObject.Find("Text_button_actions_secaction").GetComponent<Text>();
		textui_settings_controls_actions_selectguy = GameObject.Find("Text_button_actions_guy").GetComponent<Text>();
		textui_settings_controls_actions_selectdoc = GameObject.Find("Text_button_actions_doc").GetComponent<Text>();
		textui_settings_controls_actions_selecttribe = GameObject.Find("Text_button_actions_trib").GetComponent<Text>();

		textui_settings_controls_inventory_inventory = GameObject.Find("Text_button_inventory_inventory").GetComponent<Text>();
		textui_settings_controls_inventory_drop = GameObject.Find("Text_button_inventory_drop").GetComponent<Text>();
		textui_settings_controls_inventory_next = GameObject.Find("Text_button_inventory_next").GetComponent<Text>();
		textui_settings_controls_inventory_previous = GameObject.Find("Text_button_inventory_previous").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar0 = GameObject.Find("Text_button_inventory_select0").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar1 = GameObject.Find("Text_button_inventory_select1").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar2 = GameObject.Find("Text_button_inventory_select2").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar3 = GameObject.Find("Text_button_inventory_select3").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar4 = GameObject.Find("Text_button_inventory_select4").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar5 = GameObject.Find("Text_button_inventory_select5").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar6 = GameObject.Find("Text_button_inventory_select6").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar7 = GameObject.Find("Text_button_inventory_select7").GetComponent<Text>();
		textui_settings_controls_inventory_hotbar8 = GameObject.Find("Text_button_inventory_select8").GetComponent<Text>();
	}
	void Start () {
		//On application start load control
		Controls_Load ();
	}



	void Update() {
		//Every frame check if controls are reasigned and if they are save input to current string
		if (int_key_wait!=0) {

			float float_mouse_wheel = Input.GetAxisRaw("Mouse ScrollWheel");

			if (Input.anyKeyDown || float_mouse_wheel != 0f) {
				string string_pressed_key = "Escape";

				//Detect pressed key

				if (float_mouse_wheel > 0) {
					string_pressed_key = "ScrollUp";
				} else if (float_mouse_wheel < 0) {
					string_pressed_key = "ScrollDown";
				} else {

					foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
						if (Input.GetKey (vKey)) {
							string_pressed_key = vKey.ToString ();
						}
					}
				}

				if (string_pressed_key == "Escape") {
					string_pressed_key = string_old_key;
				}



				switch (int_key_wait) {
				case 1: 
					textui_settings_controls_movement_moveup.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_moveup = string_pressed_key;
					break;
				case 2: 
					textui_settings_controls_movement_movedown.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_movedown = string_pressed_key;
					break;
				case 3: 
					textui_settings_controls_movement_moveleft.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_moveleft = string_pressed_key;
					break;
				case 4: 
					textui_settings_controls_movement_moveright.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_moveright = string_pressed_key;
					break;
				case 5: 
					textui_settings_controls_actions_action.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_action = string_pressed_key;
					break;
				case 6: 
					textui_settings_controls_actions_secaction.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_secaction = string_pressed_key;
					break;
				case 7: 
					textui_settings_controls_actions_selectguy.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_selectguy = string_pressed_key;
					break;
				case 8: 
					textui_settings_controls_actions_selectdoc.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_selectdoc = string_pressed_key;
					break;
				case 9: 
					textui_settings_controls_actions_selecttribe.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_selecttribe = string_pressed_key;
					break;
				case 10: 
					textui_settings_controls_inventory_inventory.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_inventory = string_pressed_key;
					break;
				case 11: 
					textui_settings_controls_inventory_drop.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_drop = string_pressed_key;
					break;
				case 12: 
					textui_settings_controls_inventory_next.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_next = string_pressed_key;
					break;
				case 13: 
					textui_settings_controls_inventory_previous.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_previous = string_pressed_key;
					break;
				case 14:
					textui_settings_controls_inventory_hotbar0.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar0 = string_pressed_key;
					break;
				case 15: 
					textui_settings_controls_inventory_hotbar1.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar1 = string_pressed_key;
					break;
				case 16: 
					textui_settings_controls_inventory_hotbar2.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar2 = string_pressed_key;
					break;
				case 17: 
					textui_settings_controls_inventory_hotbar3.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar3 = string_pressed_key;
					break;
				case 18: 
					textui_settings_controls_inventory_hotbar4.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar4 = string_pressed_key;
					break;
				case 19: 
					textui_settings_controls_inventory_hotbar5.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar5 = string_pressed_key;
					break;
				case 20: 
					textui_settings_controls_inventory_hotbar6.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar6 = string_pressed_key;
					break;
				case 21: 
					textui_settings_controls_inventory_hotbar7.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar7 = string_pressed_key;
					break;
				case 22: 
					textui_settings_controls_inventory_hotbar8.text = Settings.GetComponent<Settings>().Key_To_String (string_pressed_key);
					GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar8 = string_pressed_key;
					break;
				default:
					break;
				}

				StartCoroutine(control_change_delay());
			}
		}
		//KeyCode thisKeyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), "Mouse0") ;

	}	

	IEnumerator control_change_delay()
	{
		//Make sure you cant click anywhere when reasigning keys
		yield return new WaitUntil( () =>  !Input.anyKey );
		//yield return new WaitForSeconds(0.1f);
		int_key_wait = 0;
	}
		
	void Controls_Load () {
		//Load controls from INI file
		string path = Application.persistentDataPath+"/controls.ini";

		if (System.IO.File.Exists (path) && File.ReadAllLines (path).Length >= 23) {
			StreamReader reader = new StreamReader (path);

			string string_read_line = reader.ReadLine ();

			//Read Move Up value
			string_read_line = reader.ReadLine ();
			string string_parsed_value = "";
			bool bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_moveup = string_parsed_value;
			}
			//Read Move Down value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_movedown = string_parsed_value;
			}
			//Read Move Left value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_moveleft = string_parsed_value;
			}
			//Read Move Right value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_moveright = string_parsed_value;
			}


			//Read action value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_action = string_parsed_value;
			}
			//Read secaction value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_secaction = string_parsed_value;
			}
			//Read selectguy value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_selectguy = string_parsed_value;
			}
			//Read selectdoc value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_selectdoc = string_parsed_value;
			}
			//Read selecttribe value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_selecttribe = string_parsed_value;
			}


			//Read inventory value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_inventory = string_parsed_value;
			}
			//Read drop value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_drop = string_parsed_value;
			}
			//Read next value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_next = string_parsed_value;
			}
			//Read previous value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_previous = string_parsed_value;
			}

			//Read hotbar0 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar0 = string_parsed_value;
			}

			//Read hotbar1 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar1 = string_parsed_value;
			}
			//Read hotbar2 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar2 = string_parsed_value;
			}
			//Read hotbar3 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar3 = string_parsed_value;
			}
			//Read hotbar4 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar4 = string_parsed_value;
			}
			//Read hotbar5 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar5 = string_parsed_value;
			}
			//Read hotbar6 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar6 = string_parsed_value;
			}
			//Read hotbar7 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar7 = string_parsed_value;
			}
			//Read hotbar8 value
			string_read_line = reader.ReadLine ();
			string_parsed_value = "";
			bool_can_read = false;
			for (int i = 0; i < string_read_line.Length; i++) {
				if (string_read_line [i] == '"') {
					bool_can_read = true;
				}
				if (bool_can_read && string_read_line [i] != '"') {
					string_parsed_value += string_read_line [i];
				}
			}
			if (Controls_Load_Check (string_parsed_value)) {
				GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar8 = string_parsed_value;
			}

			reader.Close();
		}

		//Set Controls labels
		textui_settings_controls_movement_moveup.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_moveup);
		textui_settings_controls_movement_movedown.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_movedown);
		textui_settings_controls_movement_moveleft.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_moveleft);
		textui_settings_controls_movement_moveright.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_moveright);

		textui_settings_controls_actions_action.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_action);
		textui_settings_controls_actions_secaction.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_secaction);
		textui_settings_controls_actions_selectguy.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_selectguy);
		textui_settings_controls_actions_selectdoc.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_selectdoc);
		textui_settings_controls_actions_selecttribe.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_selecttribe);

		textui_settings_controls_inventory_inventory.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_inventory);
		textui_settings_controls_inventory_drop.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_drop);
		textui_settings_controls_inventory_next.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_next);
		textui_settings_controls_inventory_previous.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_previous);
		textui_settings_controls_inventory_hotbar0.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar0);
		textui_settings_controls_inventory_hotbar1.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar1);
		textui_settings_controls_inventory_hotbar2.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar2);
		textui_settings_controls_inventory_hotbar3.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar3);
		textui_settings_controls_inventory_hotbar4.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar4);
		textui_settings_controls_inventory_hotbar5.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar5);
		textui_settings_controls_inventory_hotbar6.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar6);
		textui_settings_controls_inventory_hotbar7.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar7);
		textui_settings_controls_inventory_hotbar8.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar8);
	}

	bool Controls_Load_Check (string value) {
		//Checks if read value isnt bullshit
		if (value == "Escape") {
			return false;
		}
		if (value == "ScrollUp" || value == "ScrollDown") {
			return true;
		}
		foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode))) {
			if (value == vKey.ToString ()) {
				return true;
			}
		}

		return false;
		
	}


	public void _Button_Movement_moveup()
	{
		if (int_key_wait == 0) {
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_moveup;

			textui_settings_controls_movement_moveup.text = "???";
			int_key_wait = 1;
		}
	}

	public void _Button_Movement_movedown()
	{
		if (int_key_wait == 0) {
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_movedown;
				
			textui_settings_controls_movement_movedown.text="???";
			int_key_wait = 2;
		}
	}

	public void _Button_Movement_moveleft()
	{
		if (int_key_wait == 0) {
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_moveleft;

			textui_settings_controls_movement_moveleft.text="???";
			int_key_wait = 3;
		}
	}

	public void _Button_Movement_moveright()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_moveright;

			textui_settings_controls_movement_moveright.text="???";
			int_key_wait = 4;
		}
	}




	public void _Button_Actions_action()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_action;

			textui_settings_controls_actions_action.text="???";
			int_key_wait = 5;
		}
	}

	public void _Button_Actions_secaction()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_secaction;

			textui_settings_controls_actions_secaction.text="???";
			int_key_wait = 6;
		}
	}

	public void _Button_Actions_selectguy()
	{
		if (int_key_wait == 0) {		
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_selectguy;

			textui_settings_controls_actions_selectguy.text="???";
			int_key_wait = 7;
		}
	}

	public void _Button_Actions_selectdoc()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_selectdoc;

			textui_settings_controls_actions_selectdoc.text="???";
			int_key_wait = 8;
		}
	}

	public void _Button_Actions_selecttribe()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_selecttribe;

			textui_settings_controls_actions_selecttribe.text="???";
			int_key_wait = 9;
		}
	}




	public void _Button_inventory_inventory()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_inventory;

			textui_settings_controls_inventory_inventory.text="???";
			int_key_wait = 10;
		}
	}

	public void _Button_inventory_drop()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_drop;

			textui_settings_controls_inventory_drop.text="???";
			int_key_wait = 11;
		}
	}


	public void _Button_inventory_next()
	{
		if (int_key_wait == 0) {
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_next;

			textui_settings_controls_inventory_next.text="???";
			int_key_wait = 12;
		}
	}


	public void _Button_inventory_previous()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_previous;

			textui_settings_controls_inventory_previous.text="???";
			int_key_wait = 13;
		}
	}

	public void _Button_inventory_hotbar0()
	{
		if (int_key_wait == 0) {		
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar0;

			textui_settings_controls_inventory_hotbar0.text="???";
			int_key_wait = 14;
		}
	}

	public void _Button_inventory_hotbar1()
	{
		if (int_key_wait == 0) {		
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar1;

			textui_settings_controls_inventory_hotbar1.text="???";
			int_key_wait = 15;
		}
	}

	public void _Button_inventory_hotbar2()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar2;

			textui_settings_controls_inventory_hotbar2.text="???";
			int_key_wait = 16;
		}
	}

	public void _Button_inventory_hotbar3()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar3;

			textui_settings_controls_inventory_hotbar3.text="???";
			int_key_wait = 17;
		}
	}

	public void _Button_inventory_hotbar4()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar4;

			textui_settings_controls_inventory_hotbar4.text="???";
			int_key_wait = 18;
		}
	}

	public void _Button_inventory_hotbar5()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar5;

			textui_settings_controls_inventory_hotbar5.text="???";
			int_key_wait = 19;
		}
	}

	public void _Button_inventory_hotbar6()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar6;

			textui_settings_controls_inventory_hotbar6.text="???";
			int_key_wait = 20;
		}
	}

	public void _Button_inventory_hotbar7()
	{
		if (int_key_wait == 0) {	
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar7;

			textui_settings_controls_inventory_hotbar7.text="???";
			int_key_wait = 21;
		}
	}

	public void _Button_inventory_hotbar8()
	{
		if (int_key_wait == 0) {
			string_old_key = GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar8;

			textui_settings_controls_inventory_hotbar8.text="???";
			int_key_wait = 22;
		}
	}

	public void _Button_controls_Back()
	{
		//Saves controls to INI file. makes Settings screen appear
		string path = Application.persistentDataPath+"/controls.ini";
		StreamWriter writer = new StreamWriter(path, false);

		if (System.IO.File.Exists (path)) {
			writer.WriteLine ("=CONTROLS=");
			writer.WriteLine("move_up=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_moveup+"\"");
			writer.WriteLine("move_down=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_movedown+"\"");
			writer.WriteLine("move_left=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_moveleft+"\"");
			writer.WriteLine("move_right=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_moveright+"\"");

			writer.WriteLine("action=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_action+"\"");
			writer.WriteLine("secondary_action=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_secaction+"\"");
			writer.WriteLine("select_guy=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_selectguy+"\"");
			writer.WriteLine("select_doc=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_selectdoc+"\"");
			writer.WriteLine("select_tribe=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_selecttribe+"\"");

			writer.WriteLine("inventory=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_inventory+"\"");
			writer.WriteLine("drop=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_drop+"\"");
			writer.WriteLine("next=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_next+"\"");
			writer.WriteLine("previous=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_previous+"\"");
			writer.WriteLine("hotbar0=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar0+"\"");
			writer.WriteLine("hotbar1=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar1+"\"");
			writer.WriteLine("hotbar2=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar2+"\"");
			writer.WriteLine("hotbar3=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar3+"\"");
			writer.WriteLine("hotbar4=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar4+"\"");
			writer.WriteLine("hotbar5=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar5+"\"");
			writer.WriteLine("hotbar6=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar6+"\"");
			writer.WriteLine("hotbar7=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar7+"\"");
			writer.WriteLine("hotbar8=\""+GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar8+"\"");
		}
		writer.Close();

		GetComponent<button_click> ()._Button_Main_Settings();
	}


	public void _Button_default_yes()
	{
		//Set Controls to default
		GameObject.Find("Settings").GetComponent<Settings>().cont_moveup = "W";
		GameObject.Find("Settings").GetComponent<Settings>().cont_movedown = "S";
		GameObject.Find("Settings").GetComponent<Settings>().cont_moveleft = "A";
		GameObject.Find("Settings").GetComponent<Settings>().cont_moveright = "D";

		GameObject.Find("Settings").GetComponent<Settings>().cont_action = "Mouse0";
		GameObject.Find("Settings").GetComponent<Settings>().cont_secaction = "Mouse1";
		GameObject.Find("Settings").GetComponent<Settings>().cont_selectguy = "F1";
		GameObject.Find("Settings").GetComponent<Settings>().cont_selectdoc = "F2";
		GameObject.Find("Settings").GetComponent<Settings>().cont_selecttribe = "F3";

		GameObject.Find("Settings").GetComponent<Settings>().cont_inventory = "E";
		GameObject.Find("Settings").GetComponent<Settings>().cont_drop = "Q";
		GameObject.Find("Settings").GetComponent<Settings>().cont_next = "ScrollDown";
		GameObject.Find("Settings").GetComponent<Settings>().cont_previous = "ScrollUp";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar0 = "BackQuote";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar1 = "Alpha1";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar2 = "Alpha2";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar3 = "Alpha3";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar4 = "Alpha4";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar5 = "Alpha5";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar6 = "Alpha6";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar7 = "Alpha7";
		GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar8 = "Alpha8";

		//Set Controls labels

		textui_settings_controls_movement_moveup.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_moveup);
		textui_settings_controls_movement_movedown.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_movedown);
		textui_settings_controls_movement_moveleft.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_moveleft);
		textui_settings_controls_movement_moveright.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_moveright);

		textui_settings_controls_actions_action.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_action);
		textui_settings_controls_actions_secaction.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_secaction);
		textui_settings_controls_actions_selectguy.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_selectguy);
		textui_settings_controls_actions_selectdoc.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_selectdoc);
		textui_settings_controls_actions_selecttribe.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_selecttribe);

		textui_settings_controls_inventory_inventory.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_inventory);
		textui_settings_controls_inventory_drop.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_drop);
		textui_settings_controls_inventory_next.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_next);
		textui_settings_controls_inventory_previous.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_previous);
		textui_settings_controls_inventory_hotbar0.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar0);
		textui_settings_controls_inventory_hotbar1.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar1);
		textui_settings_controls_inventory_hotbar2.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar2);
		textui_settings_controls_inventory_hotbar3.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar3);
		textui_settings_controls_inventory_hotbar4.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar4);
		textui_settings_controls_inventory_hotbar5.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar5);
		textui_settings_controls_inventory_hotbar6.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar6);
		textui_settings_controls_inventory_hotbar7.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar7);
		textui_settings_controls_inventory_hotbar8.text = Settings.GetComponent<Settings>().Key_To_String (GameObject.Find("Settings").GetComponent<Settings>().cont_hotbar8);

		//Go back to Controls screen
		GetComponent<button_click> ()._Button_Settings_Controls();
	}		

	public void _Back_Check_Zero () {
		//Makes sure controls are inactive before calling back function
		if (int_key_wait== 0) {
			GetComponent<button_click> ()._Button_Settings_Controls ();
		}
	}



}
