using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement : MonoBehaviour {
    public float speed = 1f;
    public float newTime;

    public bool hasStarted = false;

    MusicPlayer mP;


	// Use this for initialization
	void Start () {
        mP = FindObjectOfType<MusicPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasStarted = true;
                newTime = Time.time;
                mP.StartMusic();
            }
        }
        else {
            transform.position += Vector3.down * Time.deltaTime * speed;

        }
	}
}
