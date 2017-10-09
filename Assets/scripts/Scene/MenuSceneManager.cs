using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneManager : MonoBehaviour {

    public Button button;
    public AudioSource buttonSound;

	// Use this for initialization
	void Start () {
        buttonSound.volume = 0.0F;
        button = FindObjectOfType<Button>();
        button.onClick.AddListener(Begin);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Begin(){
        buttonSound.volume = 1.0F;
        buttonSound.Play();
        SceneManager.LoadScene("main");
    }
}
