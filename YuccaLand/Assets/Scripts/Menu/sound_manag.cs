using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class sound_manag : MonoBehaviour {

	Text textui_settings_sound_sound;
	Text textui_settings_sound_music;

	AudioSource audiosource_sound_test;
	AudioSource audiosource_music_test;

	AudioSource audiosource_main_menu;

	AudioListener audiolistener_menu;


	void Awake () {
		textui_settings_sound_sound = GameObject.Find ("Text_button_sound_sound").GetComponent<Text> ();
		textui_settings_sound_music = GameObject.Find ("Text_button_sound_music").GetComponent<Text> ();

		audiosource_sound_test = GameObject.Find ("audiosource_soundtest").GetComponent<AudioSource>();
		audiosource_music_test = GameObject.Find ("audiosource_musictest").GetComponent<AudioSource>();

		audiosource_main_menu = GameObject.Find ("audiosource_main_menu").GetComponent<AudioSource> ();

		audiolistener_menu = GameObject.Find ("Main Camera").GetComponent<AudioListener> ();
	}
	void Start () {
		//On application start loads sound values from INI file
		Sound_Load ();
	}

	void Sound_Load () {
		//Loads INI file
		string path = Application.persistentDataPath+"/sound.ini";	

		if (System.IO.File.Exists (path) && File.ReadAllLines (path).Length >= 3) {
			StreamReader reader = new StreamReader (path);
		
			string string_read_line = reader.ReadLine ();

			//Read Sound value
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
			if (!System.Int32.TryParse (string_parsed_value, out GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity)) {
				GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity = 5;
			} else if (GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity > 10 || GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity < 0) {
				GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity = 5;
			}
				
			//Read Music value
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
			if (!System.Int32.TryParse (string_parsed_value, out GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity)) {
				GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity = 5;
			} else if (GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity > 10 || GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity < 0) {
				GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity = 5;
			}


			reader.Close();
		}

		//Set Sound settings
		audiosource_sound_test.volume = GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity / 10f;
		audiosource_music_test.volume = GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity / 10f;

		audiosource_main_menu.volume = GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity / 10f;

		//Set Sound labels
		if (GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity == 0) {
			textui_settings_sound_sound.text = "Sound: Off";
		} else {
			textui_settings_sound_sound.text = "Sound: "+GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity;
		}

		if (GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity == 0) {
			textui_settings_sound_music.text = "Music: Off";
		} else {
			textui_settings_sound_music.text = "Music: "+GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity;
		}

		if (GameObject.Find("buttons").GetComponent<new_load_game_manag> ()._Check_Menu ()) {
			audiolistener_menu.enabled = true;
		}
	}


	public void _Button_Sound_Sound()
	{
		//Changes Sound intenzity
		if (GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity == 0) {
			GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity = 10;
		} else {
			GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity--;
		}
		if (GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity == 0) {
			textui_settings_sound_sound.text = "Sound: Off";
		} else {
			textui_settings_sound_sound.text = "Sound: " + GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity;
		}

		audiosource_sound_test.volume = GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity / 10f;
	}

	public void _Button_Sound_Music()
	{
		//Changes music intenzity
		if (GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity == 0) {
			GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity = 10;
		} else {
			GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity--;
		}

		if (GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity == 0) {
			textui_settings_sound_music.text = "Music: Off";
		} else {
			textui_settings_sound_music.text = "Music: " + GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity;
		}

		audiosource_music_test.volume = GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity / 10f;

		audiosource_main_menu.volume = GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity / 10f;

	}

	public void _Button_Sound_Test()
	{
		//Starts sound sample
		audiosource_sound_test.volume = GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity / 10f;
		audiosource_music_test.volume = GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity / 10f;

		audiosource_sound_test.Play();
		audiosource_music_test.Play();
	}


	public void _Button_Sound_Back()
	{
		//Saves music values into INI file and Makes Settings screen appear
		audiosource_sound_test.Stop();
		audiosource_music_test.Stop();

		string path = Application.persistentDataPath+"/sound.ini";
		StreamWriter writer = new StreamWriter(path, false);

		if (System.IO.File.Exists (path)) {
			writer.WriteLine("=SOUND=");
			writer.WriteLine("sound=\""+GameObject.Find("Settings").GetComponent<Settings>().int_sound_intensity+"\"");
			writer.WriteLine("music=\""+GameObject.Find("Settings").GetComponent<Settings>().int_music_intensity+"\"");
		}
		writer.Close();


		GetComponent<button_click> ()._Button_Main_Settings();
	}
}
