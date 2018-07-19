using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseManager : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject resumeButton;

	EventSystem eS;

	bool isPaused;

	// Use this for initialization
	void Start () {
		eS = FindObjectOfType<EventSystem>();
	}
	
	// Update is called once per frame
	void Update () {


		if(Input.GetKeyDown(KeyCode.Escape)){
			isPaused = !isPaused;

			if(isPaused){
				Pause();            
			} else {
				UnPause();            
			}         
		}
	}



	public void Pause(){
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
		eS.SetSelectedGameObject(resumeButton);
	}

	public void UnPause(){
		isPaused = false;
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
		eS.SetSelectedGameObject(null);

	}
}
