using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ControllerMOvement : MonoBehaviour {
    
    public float speed = 1f;
    public float attackspeed;
    public float xClamp, yClamp;
    public bool isAttacking, isDead;
    public float scoreDecreaseSpeed;

    [FMODUnity.EventRef]
    public string flowerPickUpSound, pointsGoDpwnSound;

    FMOD.Studio.EventInstance pointsGoingDown;

    bool isGameOver;

    LevelMovement lM;
    GameManager gM;
    Animator anim;
    Camera cam;

	[HideInInspector]
	public LineRenderer lineRend;

	// Use this for initialization
	void Start () {
        lM = FindObjectOfType<LevelMovement>();
        gM = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
		lineRend = GetComponent<LineRenderer>();
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
		anim.SetBool("isDead", isDead);
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
		} else {
			float h = Input.GetAxis("Horizontal");
            transform.position += new Vector3(h, 0, 0) * Time.deltaTime * speed;

		}

    }
    void DecreasingScore() {
        if (isGameOver) {
            if (gM.score > 0) {
                gM.score -= Time.deltaTime * scoreDecreaseSpeed;
                pointsGoingDown = FMODUnity.RuntimeManager.CreateInstance(pointsGoDpwnSound);
                pointsGoingDown.start();
                pointsGoingDown.release();

			} else if(gM.score < 0){
				gM.score = 0;
			}

        }

    }

    void GameOver() {
		isDead = true;
        lM.speed = 0;
        attackspeed = 0;
        speed = 0;
		scoreDecreaseSpeed = gM.score / 2;
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
			//Destroy(collision.gameObject);
			Flower flower = collision.gameObject.GetComponent<Flower>();
			flower.PlayParticle();
            gM.score += 10;
            FMODUnity.RuntimeManager.PlayOneShot(flowerPickUpSound);
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
