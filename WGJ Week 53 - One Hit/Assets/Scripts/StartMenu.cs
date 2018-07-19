using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using FMODUnity;

public class StartMenu : MonoBehaviour {


    [FMODUnity.EventRef]
    public string buttonHoverSound;


	public GameObject mainMenu, howToPlayMenu, highscoresMenu;
	public GameObject startGameButton, highscoreBackButton, howtoPlayBackbutton;

	EventSystem eS;

	// Use this for initialization
	void Start () {
		eS = FindObjectOfType<EventSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame() {
        SceneManager.LoadScene(1);

    }

    public void QuitGame() {
        Application.Quit();
    }



    public void TestMethod() {
        Debug.Log("Testing!");

    }

    public void PlayHoverSound() {
        FMODUnity.RuntimeManager.PlayOneShot(buttonHoverSound);
    }


	public void BackToMainMenu(){
		mainMenu.SetActive(true);
		highscoresMenu.SetActive(false);
		howToPlayMenu.SetActive(false);

		eS.SetSelectedGameObject(startGameButton);
	}

	public void GoToHighScoresMenu(){
		mainMenu.SetActive(false);
		highscoresMenu.SetActive(true);
		howToPlayMenu.SetActive(false);

		eS.SetSelectedGameObject(highscoreBackButton);

	}

	public void GoToHowToPlayMenu(){
		mainMenu.SetActive(false);
		highscoresMenu.SetActive(false);
		howToPlayMenu.SetActive(true);
        
		eS.SetSelectedGameObject(howtoPlayBackbutton);
	}
}
