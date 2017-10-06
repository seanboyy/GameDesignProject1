using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject doorComponent;
    public AudioSource audioSource;
    public bool isOpen = false;
    public string orientation = "";

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.0F;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Open(){
        if (orientation == "up") doorComponent.transform.position = new Vector3(doorComponent.transform.position.x, doorComponent.transform.position.y + 5, doorComponent.transform.position.z);
        if (orientation == "down") doorComponent.transform.position = new Vector3(doorComponent.transform.position.x, doorComponent.transform.position.y - 5, doorComponent.transform.position.z);
        if (orientation == "left") doorComponent.transform.position = new Vector3(doorComponent.transform.position.x + 5, doorComponent.transform.position.y, doorComponent.transform.position.z);
        if (orientation == "right") doorComponent.transform.position = new Vector3(doorComponent.transform.position.x - 5, doorComponent.transform.position.y, doorComponent.transform.position.z);
        audioSource.volume = 1.0F;
        audioSource.Play();
        isOpen = true;
    }

    public void Close(){
        if (orientation == "up") doorComponent.transform.position = new Vector3(doorComponent.transform.position.x, doorComponent.transform.position.y - 5, doorComponent.transform.position.z);
        if (orientation == "down") doorComponent.transform.position = new Vector3(doorComponent.transform.position.x, doorComponent.transform.position.y + 5, doorComponent.transform.position.z);
        if (orientation == "left") doorComponent.transform.position = new Vector3(doorComponent.transform.position.x - 5, doorComponent.transform.position.y, doorComponent.transform.position.z);
        if (orientation == "right") doorComponent.transform.position = new Vector3(doorComponent.transform.position.x + 5, doorComponent.transform.position.y, doorComponent.transform.position.z);
        audioSource.volume = 1.0F;
        audioSource.Play();
        isOpen = false;
    }
}
