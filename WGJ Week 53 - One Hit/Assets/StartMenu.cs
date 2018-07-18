using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;

public class StartMenu : MonoBehaviour {


    [FMODUnity.EventRef]
    public string buttonHoverSound;

	// Use this for initialization
	void Start () {
		
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
}
