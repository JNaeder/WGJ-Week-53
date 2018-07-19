using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class LevelMovement : MonoBehaviour {
    public float speed = 1f;
    public float newTime;

    public bool hasStarted = false;
	public GameObject pressSpaceToStartText;

    [FMODUnity.EventRef]
    public string startGameSound;

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
                FMODUnity.RuntimeManager.PlayOneShot(startGameSound);
                hasStarted = true;
                newTime = Time.time;
                mP.StartMusic();
				pressSpaceToStartText.SetActive(false);
            }
        }
        else {
            transform.position += Vector3.down * Time.deltaTime * speed;

        }
	}
}
