using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class event_manager_loader : MonoBehaviour {

	//GameObject player;

	void Awake () {
		//player = GameObject.Find ("player");
		/*if (player) {
			gameObject.transform.SetParent(player.transform);
		}*/

		EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
		if (sceneEventSystem == null)
		{
			GameObject eventSystem = new GameObject("EventSystem");
			eventSystem.AddComponent<EventSystem>();
			eventSystem.AddComponent<StandaloneInputModule>();
		}

		if (!GameObject.Find ("Settings")) {
			GameObject Settings = new GameObject ("Settings");
			Settings.AddComponent<Settings> ();
		}
	}

	/*void Update () {
		gameObject.transform.position = player.transform.position;
	}*/
}
