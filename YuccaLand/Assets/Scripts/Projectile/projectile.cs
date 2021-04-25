using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    public Vector2 direction;
    public float speed;
    public float deadLife;
    public float maxCliffDistance = 2;

    private Rigidbody2D bulletRigidbody;
    private float toDie;

    private Vector2 startPos;
    private Vector2 cliffPos;
    private Vector2 cliffHeights;
    int hittableStatus = 100; //-100:No, //0:Cliff, //100:Yes

    // Use this for initialization
    void Start () {
        startPos = new Vector2(transform.position.x, transform.position.y);
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

    private void hitNPC(ref Collider2D hit)
    {
        Destroy(hit.gameObject);
        Destroy(this.gameObject);
    }

    private bool cliffGoUphill() {

        //Y
        int x = 0;
        int y = 0;

        if (cliffHeights.y > 0.01f || cliffHeights.y < -0.01f)
        {
            if ((direction.y > 0.01f && cliffHeights.y > 0.01f) || (direction.y < -0.01f && cliffHeights.y < -0.01f))
            {
                y = 1;
                //Shooting uphill                   
            }
            else if ((direction.y > 0.01f && cliffHeights.y < -0.01f) || (direction.y < -0.01f && cliffHeights.y > 0.01f))
            {
                y = -1;
                //Shooting downhill
            }
        }

        //X
        if (cliffHeights.x > 0.01f || cliffHeights.x < -0.01f)
        {
            if ((direction.x > 0.01f && cliffHeights.x > 0.01f) || (direction.x < -0.01f && cliffHeights.x < -0.01f))
            {
                x = 1;
                //Shooting uphill                   
            }
            else if ((direction.y > 0.01f && cliffHeights.y < -0.01f) || (direction.y < -0.01f && cliffHeights.y > 0.01f))
            {
                x = -1;
                //Shooting downhill
            }
        }

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            return x > 0;
        }
        return y > 0;

    }

    private void cliffDetermineHit(ref Collider2D hit) {
        bool upHill = cliffGoUphill();

        if ((upHill && Vector2.Distance(transform.position, cliffPos) < maxCliffDistance) || 
            !upHill && (Vector2.Distance(startPos, cliffPos) < maxCliffDistance))
        {
            hitNPC(ref hit);
        }
        else
        {
            hittableStatus = -100;
        }
    }

    private void cliffDetermineSecond(ref Collider2D hit)
    {
        bool up1 = cliffGoUphill();

        cliffPos = new Vector2(this.transform.position.x, this.transform.position.y);
        cliffHeights = hit.GetComponent<Cliff_settings>().heights;

        if (!up1 && cliffGoUphill())
        {
            hittableStatus = 100;
        }
        else
        {
            hittableStatus = -100;
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        switch (hit.transform.gameObject.layer) {
            case 12: //NPC
                switch (hittableStatus)
                {
                    case 100:
                        hitNPC(ref hit);
                        break;
                    case 0:
                        cliffDetermineHit(ref hit);
                        break;
                    default:
                        break;
                }
                break;
            case 11: //cliff
                switch (hittableStatus) {
                    case 100:
                        hittableStatus = 0;
                        cliffPos = new Vector2(this.transform.position.x, this.transform.position.y);
                        cliffHeights = hit.GetComponent<Cliff_settings>().heights;
                        break;
                    case 0:
                        cliffDetermineSecond(ref hit);
                        break;
                }
                break;
            default: //Other
                break;
        }
    }
}
