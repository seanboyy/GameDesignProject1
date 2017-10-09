﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetText(int score, bool mode){
        if (!mode) text.text = "YOU HAVE DIED\nSCORE: " + score.ToString();
        if (mode) text.text = "YOU WIN\nSCORE: " + score.ToString();
    }
}
