using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

    public int score;
    public GameButton button1, button2;
    public Door door;
    public Sprite[] time;
    public Image countdownText;
    public Sprite empty;
    public float startTime, nowTime;
    public bool button1pressed = false, button2pressed = false;
    public int timeUntilFail;
    public bool timeLimitExceeded = false;
    public bool isSolved = false;

	// Use this for initialization
	void Start() {

	}
	
	// Update is called once per frame
	void Update () {
        CheckSolve();
        nowTime = button1pressed || button2pressed ? Time.time : startTime;
        UpdateTimer();
	}

    private void UpdateTimer(){
        if ((button1pressed || button2pressed) && !timeLimitExceeded && !isSolved){
            countdownText.sprite = time[(this.timeUntilFail - ((int)(this.nowTime - this.startTime)))];
        }
        else{
            countdownText.sprite = empty;
        }
    }

    void CheckSolve(){
        if (button1.isPressed && !button1pressed && !button2pressed && !isSolved){
            timeLimitExceeded = false;
            startTime = Time.time;
            button1pressed = true;
        }
        else if(button2.isPressed && !button2pressed && !button1pressed && !isSolved){
            timeLimitExceeded = false;
            startTime = Time.time;
            button2pressed = true;
        }
        else if(button1.isPressed && button2pressed && !isSolved){
            if (nowTime - startTime <= timeUntilFail){
                if(!door.isOpen) door.Open();
                isSolved = true;
                score += timeUntilFail - (int)(nowTime - startTime);
            }
        }
        else if(button2.isPressed && button1pressed && !isSolved){
            if (nowTime - startTime <= timeUntilFail){
                if (!door.isOpen) door.Open();
                isSolved = true;
                score += timeUntilFail - (int)(nowTime - startTime);
            }
        }
        if(nowTime - startTime > timeUntilFail){
            button1pressed = false;
            button2pressed = false;
            timeLimitExceeded = true;
        }
    }
}
