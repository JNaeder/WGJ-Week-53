using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using FMODUnity;

public class GameManager : MonoBehaviour {

    public float score;
    public GameObject gameOverScreen, winScreen, highscoreScreen;
    public GameObject gameOverRestartButton, winRestartButton, highscoreBackButton;
    public TextMeshProUGUI scoreNumText, winScreenScoreNum;
	public GameObject scoreUI;


	public PlayableDirector winTimeline;
	public Transform[] winScreenPos;

    [FMODUnity.EventRef]
    public string stingSound, menuButtonSound;

	FMOD.Studio.EventInstance stingInst;

    int tempScore = 50;
    public int speedChangeCount;

	public bool isGameOver, isWin;

    EventSystem eS;
    LevelMovement lM;
	Animator anim;
	ControllerMOvement bee;
    MusicPlayer mP;
	Camera cam;
	HighScoreManager hSM;

	// Use this for initialization
	void Start () {
        eS = FindObjectOfType<EventSystem>();
        lM = FindObjectOfType<LevelMovement>();
		bee = FindObjectOfType<ControllerMOvement>();
		anim = GetComponent<Animator>();
        mP = FindObjectOfType<MusicPlayer>();
		cam = Camera.main;
		hSM = GetComponent<HighScoreManager>();

        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        SetNewSpeed();

        scoreNumText.text = score.ToString("F0");
		anim.SetBool("isWin", isWin);

	}

    public void RestartScene() {
		Time.timeScale = 1;
        SceneManager.LoadScene(1);
		mP.StopMusic();
		eS.SetSelectedGameObject(null);
		stingInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		stingInst.release();
    }

	public void SubmitHighScore(){
		hSM.AddNewHighScore((int)score);

	}


    public void GameOver() {
        gameOverScreen.SetActive(true);
        eS.SetSelectedGameObject(gameOverRestartButton);
		isGameOver = true;
        mP.StopMusic();

    }


	public void WinScreen(){
		winTimeline.Play();
		scoreUI.SetActive(false);
		SetWinScreenPosition();      
        eS.SetSelectedGameObject(winRestartButton);
        winScreenScoreNum.text = score.ToString();
		isWin = true;
        mP.StopMusic();
		stingInst = FMODUnity.RuntimeManager.CreateInstance(stingSound);
		stingInst.start();

    }

    void SetNewSpeed() {
        if (score >= tempScore) {
            lM.speed++;
            speedChangeCount++;
            tempScore += tempScore;

        }

    }


	void SetWinScreenPosition(){
		Vector3 beePos = bee.transform.position;
		LineRenderer lineRend = bee.lineRend;
		lineRend.enabled = true;
		float offset = 5f;
		Vector3 screenSize= cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
        lineRend.SetPosition(0, beePos);
        //Top Right
		if(bee.transform.position.x > 0 && bee.transform.position.y > 0){
			//winScreen.transform.position = new Vector3(beePos.x - screenSize.x, beePos.y - screenSize.y, 0);
			winScreen.transform.position = winScreenPos[2].position;
        //Top Left
		} else if(bee.transform.position.x < 0 && bee.transform.position.y > 0){
			//winScreen.transform.position = new Vector3(beePos.x + screenSize.x, beePos.y - screenSize.y, 0);
			winScreen.transform.position = winScreenPos[3].position;
        // Bottom Right
		} else if(bee.transform.position.x > 0 && bee.transform.position.y < 0){
			//winScreen.transform.position = new Vector3(beePos.x - screenSize.x, beePos.y + screenSize.y, 0);
			winScreen.transform.position = winScreenPos[0].position;
        // Bottom Left
		} else if(bee.transform.position.x < 0 && bee.transform.position.y < 0){
			//winScreen.transform.position = new Vector3(beePos.x + screenSize.x, beePos.y + screenSize.y, 0);
			winScreen.transform.position = winScreenPos[1].position;
		}

		lineRend.SetPosition(1, winScreen.transform.position);


	}

	public void ShowHighScoreScreem(){
		highscoreScreen.transform.position = winScreen.transform.position;
		winScreen.SetActive(false);
		highscoreScreen.SetActive(true);
		eS.SetSelectedGameObject(highscoreBackButton);
	}

	public void BackToWinScreen(){
		winScreen.SetActive(true);
		highscoreScreen.SetActive(false);
		eS.SetSelectedGameObject(winRestartButton);

	}

	public void PlayMenuButtonSound(){

		FMODUnity.RuntimeManager.PlayOneShot(menuButtonSound);
	}

	public void QuitGame(){
		Application.Quit();

	}

	public void BackToMainMenu(){
		Time.timeScale = 1;
		SceneManager.LoadScene(0);
		mP.StopMusic();
		stingInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        stingInst.release();
	}
}
