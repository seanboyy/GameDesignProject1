using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public GameObject pauseCamera;
    public Camera pauseCameraCamera;
    public Camera mainCamera;
    public PauseCamera pauseCameraScript;
    public AudioSource audioSource;
    public GameButton buttonNear;
    public AudioClip hurtSound;
    public Sprite[] healthBar = new Sprite[11];
    public MainSceneManager sceneManager;
    private Constants constants;
    public Image healthBarImg;
    private float enteredTrigger;
    private System.Int32 frameCount = 0;
    public Text hintText;
    public Text countdownText;
    public Text pause;
    public Button pauseGameExitButton, pauseGameResumeButton;
    private float startTime;
    public bool isPaused = false;

    public int health;

    // Use this for initialization
    void Start(){
        sceneManager = FindObjectOfType<MainSceneManager>();
        constants = FindObjectOfType<Constants>();
        audioSource.clip = hurtSound;
        health = constants.InitialPlayerHealth;
        healthBarImg.sprite = healthBar[health];
    }

    // Update is called once per frame
    void Update(){
        frameCount++;
        if (health < 0){
            healthBarImg.sprite = healthBar[0];
            Destroy(this.gameObject);
            sceneManager.InstantiateDeathCamera();
            health--;
        }
        RegenerateHealth();
        if (frameCount == System.Int32.MaxValue){
            frameCount = 0;
        }
    }

    public void Pause(){
        pause = FindObjectOfType<Text>();
        pause.text = "GAME PAUSED";
        pauseGameExitButton = FindObjectsOfType<Button>()[1];
        pauseGameResumeButton = FindObjectsOfType<Button>()[0];
        pauseGameExitButton.onClick.AddListener(Exit);
        pauseGameResumeButton.onClick.AddListener(Resume);
        isPaused = true;
    }

    private void RegenerateHealth(){
        if (health < constants.InitialPlayerHealth){
            if (frameCount % 30 == 0){
                health++;
                healthBarImg.sprite = healthBar[health];
            }
        }
    }

    void OnTriggerEnter(Collider c){
        GameObject o = c.gameObject as GameObject;
        string tag = c.tag;
        if(tag == "GameButton"){
            buttonNear = o.GetComponent<GameButton>();
        }
    }

    void OnTriggerStay(Collider c){
        GameObject o = c.gameObject as GameObject;
        string tag = o.tag;
        if(tag == "PAIN"){
            if(frameCount % 10 == 0){
                health--;
                audioSource.pitch = Random.Range(0.9F, 1.1F);
                audioSource.volume = 0.3F;
                audioSource.Play();
                healthBarImg.sprite = healthBar[health >= 0 ? health : 0];
            }
        }
        if(tag == "DEATH"){
            if(frameCount % 10 == 0){
                audioSource.pitch = Random.Range(0.9F, 1.1F);
                audioSource.volume = 0.3F;
                audioSource.Play();
                healthBarImg.sprite = healthBar[0];
                health = -1;
            }
        }
        if (tag == "GameButton") {
            if (Input.GetMouseButtonDown(0) && !buttonNear.isPressed) {
                buttonNear.Pressed();
            }
            if (Input.GetMouseButtonUp(0) && buttonNear.isPressed) {
                buttonNear.Released();
            }
        }
        if (tag == "Level1"){
            hintText.text = "Left click to press buttons\nMove close to buttons to click";
        }
        if (tag == "Level2"){
            hintText.text = "Hold left shift to sprint";
        }
        if (tag == "Level3"){
            hintText.text = "Press space to jump\nJump over pits\nIf you fall in, you die";
        }
        if (tag == "Level4"){
            hintText.text = "Your score is based\non how fast you\ncomplete each room";
        }
    }

    public void OnTriggerExit(Collider c){
        GameObject o = c.gameObject as GameObject;
        string tag = o.tag;
        if(tag == "GameButton"){
            if (buttonNear.isPressed) buttonNear.Released();
        }
    }

    void Exit(){
        Application.Quit();
    }

    public void Resume(){
        pauseGameExitButton.onClick.RemoveAllListeners();
        pauseGameResumeButton.onClick.RemoveAllListeners();
        pauseCameraCamera.depth = 70;
        isPaused = false;
    }
}
