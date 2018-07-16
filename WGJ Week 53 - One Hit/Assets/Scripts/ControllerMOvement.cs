using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMOvement : MonoBehaviour {
    
    public float speed = 1f;
    public float attackspeed;
    public float xClamp, yClamp;
    public bool isAttacking;
    public float scoreDecreaseSpeed;

    bool isGameOver;

    LevelMovement lM;
    GameManager gM;
    Animator anim;
    Camera cam;

	// Use this for initialization
	void Start () {
        lM = FindObjectOfType<LevelMovement>();
        gM = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        cam = Camera.main;

        Vector3 camScreen = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        xClamp = camScreen.x;
        yClamp = camScreen.y - 1;
	}
	
	// Update is called once per frame
	void Update () {
            Movement();
            ScreenCrossOver();
            DecreasingScore();

        anim.SetBool("isAttacking", isAttacking);
	}



    void Movement() {
        if (lM.hasStarted)
        {
            if (!isAttacking)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");
                transform.position += new Vector3(h, v, 0) * Time.deltaTime * speed;

                //float newX = Mathf.Clamp(transform.position.x, -xClamp, xClamp);
                   float newY = Mathf.Clamp(transform.position.y, -yClamp, yClamp);
                    transform.position = new Vector3(transform.position.x, newY, 0);

                if(Time.time > lM.newTime + 1f)
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        isAttacking = true;
                    }
            }
            else
            {
                lM.speed = 0;
                transform.position += Vector3.up * attackspeed * Time.deltaTime;


            }
        }

    }
    void DecreasingScore() {
        if (isGameOver) {
            if (gM.score > 0) {
                gM.score -= Time.deltaTime * scoreDecreaseSpeed;

            }

        }

    }

    void GameOver() {
        lM.speed = 0;
        attackspeed = 0;
        speed = 0;
        isGameOver = true;
        gM.GameOver();
    }

    void ScreenCrossOver() {
        
        if (transform.position.x > xClamp)
        {
            Vector3 newPos = new Vector3(-xClamp, transform.position.y, 0);
            transform.position = newPos;
        }
        else if (transform.position.x < -xClamp) {

            Vector3 newPos = new Vector3(xClamp, transform.position.y, 0);
            transform.position = newPos;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Coin") {
            Destroy(collision.gameObject);
            gM.score += 10;
            Debug.Log("Get Coin!");
        }
        if (collision.gameObject.tag == "Piece") {
            GameOver();
        }
        if (collision.gameObject.tag == "Person") {
            if (isAttacking)
            {
                Person person = collision.gameObject.GetComponent<Person>();
                person.GetAttacked();
                gM.WinScreen();
                lM.speed = 0;
                attackspeed = 0;
                speed = 0;
            }
            else {
                GameOver();
            }
        }
    }
}
