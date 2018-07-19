using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MusicPlayer : MonoBehaviour {

    [FMODUnity.EventRef]
    public string musicEvent, ambienceEvent;

    FMOD.Studio.EventInstance musicInst, ambienceInst;

    LevelMovement lM;
    GameManager gM;

    public static MusicPlayer musicplayer;

    bool musicStarted;

    private void Awake()
    {
        if (musicplayer == null)
        {
            musicplayer = this;
        }
        else {
            Destroy(gameObject);

        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);


        

        ambienceInst = FMODUnity.RuntimeManager.CreateInstance(ambienceEvent);
        ambienceInst.start();
    }

    // Update is called once per frame
    void Update()
    {   if (musicStarted)
        {
            {
                musicInst.setParameterValue("SpeedChangeCount", gM.speedChangeCount);
            }
        }
    }


    public void StopMusic() {
        musicInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        musicInst.release();

    }

    public void StartMusic() {
        lM = FindObjectOfType<LevelMovement>();
        gM = FindObjectOfType<GameManager>();
        musicInst = FMODUnity.RuntimeManager.CreateInstance(musicEvent);
        musicInst.start();
        musicStarted = true;

    }
}
