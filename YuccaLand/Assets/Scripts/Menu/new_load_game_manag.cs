using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class new_load_game_manag : MonoBehaviour {

	//Text UI which tells player if current save slot is occupied
	Text textui_newgame_story_slot1;
	Text textui_newgame_story_slot2;
	Text textui_newgame_story_slot3;
	Text textui_newgame_story_slot4;
	Text textui_newgame_story_slot5;
	Text textui_newgame_story_slot6;

	//Boolean which defines if current save slot is occupied
	public bool bool_newgame_slot1_empty = true;
	public bool bool_newgame_slot2_empty = true;
	public bool bool_newgame_slot3_empty = true;
	public bool bool_newgame_slot4_empty = true;
	public bool bool_newgame_slot5_empty = true;
	public bool bool_newgame_slot6_empty = true;

	void Awake () {
		textui_newgame_story_slot1 = GameObject.Find("Text_button_new_story_slot1").GetComponent<Text>();
		textui_newgame_story_slot2 = GameObject.Find("Text_button_new_story_slot2").GetComponent<Text>();
		textui_newgame_story_slot3 = GameObject.Find("Text_button_new_story_slot3").GetComponent<Text>();
		textui_newgame_story_slot4 = GameObject.Find("Text_button_new_story_slot4").GetComponent<Text>();
		textui_newgame_story_slot5 = GameObject.Find("Text_button_new_story_slot5").GetComponent<Text>();
		textui_newgame_story_slot6 = GameObject.Find("Text_button_new_story_slot6").GetComponent<Text>();
	}


	public void _Start_Tutorial () {
		//Starts Tutorial

		Time.timeScale = 1;

		SceneManager.LoadScene ("tutorial");
		//SceneManager.SetActiveScene (SceneManager.GetSceneByName ("tutorial"));

		//Close_Scenes();
	}

	void Close_Scenes () {
		SceneManager.UnloadSceneAsync ("Menu");
	}


	public void _New_Game_Story_Slots_Init () {
		//This function checks if save games are present. If they are, textui writes it is occupied
		//NOTE: This function does NOT PARSE SAVE FILES!
		string path = Application.persistentDataPath + "/Saves/Story/";
		if (!Directory.Exists ( path )) {
			Directory.CreateDirectory( path );
		}
			
		if (File.Exists (path+"StorySlot1.dat")) {
			bool_newgame_slot1_empty = false;
			textui_newgame_story_slot1.text = "Occupied";
		} else {
			bool_newgame_slot1_empty = true;
			textui_newgame_story_slot1.text = "Empty";
		}

		if (File.Exists (path+"StorySlot2.dat")) {
			bool_newgame_slot2_empty = false;
			textui_newgame_story_slot2.text = "Occupied";
		} else {
			bool_newgame_slot2_empty = true;
			textui_newgame_story_slot2.text = "Empty";
		}

		if (File.Exists (path+"StorySlot3.dat")) {
			bool_newgame_slot3_empty = false;
			textui_newgame_story_slot3.text = "Occupied";
		} else {
			bool_newgame_slot3_empty = true;
			textui_newgame_story_slot3.text = "Empty";
		}

		if (File.Exists (path+"StorySlot4.dat")) {
			bool_newgame_slot4_empty = false;
			textui_newgame_story_slot4.text = "Occupied";
		} else {
			bool_newgame_slot4_empty = true;
			textui_newgame_story_slot4.text = "Empty";
		}

		if (File.Exists (path+"StorySlot5.dat")) {
			bool_newgame_slot5_empty = false;
			textui_newgame_story_slot5.text = "Occupied";
		} else {
			bool_newgame_slot5_empty = true;
			textui_newgame_story_slot5.text = "Empty";
		}

		if (File.Exists (path+"StorySlot6.dat")) {
			bool_newgame_slot6_empty = false;
			textui_newgame_story_slot6.text = "Occupied";
		} else {
			bool_newgame_slot6_empty = true;
			textui_newgame_story_slot6.text = "Empty";
		}
					
	}

	public void _Story_NewGame_Start (byte byte_slot_number) {
		//This function creates save file
		//TODO
		string path = Application.persistentDataPath + "/Saves/Story/StorySlot"+byte_slot_number+".dat";
		StreamWriter writer = new StreamWriter(path, false);

		writer.WriteLine ("Moje prvni ulozena hra. hehe.");

		writer.Close();
	}

	public bool _Check_Menu () {
		if (SceneManager.GetActiveScene ().name == "Menu") {
			return true;
		}
		return false;
	}

	public void _Unload_Menu () {
		SceneManager.UnloadSceneAsync ("Menu");
	}
}
