using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class dialog_system : MonoBehaviour {

	Settings settings;

	Canvas main_bubble;
	Text person_text;
	Text say_text;
	Image person_icon;

	public Sprite player_neutral, player_happy, player_angry, player_sad;

	public string player_name;

	void Start () {
		settings = GameObject.Find ("Settings").GetComponent<Settings> ();

		main_bubble = gameObject.GetComponent<Canvas> ();
		person_text = GameObject.Find ("text_person").GetComponent<Text>();
		say_text = GameObject.Find ("text_say").GetComponent<Text>();
		person_icon = GameObject.Find ("image_person").GetComponent<Image> ();
	}


	public IEnumerator say_something( string person_name, string said_text, float said_length, Sprite icon, Animator head ) {

		main_bubble.enabled = true;

		if (head != null) {
			head.SetBool ("talking", true);
		}
			
		person_icon.sprite = icon;
		person_text.text = person_name + ":";
		say_text.text = said_text;

		StartCoroutine (animate_text(said_text));

		float current_time = 0f;
		while (current_time < said_length) {
			current_time += Time.deltaTime;

			if (settings.cutscene_skip) {
				break;
			}

			yield return null;
		}

		if (head != null) {
			head.SetBool ("talking", false);
		}
		main_bubble.enabled = false;

	}


	IEnumerator animate_text ( string said_text ) {
		string animated_text = "";

		for (int i = 0; i < said_text.Length; i++) {
			animated_text += said_text [i];
			say_text.text = animated_text;



			float wanted_time = 0.02f;
			float current_time = 0f;
			while (current_time < wanted_time) {
				current_time += Time.deltaTime;

				if (settings.cutscene_skip) {
					break;
				}

				yield return null;
			}
			if (settings.cutscene_skip) {
				break;
			}
			//yield return new WaitForSeconds(0.02f);

		}

	}
	

}
