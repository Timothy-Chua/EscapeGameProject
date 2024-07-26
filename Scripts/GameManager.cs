using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;

    [Header("UI - Panels")]
    public GameObject panelInGame;
    public GameObject panelPause;
    public GameObject panelGameOver;
    public GameObject panelObjectives;
    public GameObject panelGameOverMenu;

    [Header("Transitions")]
    public GameObject panelBlink;
    public GameObject panelFade;

    [Header("UI - Text")]
    public TMP_Text txtObjMain;
    public TMP_Text txtObjSub;
    public TMP_Text txtReact;

    public TMP_Text txtResult;

    [Header("Game Settings")]
    public float baseTime;
    private float currentTime;
    public float reactClearDelay = 2.5f;
    
    public bool isIntroComplete;
    public bool isGameOverTriggered;

    [Header("Bomb")]
    public GameObject bombObj;
    public AudioSource bombSound;
    private bool isSoundPlay;

    [Header("Bool - Find Bomb")]
    public bool isFlashlightCollected;
    public bool isBatteryCollected;
    public bool isKeyCRCollected;
    public bool isBombFound;

    [Header("Bool - Defuse")]
    public bool isScissorsCollected;
    public bool isBombDefused;

    [Header("Game Over Settings")]
    public VideoPlayer playerGameOver;
    public VideoClip clipWin;
    public VideoClip clipLose;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        state = GameState.isResume;

        txtObjMain.text = "";
        txtObjSub.text = "";
        txtReact.text = "";

        currentTime = baseTime;
        
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.isResume:
                panelInGame.SetActive(true);
                panelGameOver.SetActive(false);
                panelPause.SetActive(false);

                // checks if the player is in a cutscene
                if (isIntroComplete && !isGameOverTriggered)
                {
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        QuickMenu();
                    }

                    if (!isBombDefused)
                    {
                        currentTime -= Time.deltaTime;

                        if (!isSoundPlay)
                        {
                            bombSound.UnPause();
                            isSoundPlay = true;
                        }

                        // lose condition
                        if (currentTime <= 0)
                        {
                            bombSound.Stop();
                            StartCoroutine(GameOverTrigger());
                        }
                    }
                    else
                    {
                        bombSound.Stop();
                    }

                    // Mission updates
                    if (!isBombFound && !isBombDefused)
                    {
                        txtObjMain.text = "Find the source of the sound and stop it.";
                    }
                    else if (isBombFound && !isBombDefused && !isScissorsCollected)
                    {
                        txtObjMain.text = "Find a way to disarm the bomb.";
                        txtObjSub.text = "Remaining time: " + TimeToString(currentTime);
                    }
                    else if (isBombFound && !isBombDefused && isScissorsCollected)
                    {
                        txtObjMain.text = "Disarm the bomb.";
                        txtObjSub.text = "Remaining time: " + TimeToString(currentTime);
                    }
                    else if (isBombFound && isBombDefused)
                    {
                        txtObjMain.text = "Get out of the house.";
                        txtObjSub.text = "";
                    }

                }

                break;
            case GameState.isPause:
                panelInGame.SetActive(false);
                panelGameOver.SetActive(false);
                panelPause.SetActive(true);

                if (isSoundPlay)
                {
                    bombSound.Pause();
                    isSoundPlay = false;
                }

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    QuickMenu();
                }

                break;
            case GameState.isGameOver:
                panelInGame.SetActive(false);
                panelGameOver.SetActive(true);
                panelPause.SetActive(false);

                break;
        }
    }

    // FUNCTIONS --------------------------------------
    private string TimeToString(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}:{1:00}", minutes, seconds);
        return currentTime;
    }

    public void QuickMenu()
    {
        switch (state)
        {
            case GameState.isResume:
                state = GameState.isPause;
                break;
            case GameState.isPause:
                state = GameState.isResume;
                break;
        }
    }

    private IEnumerator Intro()
    {
        panelObjectives.SetActive(false);
        panelBlink.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        panelBlink.SetActive(false);
        StartCoroutine(QuickReaction("What happened? I don't feel good."));

        yield return new WaitForSeconds(3f);

        bombSound.Play();
        StartCoroutine(QuickReaction("What is that sound, make it stop!"));

        panelObjectives.SetActive(true);
        txtObjMain.text = "Find the source of the sound and stop it.";

        isIntroComplete = true;
    }

    public IEnumerator QuickReaction(string text)
    {
        txtReact.text = text;
        yield return new WaitForSeconds(reactClearDelay);
        txtReact.text = "";
    }

    public IEnumerator GameOverTrigger()
    {
        isGameOverTriggered = true;

        txtReact.text = "";
        panelObjectives.SetActive(false);

        panelFade.SetActive(true);

        yield return new WaitForSeconds(3f);

        state = GameState.isGameOver;
        StartCoroutine(CutsceneGameOver());
    }

    public IEnumerator CutsceneGameOver()
    {
        float delayUI;

        if (isBombDefused)
        {
            playerGameOver.clip = clipWin;
            delayUI = 25f;
        }
        else
        {
            playerGameOver.clip = clipLose;
            delayUI = 5f;
        }

        yield return new WaitForFixedUpdate();

        playerGameOver.Play();

        yield return new WaitForSeconds(delayUI);

        if (isBombDefused)
        {
            txtResult.text = "You escaped!";
        }
        else
        {
            txtResult.text = "The bomb exploded!";
        }

        playerGameOver.SetDirectAudioMute(0, true);
        panelGameOverMenu.SetActive(true);
    }

    // BUTTONS ---------------------------------------
    public void OnBtnResume()
    {
        state = GameState.isResume;
    }

    public void OnBtnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnBtnQuit()
    {
        Application.Quit();
    }

    public void OnBtnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum GameState
{
    isResume,
    isPause,
    isGameOver
}
