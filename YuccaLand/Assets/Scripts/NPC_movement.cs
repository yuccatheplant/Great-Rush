using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_movement : MonoBehaviour {

	public float walk_speed;

	public float standX = 0f;
	public float standY = -1f;
	public float walkX = 0f;
	public float walkY = 0f;
	public bool walking = false;

	public GameObject npc_watch_target;

	Animator animator;
	Animator head;

	float blink = 0f;
	float next_blink;

	void Start () {
		animator = gameObject.GetComponent<Animator> ();

		head = gameObject.transform.Find ("npc_head").gameObject.GetComponent<Animator>();

		next_blink= Random.Range (3f , 7f);
	}

	void Update () {
		if (npc_watch_target) {
			standX = npc_watch_target.transform.position.x - gameObject.transform.position.x;
			standY = npc_watch_target.transform.position.y - gameObject.transform.position.y;
		}


		animator.SetFloat ("walkX", walkX);
		animator.SetFloat ("walkY", walkY);
		animator.SetFloat ("standX", standX);
		animator.SetFloat ("standY", standY);
		animator.SetBool ("walking", walking);

		head.SetFloat ("LastX", standX);
		head.SetFloat ("LastY", standY);
		head.SetBool ("blink",false);

		if (walking) {
			head.SetFloat ("LastX", walkX);
			head.SetFloat ("LastY", walkY);

			gameObject.transform.Translate (walkX * walk_speed * Time.deltaTime, walkY * walk_speed * Time.deltaTime, 0f);
		}

		blink += Time.deltaTime;
		if (blink > next_blink) {
			blink = 0f;
			next_blink= Random.Range (2f , 8f);
			head.SetBool ("blink",true);
		}

	}
}
