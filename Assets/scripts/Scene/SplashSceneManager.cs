using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour {

    float counter = 0;
    private float endSplash = 1.5F;
    public Texture cursorTexture;
    
    // Use this for initializati+on
    void Start () {
        Cursor.SetCursor(cursorTexture as Texture2D, Vector2.zero, CursorMode.Auto);
    }
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if(counter >= endSplash){
            SceneManager.LoadScene("menu");
        }
	}
}
