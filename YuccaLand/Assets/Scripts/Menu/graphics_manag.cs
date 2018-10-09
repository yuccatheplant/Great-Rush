using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class graphics_manag : MonoBehaviour {

	Text textui_main_caption;//Main caption on the screen

	Text textui_settings_graphics_fullscreen;
	Text textui_settings_graphics_resolution;
	Text textui_settings_graphics_vsync;

	public float float_resolution_height = 500;//Default == 500

	int int_width_memory=0;
	int int_height_memory=0;

	int int_resolutions_type = 0;

	void Awake () {
		textui_settings_graphics_fullscreen = GameObject.Find("Text_button_graphics_fullscreen").GetComponent<Text>();
		textui_settings_graphics_resolution = GameObject.Find("Text_button_graphics_resolution").GetComponent<Text>();
		textui_settings_graphics_vsync = GameObject.Find("Text_button_graphics_vsync").GetComponent<Text>();
	}

	void Start() {
		//On Application start load graphics settings from INI file
		if (GameObject.Find("buttons").GetComponent<new_load_game_manag> ()._Check_Menu ()) {
			Graphics_Load ();
		} else {
			Graphics_Load_InGame ();
		}
	}	

	void Update () {
		//Every frame check if resolution was changed and if it was respond by GUI scale update
		if (Screen.width != int_width_memory || Screen.height != int_height_memory) {

			textui_settings_graphics_resolution.text = "Resolution: " + Screen.width + " x " + Screen.height;

			int_width_memory = Screen.width;
			int_height_memory = Screen.height;

			/*
			GameObject main_caption = GameObject.Find("caption_main");
			main_caption.transform.localScale = new Vector3( (int_height_memory/float_resolution_height) , (int_height_memory/float_resolution_height), 1);
			main_caption.transform.localPosition= new Vector3(0, 0f+(int_height_memory/2.5f), 0);

			GameObject main_caption_version = GameObject.Find("caption_version");

			main_caption_version.transform.localScale = new Vector3( (int_height_memory/float_resolution_height) , (int_height_memory/float_resolution_height), 1);
			main_caption_version.transform.localPosition= new Vector3(0 - (int_width_memory/2f) + ((RectTransform)main_caption_version.transform).rect.width/2f*main_caption_version.transform.localScale.x, 0 - (int_height_memory/2f) + ((RectTransform)main_caption_version.transform).rect.height/2f *main_caption_version.transform.localScale.y, 0);

			transform.localScale = new Vector3( (int_height_memory/float_resolution_height) , (int_height_memory/float_resolution_height), 1);
			transform.localPosition= new Vector3(0, 0f-(int_height_memory/5f), 0);
			*/
			/*
			GameObject background = GameObject.Find("background");

			if (int_width_memory / 16f > int_height_memory / 9f) {
				background.transform.localScale = new Vector3 (int_width_memory/1920f ,int_width_memory/1920f , 1);
			} else {
				background.transform.localScale = new Vector3 (int_height_memory/1080f ,int_height_memory/1080f , 1);
			}
			*/
				
		}
	}

	void Graphics_Load () {
		//Loads graphics settings from INI file.
		bool bool_fullscreen_memory = true;
		int int_width_memory = Display.main.systemWidth;
		int int_height_memory = Display.main.systemHeight;
		int int_vsync_memory = 1;

		string path = Application.persistentDataPath+"/graphics.ini";

		if (System.IO.File.Exists (path) && File.ReadAllLines(path).Length >= 5) {
			StreamReader reader = new StreamReader (path);

			string string_read_line = reader.ReadLine ();

			//Read Fullscreen value
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
			if (string_parsed_value == "0") {
				bool_fullscreen_memory = false;
			} else {
				bool_fullscreen_memory = true;
			}

			//Read Width value
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
			string_read_line = reader.ReadLine ();
			if (System.Int32.TryParse (string_parsed_value, out int_width_memory)) {
				//Width value parsed sucessfully.
				//Read Height value
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
				if (!System.Int32.TryParse (string_parsed_value, out int_height_memory)) {
					int_width_memory = Display.main.systemWidth;
					int_height_memory = Display.main.systemHeight;
				}

			} else {
				int_width_memory = Display.main.systemWidth;
				int_height_memory = Display.main.systemHeight;
			}

			//Read Vsync value
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

			if (int_width_memory < 0 || int_height_memory < 0 || int_width_memory > Display.main.systemWidth || int_height_memory > Display.main.systemHeight) {
				int_width_memory = Display.main.systemWidth;
				int_height_memory = Display.main.systemHeight;
			}

			if (!System.Int32.TryParse (string_parsed_value, out int_vsync_memory)) {
				int_vsync_memory = 1;
			} else if (int_vsync_memory > 2 || int_vsync_memory < 0) {
				int_vsync_memory = 1;
			}
		
			reader.Close();
		}

		//Set Graphics settings
		Screen.SetResolution(int_width_memory, int_height_memory, bool_fullscreen_memory);
		Screen.fullScreen = bool_fullscreen_memory;
		QualitySettings.vSyncCount = int_vsync_memory;

		//Set Graphics labels
		textui_settings_graphics_resolution.text = "Resolution: " + int_width_memory + " x " + int_height_memory;

		if (bool_fullscreen_memory) {
			textui_settings_graphics_fullscreen.text = "Fullscreen: On";
		} else {
			textui_settings_graphics_fullscreen.text = "Fullscreen: Off";
		}

		switch (int_vsync_memory) {
		case 0:	
			textui_settings_graphics_vsync.text = "vSync: Off";
			break;
		case 1:	
			textui_settings_graphics_vsync.text = "vSync: On";
			break;
		default:
			textui_settings_graphics_vsync.text= "vSync: Half";
			break;
		}


	}

	void Graphics_Load_InGame () {
		//Load Graphics labels from game - In case that graphics changed during game somehow
		bool bool_fullscreen_memory = Screen.fullScreen;
		int int_width_memory = Screen.width;
		int int_height_memory = Screen.height;
		int int_vsync_memory = QualitySettings.vSyncCount;

		//Set Graphics labels
		textui_settings_graphics_resolution.text = "Resolution: " + int_width_memory + " x " + int_height_memory;

		if (bool_fullscreen_memory) {
			textui_settings_graphics_fullscreen.text = "Fullscreen: On";
		} else {
			textui_settings_graphics_fullscreen.text = "Fullscreen: Off";
		}

		switch (int_vsync_memory) {
		case 0:	
			textui_settings_graphics_vsync.text = "vSync: Off";
			break;
		case 1:	
			textui_settings_graphics_vsync.text = "vSync: On";
			break;
		default:
			textui_settings_graphics_vsync.text= "vSync: Half";
			break;
		}
	}


	public void _Button_Graphics_Fullscreen()
	{
		//Toggles fullscreen mode
		Screen.fullScreen = !Screen.fullScreen;

		if (!Screen.fullScreen) {
			textui_settings_graphics_fullscreen.text = "Fullscreen: On";

			int_resolutions_type = Screen.resolutions.Length - 1;

			while (Screen.resolutions[int_resolutions_type].width > Display.main.systemWidth || Screen.resolutions[int_resolutions_type].height > Display.main.systemHeight) {
				if (int_resolutions_type == 0) {
					int_resolutions_type = Screen.resolutions.Length;
				}
				int_resolutions_type--;
			} 


			Screen.SetResolution(Screen.resolutions[int_resolutions_type].width, Screen.resolutions[int_resolutions_type].height, !Screen.fullScreen);
		} else {
			textui_settings_graphics_fullscreen.text = "Fullscreen: Off";
		}
	}


	public void _Button_Graphics_Resolution()
	{
		//Toggles resolution of application
		do {
			if (int_resolutions_type == 0) {
				int_resolutions_type = Screen.resolutions.Length;
			}
			int_resolutions_type--;
		} while (Screen.resolutions[int_resolutions_type].width > Display.main.systemWidth || Screen.resolutions[int_resolutions_type].height > Display.main.systemHeight || ( Screen.resolutions [int_resolutions_type].width == Screen.width && Screen.resolutions [int_resolutions_type].height == Screen.height && int_resolutions_type!=0 ) );
		/*
		if (Screen.resolutions [int_resolutions_type].width == Screen.width && Screen.resolutions [int_resolutions_type].height == Screen.height && int_resolutions_type!=0) {
			int_resolutions_type--;
		}*/

		Screen.SetResolution(Screen.resolutions[int_resolutions_type].width, Screen.resolutions[int_resolutions_type].height, Screen.fullScreen);

		//}
	}

	public void _Button_Graphics_VSync()
	{
		//Toggles vsync value of application

		if (QualitySettings.vSyncCount >= 2) {
			QualitySettings.vSyncCount = 0;
		} else {
			QualitySettings.vSyncCount++;
		}

		switch (QualitySettings.vSyncCount) {
		case 0:	
			textui_settings_graphics_vsync.text = "vSync: Off";
			break;
		case 1:	
			textui_settings_graphics_vsync.text = "vSync: On";
			break;
		default:
			textui_settings_graphics_vsync.text= "vSync: Half";
			break;
		}
	}

	public void _Button_graphics_Back()
	{
		//Saves Graphics values in INI file and leaves to Settings screen
		string path = Application.persistentDataPath+"/graphics.ini";
		StreamWriter writer = new StreamWriter(path, false);

		if (System.IO.File.Exists (path)) {
			writer.WriteLine ("=GRAPHICS=");
			if (Screen.fullScreen) {
				writer.WriteLine ("fullscreen=\"1\"");
			} else {
				writer.WriteLine ("fullscreen=\"0\"");
			}
			writer.WriteLine ("resolution_width=\"" + Screen.width + "\"");
			writer.WriteLine ("resolution_height=\"" + Screen.height + "\"");
			writer.WriteLine ("vsync=\"" + QualitySettings.vSyncCount + "\"");
		}
		writer.Close();


		GetComponent<button_click> ()._Button_Main_Settings();
	}

}
