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
    public GameObject gameOverScreen, winScreen;
    public GameObject gameOverRestartButton, winRestartButton;
    public TextMeshProUGUI scoreNumText, winScreenScoreNum;
	public GameObject scoreUI;

	public PlayableDirector winTimeline;

    [FMODUnity.EventRef]
    public string stingSound;

    int tempScore = 50;
    public int speedChangeCount;

	bool isGameOver, isWin;

    EventSystem eS;
    LevelMovement lM;
	Animator anim;
	ControllerMOvement bee;
    MusicPlayer mP;

	// Use this for initialization
	void Start () {
        eS = FindObjectOfType<EventSystem>();
        lM = FindObjectOfType<LevelMovement>();
		bee = FindObjectOfType<ControllerMOvement>();
		anim = GetComponent<Animator>();
        mP = FindObjectOfType<MusicPlayer>();
	}
	
	// Update is called once per frame
	void Update () {
        SetNewSpeed();

        scoreNumText.text = score.ToString("F0");
		anim.SetBool("isWin", isWin);

	}

    public void RestartScene() {
        SceneManager.LoadScene(1);
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
        FMODUnity.RuntimeManager.PlayOneShot(stingSound);

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
		float offset = 7f;
        lineRend.SetPosition(0, beePos);
		if(bee.transform.position.x > 0 && bee.transform.position.y > 0){
			winScreen.transform.position = new Vector3(beePos.x - offset, beePos.y - offset, 0);

		} else if(bee.transform.position.x < 0 && bee.transform.position.y > 0){
			winScreen.transform.position = new Vector3(beePos.x + offset, beePos.y - offset, 0);

		} else if(bee.transform.position.x > 0 && bee.transform.position.y < 0){
			winScreen.transform.position = new Vector3(beePos.x - offset, beePos.y + offset, 0);
		} else if(bee.transform.position.x < 0 && bee.transform.position.y < 0){
			winScreen.transform.position = new Vector3(beePos.x + offset, beePos.y + offset, 0);
		}

		lineRend.SetPosition(1, winScreen.transform.position);


	}
}
