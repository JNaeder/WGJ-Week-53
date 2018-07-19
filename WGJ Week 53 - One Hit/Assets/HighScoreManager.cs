using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreManager : MonoBehaviour {

	const string privateCode = "bBGGR1Zlw0iTY7YSeRRzMgfhzvEQRaSUu8FSFK6OGT-w";
	const string publicCode = "5b4fdf62191a8a0bcc569947";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;

	public TextMeshProUGUI[] highscoreUserNames;
	public TextMeshProUGUI[] highscoreScores;

    string playerName;

    private void Awake()
    {
        //DownloadHighScores();
        //SetFakeHighScores();
    }


    public void UpdatePlayerName(string newName) {
        playerName = newName;
    }



    public void AddNewHighScore(int score) {
        StartCoroutine(UpLoadNewHighScore(playerName, score));


    }

    public void DownloadHighScores() {

        StartCoroutine("DownloadHighScoresFromServer");

    }

    void SetFakeHighScores() {
       StartCoroutine(UpLoadNewHighScore("Blank1", 500));
        StartCoroutine(UpLoadNewHighScore("Blank2", 500));
        StartCoroutine(UpLoadNewHighScore("Blank3", 500));
        StartCoroutine(UpLoadNewHighScore("Blank4", 500));
        StartCoroutine(UpLoadNewHighScore("Blank5", 500));
        StartCoroutine(UpLoadNewHighScore("Blank6", 250));
        StartCoroutine(UpLoadNewHighScore("Blank7", 250));
        StartCoroutine(UpLoadNewHighScore("Blank8", 250));
        StartCoroutine(UpLoadNewHighScore("Blank9", 250));
        StartCoroutine(UpLoadNewHighScore("Blank10", 250));

    }


    IEnumerator UpLoadNewHighScore(string username, int score) {

        WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Upload Siccessul");
        }
        else {
            Debug.Log("Error Uploading" + www.error);
        }


    }


    IEnumerator DownloadHighScoresFromServer() {
        WWW www = new WWW(webURL + publicCode + "/pipe/");
        yield return www;

        if (string.IsNullOrEmpty(www.error))
        {
            FormatHighScores(www.text);
        }
        else {
            print("Error downloading");
        }


    }


    void FormatHighScores(string textStream) {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i < highscoreUserNames.Length; i++) {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
            //print(highscoresList[i].userName + " : " + highscoresList[i].score);
            highscoreUserNames[i].text = highscoresList[i].userName;
            highscoreScores[i].text = highscoresList[i].score.ToString();
        }

    }
}


public struct Highscore {
    public string userName;
    public int score;

    public Highscore(string _username, int _score) {
        userName = _username;
        score = _score;

    }
}
 
