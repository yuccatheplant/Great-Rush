using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controler : MonoBehaviour {

	public GameObject target_object;
	private Vector3 target_pos;
	public float camera_speed;

	GameObject follow_obj;

	float deltaX;
	float deltaY;

	void Start() {
		
		follow_obj = GameObject.Find ("camera_target");

		follow_obj.transform.position = target_object.transform.position;
	}

	/*void Update() {
		
	}*/

	void LateUpdate () {
		/*target_pos = new Vector3 (follow_obj.transform.position.x, follow_obj.transform.position.y, gameObject.transform.position.z);
		gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, target_pos, camera_speed * Time.fixedDeltaTime);*/

		if (target_object != null) {
			deltaX = follow_obj.transform.position.x - target_object.transform.position.x;
			deltaY = follow_obj.transform.position.y - target_object.transform.position.y;


			if (deltaX > 0.2f) {
				deltaX = 0.2f;
			} else if (deltaX < -0.2f) {
				deltaX = -0.2f;
			}

			if (deltaY > 0.2f) {
				deltaY = 0.2f;
			} else if (deltaY < -0.2f) {
				deltaY = -0.2f;
			}

			follow_obj.transform.position = new Vector3 (target_object.transform.position.x + deltaX, target_object.transform.position.y + deltaY, 0f);
		}

		gameObject.transform.position = new Vector3 ( follow_obj.transform.position.x, follow_obj.transform.position.y, gameObject.transform.position.z );

	}

	public void change_target (GameObject new_target, float new_speed, bool instantly) {
		target_object = new_target;
		//follow_obj.transform.position = target_object.transform.position;


		if (new_speed != 0f) {
			camera_speed = new_speed;
		}

		if (instantly) {
			gameObject.transform.position = new Vector3 (new_target.transform.position.x, new_target.transform.position.y, gameObject.transform.position.z);
		}
	}
		

}
