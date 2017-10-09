using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    public End end;
    public AudioSource audioSource;
    public ProjectileLauncher launcher;
    public GameButton buttonNear;
    public AudioClip hurtSound;
    public Sprite[] healthBar = new Sprite[11];
    public MainSceneManager sceneManager;
    public Image healthBarImg;
    private float enteredTrigger;
    private System.Int32 frameCount = 0;
    public Text hintText;
    public Text countdownText;
    public Text pause;
    public Button pauseGameExitButton, pauseGameResumeButton;
    private float startTime;
    public bool isPaused = false;

    public int health = Constants.Instance.InitialPlayerHealth;

    // Use this for initialization
    void Start(){
        end = FindObjectOfType<End>();
        sceneManager = FindObjectOfType<MainSceneManager>();
        audioSource.clip = hurtSound;
        health = Constants.Instance.InitialPlayerHealth;
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
    
    private void RegenerateHealth(){
        if (health < Constants.Instance.InitialPlayerHealth){
            if (frameCount % 100 == 0){
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
        if (tag == "ProjectileLauncher") {
            launcher = o.GetComponent<ProjectileLauncher>();
            launcher.CreateArrow();
        }
        if (tag == "Arrow") { 
            health -= 2;
            audioSource.pitch = Random.Range(0.9F, 1.1F);
            audioSource.volume = 0.3F;
            audioSource.Play();
            healthBarImg.sprite = healthBar[health >= 0 ? health : 0];
        }
        if(tag == "Pedestal"){
            end.RemoveMacGuffin();
            Destroy(this.gameObject);
            sceneManager.InstantiateWinCamera();
        }
    }

    void OnTriggerStay(Collider c) {
        GameObject o = c.gameObject as GameObject;
        string tag = o.tag;
        if (tag == "PAIN") {
            if (frameCount % 10 == 0) {
                health--;
                audioSource.pitch = Random.Range(0.9F, 1.1F);
                audioSource.volume = 0.3F;
                audioSource.Play();
                healthBarImg.sprite = healthBar[health >= 0 ? health : 0];
            }
        }
        if (tag == "DEATH") {
            if (frameCount % 10 == 0) {
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
        if (tag == "Level1") {
            hintText.text = "Left click to press buttons\nMove close to buttons to click";
        }
        if (tag == "Level2") {
            hintText.text = "Hold left shift to sprint";
        }
        if (tag == "Level3") {
            hintText.text = "Press space to jump\nJump over pits\nIf you fall in, you die";
        }
        if (tag == "Level4") {
            hintText.text = "Your score is based\non how fast you\ncomplete each room";
        }
        if (tag == "Level5") {
            hintText.text = "Watch out for arrows\nThey deal two damage";
        }
        if (tag == "Level6") {
            hintText.text = "";
        }
    }

    public void OnTriggerExit(Collider c){
        GameObject o = c.gameObject as GameObject;
        string tag = o.tag;
        if(tag == "GameButton"){
            if (buttonNear.isPressed) buttonNear.Released();
        }
    }
}
