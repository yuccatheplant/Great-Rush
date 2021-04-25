using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    public Vector2 direction;
    public float speed;
    public float deadLife;

    private Rigidbody2D bulletRigidbody;
    private float toDie;
	// Use this for initialization
	void Start () {
        bulletRigidbody = this.GetComponent<Rigidbody2D>();
        toDie = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        bulletRigidbody.velocity = new Vector2(direction.x * speed, direction.y * speed);
        toDie += Time.deltaTime;
        if (toDie > deadLife) {
            Destroy(gameObject);
        }
    }
}
