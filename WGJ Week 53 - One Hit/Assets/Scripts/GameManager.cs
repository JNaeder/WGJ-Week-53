using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {

    public float score;
    public GameObject gameOverScreen, winScreen;
    public GameObject gameOverRestartButton, winRestartButton;
    public TextMeshProUGUI scoreNumText, winScreenScoreNum;

    int tempScore = 50;

    EventSystem eS;
    LevelMovement lM;

	// Use this for initialization
	void Start () {
        eS = FindObjectOfType<EventSystem>();
        lM = FindObjectOfType<LevelMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        SetNewSpeed();

        scoreNumText.text = score.ToString("F0");


	}

    public void RestartScene() {
        SceneManager.LoadScene(0);
    }


    public void GameOver() {
        gameOverScreen.SetActive(true);
        eS.SetSelectedGameObject(gameOverRestartButton);
    }


    public void WinScreen() {
        winScreen.SetActive(true);
        eS.SetSelectedGameObject(winRestartButton);
        winScreenScoreNum.text = score.ToString();

    }

    void SetNewSpeed() {
        if (score >= tempScore) {
            lM.speed++;
            tempScore += tempScore;

        }

    }
}
