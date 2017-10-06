using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCamera : MonoBehaviour {

    public Light dirLight;
    
    //public Camera playerCamera;
    //public Camera pauseCameraCamera;

    // Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update(){

    }

    public void Create(MainSceneManager sceneManager){
        dirLight.intensity = 1.0F;
    }

    public void Remove(){
        dirLight.intensity = 0.0F;
    }
}
