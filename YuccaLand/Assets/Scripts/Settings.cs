using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Settings : MonoBehaviour {
	public byte language = 1;

	public int int_sound_intensity = 5;
	public int int_music_intensity = 5;


	public string cont_moveup = "W";
	public string cont_movedown = "S";
	public string cont_moveleft = "A";
	public string cont_moveright = "D";
	public string cont_crouch_hold = "C";
	public string cont_crouch_toggle = "X";

	public string cont_action = "Mouse0";
	public string cont_secaction = "Mouse1";
	public string cont_selectguy = "F1";
	public string cont_selectdoc = "F2";
	public string cont_selecttribe = "F3";

	public string cont_objective = "O";
	public string cont_inventory = "E";
	public string cont_drop = "Q";
	public string cont_next = "ScrollDown";
	public string cont_previous = "ScrollUp";
	public string cont_hotbar0 = "BackQuote";
	public string cont_hotbar1 = "Alpha1";
	public string cont_hotbar2 = "Alpha2";
	public string cont_hotbar3 = "Alpha3";
	public string cont_hotbar4 = "Alpha4";
	public string cont_hotbar5 = "Alpha5";
	public string cont_hotbar6 = "Alpha6";
	public string cont_hotbar7 = "Alpha7";
	public string cont_hotbar8 = "Alpha8";


	public bool game_paused = true;
	public bool inventory_opened = false;
	public bool objective_opened = false;
	public bool cutscene_skip = false;
	public bool already_interacting = false;
	public bool hotbar_hidden = false;


	void Awake () {
		DontDestroyOnLoad(GameObject.Find("Settings"));
	}


	public string Key_To_String (string string_key) {
		//Loads string value of key and changes it to human readable values
		switch (string_key) {
		case "BackQuote" : 
			return "~";
		case "Alpha0" : 
			return "0";
		case "Alpha1" : 
			return "1";
		case "Alpha2" : 
			return "2";
		case "Alpha3" : 
			return "3";
		case "Alpha4" : 
			return "4";
		case "Alpha5" : 
			return "5";
		case "Alpha6" : 
			return "6";
		case "Alpha7" : 
			return "7";
		case "Alpha8" : 
			return "8";
		case "Alpha9" : 
			return "9";
		case "Minus" : 
			return "-";
		case "Equals" : 
			return "=";
		case "KeypadDivide" : 
			return "Num /";
		case "KeypadMultiply" : 
			return "Num *";
		case "KeypadMinus" : 
			return "Num -";
		case "KeypadPlus" : 
			return "Num +";
		case "KeypadEnter" : 
			return "Num Enter";
		case "KeypadPeriod" : 
			return "Num .";
		case "Keypad0" : 
			return "Num 0";
		case "Keypad1" : 
			return "Num 1";
		case "Keypad2" : 
			return "Num 2";
		case "Keypad3" : 
			return "Num 3";
		case "Keypad4" : 
			return "Num 4";
		case "Keypad5" : 
			return "Num 5";
		case "Keypad6" : 
			return "Num 6";
		case "Keypad7" : 
			return "Num 7";
		case "Keypad8" : 
			return "Num 8";
		case "Keypad9" : 
			return "Num 9";
		case "UpArrow" : 
			return "Up";
		case "DownArrow" : 
			return "Down";
		case "LeftArrow" : 
			return "Left";
		case "RightArrow" : 
			return "Right";
		case "LeftBracket" : 
			return "[";
		case "RightBracket" : 
			return "]";
		case "Semicolon" : 
			return ";";
		case "Quote" : 
			return "'";
		case "Backslash" : 
			return "\\";
		case "Comma" : 
			return ",";
		case "Period" : 
			return ".";
		case "Slash" : 
			return "/";
		case "CapsLock" : 
			return "Caps Lock";
		case "LeftShift" : 
			return "L Shift";
		case "LeftControl" : 
			return "L Control";
		case "LeftCommand" : 
			return "L Command";
		case "Left Alt" : 
			return "L Alt";
		case "AltGr" : 
			return "R Alt";
		case "RightApple" : 
			return "R Command";
		case "RightControl" : 
			return "R Control";
		case "RightShift" : 
			return "R Shift";

		case "Mouse0" : 
			return "L Mouse";
		case "Mouse1" : 
			return "R Mouse";
		case "Mouse2" : 
			return "M Mouse";
		case "Mouse3" : 
			return "Mouse #3";
		case "Mouse4" : 
			return "Mouse #4";
		case "ScrollUp" : 
			return "Scroll Up";
		case "ScrollDown" : 
			return "Scroll Down";
		
		default:
			return string_key;
		}
	}



}
