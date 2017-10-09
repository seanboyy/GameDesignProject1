using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour {

    public GameObject deathField;
    public GameObject winField;
    private ScoreText scoreText;
    private Button exitButton;
    private AudioSource exitButtonSound;
    public GameObject player;
    public PlayerManager playerManager;
    private Level[] levels;
    private int score;
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
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetMouseButtonDown(0) && !gameOver){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void InstantiatePlayer(){
        GameObject.Instantiate(player);
        player.transform.position = new Vector3(0, 0, 0);
    }

    public void GameOver(){
        Constants.Instance.Score = 0;
        for(int i = 0; i < levels.GetLength(0); i++){
            score += levels[i].score;
        }
    }

    public void InstantiateDeathCamera(){
        GameOver();
        scoreText = deathField.GetComponentInChildren<ScoreText>();
        scoreText.SetText(score, false);
        GameObject.Instantiate(deathField);
        exitButton = FindObjectOfType<Button>();
        exitButton.GetComponentInChildren<Text>().text = "Exit";
        exitButton.onClick.AddListener(ReturnToMenu);
        gameOver = true;
        deathField.transform.position = new Vector3(0, 0, 0);
    }

    public void InstantiateWinCamera(){
        GameOver();
        scoreText = winField.GetComponentInChildren<ScoreText>();
        scoreText.SetText(score, true);
        GameObject.Instantiate(winField);
        exitButton = FindObjectOfType<Button>();
        exitButton.GetComponentInChildren<Text>().text = "Exit";
        exitButton.onClick.AddListener(ReturnToMenu);
        gameOver = true;
        winField.transform.position = new Vector3(0, 0, 0);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene("menu");
    }
}
