using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour {

    AudioSource audioSource;
    public GameObject buttonCube;
    public bool isPressed = false;
    public string orientation;

	// Use this for initialization
	void Start () {
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.volume = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pressed(){
        if (orientation == "up") buttonCube.transform.position = new Vector3(buttonCube.transform.position.x, buttonCube.transform.position.y + 0.125F, buttonCube.transform.position.z);
        if (orientation == "right") buttonCube.transform.position = new Vector3(buttonCube.transform.position.x + 0.125F, buttonCube.transform.position.y, buttonCube.transform.position.z);
        if (orientation == "down") buttonCube.transform.position = new Vector3(buttonCube.transform.position.x, buttonCube.transform.position.y - 0.125F, buttonCube.transform.position.z);
        if (orientation == "left") buttonCube.transform.position = new Vector3(buttonCube.transform.position.x - 0.125F, buttonCube.transform.position.y, buttonCube.transform.position.z);
        audioSource.volume = 1;
        audioSource.Play();
        isPressed = true;
    }

    public void Released(){
        if (orientation == "up") buttonCube.transform.position = new Vector3(buttonCube.transform.position.x, buttonCube.transform.position.y - 0.125F, buttonCube.transform.position.z);
        if (orientation == "right") buttonCube.transform.position = new Vector3(buttonCube.transform.position.x - 0.125F, buttonCube.transform.position.y, buttonCube.transform.position.z);
        if (orientation == "down") buttonCube.transform.position = new Vector3(buttonCube.transform.position.x, buttonCube.transform.position.y + 0.125F, buttonCube.transform.position.z);
        if (orientation == "left") buttonCube.transform.position = new Vector3(buttonCube.transform.position.x + 0.125F, buttonCube.transform.position.y, buttonCube.transform.position.z);
        audioSource.volume = 1;
        isPressed = false;
    }
}
