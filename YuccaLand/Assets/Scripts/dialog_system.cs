using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class dialog_system : MonoBehaviour {

	public string person_name = "";
	public string said_text = "";

	Settings settings;

	Canvas main_bubble;
	Text person_text;
	Text say_text;
	Image person_icon;

	public Sprite[] player_portrait;

	public string player_name;

	void Start () {
		settings = GameObject.Find ("Settings").GetComponent<Settings> ();

		main_bubble = gameObject.GetComponent<Canvas> ();
		person_text = GameObject.Find ("text_person").GetComponent<Text>();
		say_text = GameObject.Find ("text_say").GetComponent<Text>();
		person_icon = GameObject.Find ("image_person").GetComponent<Image> ();

		main_bubble.enabled = false;
	}


	public IEnumerator say_something( string person_name, string said_text_eng, string said_text_cze, float whole_limit, Sprite icon, Animator head ) {

		main_bubble.enabled = true;

		if (head != null) {
			head.SetBool ("talking", true);
		}

		person_icon.sprite = icon;
		person_text.text = person_name + ":";
		say_text.text = said_text_eng;

		string animated_text_eng = "";
		string animated_text_cze = "";

		int text_length = said_text_eng.Length;
		if (said_text_cze.Length > text_length) {
			text_length = said_text_cze.Length;
		}

		float animation_time = 0f;
		for (int i = 0; i < text_length; i++) {
			if (i < said_text_eng.Length) {
				animated_text_eng += said_text_eng [i];
			}
			if (i < said_text_cze.Length) {
				animated_text_cze += said_text_cze [i];
			}

			switch (settings.language) {
			case 1:
				say_text.text = animated_text_cze;
				break;
			default:
				say_text.text = animated_text_eng;
				break;
			}

			float section_limit = 0.02f;
			float section_timer = 0f;
			while (section_timer < section_limit) {
				section_timer += Time.deltaTime;
				animation_time += Time.deltaTime;

				if (settings.cutscene_skip) {
					break;
				}

				yield return null;
			}
			if (settings.cutscene_skip) {
				break;
			}

		}


		while (animation_time < whole_limit) {
			animation_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			switch (settings.language) {
			case 1:
				say_text.text = said_text_cze;
				break;
			default:
				say_text.text = said_text_eng;
				break;
			}

			yield return null;
		}

		if (head != null) {
			head.SetBool ("talking", false);
		}
		main_bubble.enabled = false;

	}

	public IEnumerator say_somethingV2( float whole_limit, Sprite icon, Animator head ) {
		yield return null;

		main_bubble.enabled = true;


		if (head != null) {
			head.SetBool ("talking", true);
		}

		person_icon.sprite = icon;
		person_text.text = person_name + ":";
		say_text.text = said_text;



		//int text_length = 1;

		float animation_time = 0f;


		for (int i = 0; i < said_text.Length; i++) {
			string animated_text = "";

			for (int n = 0; n <= i; n++) {
				animated_text += said_text [n];
			}
			person_text.text = person_name + ":";
			say_text.text = animated_text;

			float section_limit = 0.02f;
			float section_timer = 0f;
			while (section_timer < section_limit) {
				section_timer += Time.deltaTime;
				animation_time += Time.deltaTime;

				if (settings.cutscene_skip) {
					break;
				}

				yield return null;
			}
			if (settings.cutscene_skip) {
				break;
			}

		}


		while (animation_time < whole_limit) {
			animation_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}


			person_text.text = person_name + ":";
			say_text.text = said_text;



			yield return null;
		}

		if (head != null) {
			head.SetBool ("talking", false);
		}
		main_bubble.enabled = false;

	}
		
}
