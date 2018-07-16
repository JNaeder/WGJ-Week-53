using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMovement : MonoBehaviour {
    public float speed = 1f;
    public float newTime;

    public bool hasStarted = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasStarted = true;
                newTime = Time.time;
            }
        }
        else {
            transform.position += Vector3.down * Time.deltaTime * speed;

        }
	}
}
