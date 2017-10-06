using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour {

    public GameObject player;
    public GameObject deathCamera;
    public GameObject pauseCamera;
    public PauseCamera pauseCameraScript;
    public ScoreText score;
    public Text pause;
    private Text gameOverExitButtonText, pauseGameExitButtonText;
    private Button gameOverExitButton, pauseGameExitButton;
    public Constants constants;
    public PlayerManager playerManager;
    private Level[] levels;
    //private float startTime;
    public bool isPaused = false;
    private bool gameOver = false;

    // Use this for initialization
    void Start(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        levels = FindObjectsOfType<Level>();
        //startTime = Time.time;
        InstantiatePlayer();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown("escape") || gameOver){
            if (!playerManager.isPaused) playerManager.Pause();
            else playerManager.Resume();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButtonDown(0) && !gameOver && !isPaused){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void TogglePause(bool isPaused){
        if (!isPaused){
            pauseCameraScript.Create(this);
            Time.timeScale = 0;
            this.isPaused = true;
            return;
        }
        if (isPaused){
            pauseCameraScript.Remove();
            Time.timeScale = 1;
            this.isPaused = false;
            return;
        }
    }

    void InstantiatePlayer(){
        GameObject.Instantiate(player);
        player.transform.position = new Vector3(0F, 0F, 0F);
    }

    public void InstantiatePauseCamera(){
        pause = FindObjectOfType<Text>();
        pause.text = "GAME PAUSED";
        pauseGameExitButton = FindObjectOfType<Button>();
        pauseGameExitButton.onClick.AddListener(Exit);
        pauseGameExitButtonText = pauseGameExitButton.GetComponentInChildren<Text>();
        pauseGameExitButtonText.text = "Exit";
        isPaused = true;

    }

    public void InstantiateDeathCamera(){
        GameObject.Instantiate(deathCamera);
        deathCamera.transform.position = new Vector3(0F, 0F, -10F);
        score = FindObjectOfType<ScoreText>();
        //score.SetText((int)(Time.time - startTime) / 3);
        int scoreVal = 0;
        for(int i = 0; i < levels.GetLength(0); i++){
            scoreVal += levels[i].score;
        }
        score.SetText(scoreVal);
        gameOverExitButton = FindObjectOfType<Button>();
        gameOverExitButton.onClick.AddListener(Exit);
        gameOverExitButtonText = gameOverExitButton.GetComponentInChildren<Text>();
        gameOverExitButtonText.text = "Exit";
        gameOver = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Exit(){
        Application.Quit();
    }
}
