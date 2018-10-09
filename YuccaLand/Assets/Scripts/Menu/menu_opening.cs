using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menu_opening : MonoBehaviour {

	Image image_opening;
	new_load_game_manag manag;

	Image company_logo;
	Text presents_text;

	void Start () {
		image_opening = gameObject.GetComponent<Image> ();
		manag = GameObject.Find ("buttons").GetComponent<new_load_game_manag> ();

		company_logo = GameObject.Find ("company_logo").GetComponent<Image> ();
		presents_text = GameObject.Find ("presents_text").GetComponent<Text> ();


		image_opening.color = new Color(1f,1f,1f,1f);
		company_logo.color = new Color(1f,1f,1f,0f);
		presents_text.color = new Color(1f,1f,1f,0f);

		if (!manag._Check_Menu ()) {
			StartCoroutine (fade_in (image_opening, 1f, false));
		} else {
			StartCoroutine (startup_opening ());
		}

	}
		
	IEnumerator fade_in (Image image,  float wanted_time, bool skipable) {

		float current_time = 0f;

		float one_divided_by_wanted_time = -1f / wanted_time;

		image.color = new Color(1f,1f,1f,1f);

		while ( current_time < wanted_time ) {
			if (skipable && Input.anyKeyDown) {
				break;
			}
			yield return null;

			image.color = new Color(1f,1f,1f, one_divided_by_wanted_time * current_time + 1f );

			current_time += Time.unscaledDeltaTime;

		}

		image.color = new Color(1f,1f,1f,0f);

		yield return null;
	}



	IEnumerator fade_out (Image image,  float wanted_time, bool skipable) {

		float current_time = 0f;

		float one_divided_by_wanted_time = 1f / wanted_time;

		image.color = new Color(1f,1f,1f,0f);

		while ( current_time < wanted_time ) {

			if (skipable && Input.anyKeyDown) {
				break;
			}

			yield return null;

			image.color = new Color(1f,1f,1f, one_divided_by_wanted_time * current_time );

			current_time += Time.unscaledDeltaTime;

		}

		image.color = new Color(1f,1f,1f,1f);

		yield return null;
	}





	IEnumerator startup_opening () {
		float wanted_time = 3f;
		float current_time = 0f;

		while ( current_time < wanted_time ) {

			if (Input.anyKeyDown) {
				break;
			}

			yield return null;

			current_time += Time.unscaledDeltaTime;

		}


		StartCoroutine (fade_out ( company_logo, 1f, true));
		//StartCoroutine (fade_out ( presents_text, 2f));

		wanted_time = 5f;
		current_time = 0f;
		while ( current_time < wanted_time ) {
			if (Input.anyKeyDown) {
				break;
			}
			yield return null;
			current_time += Time.unscaledDeltaTime;
		}



		StartCoroutine (fade_in ( company_logo, 1f, true));

		wanted_time = 2f;
		current_time = 0f;
		while ( current_time < wanted_time ) {
			if (Input.anyKeyDown) {
				break;
			}
			yield return null;
			current_time += Time.unscaledDeltaTime;
		}

		StartCoroutine (fade_in ( image_opening, 2f, false));


		yield return null;
	}

}
