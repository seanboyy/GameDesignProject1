using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float counter;
    public float arrowExistTime;
    public Vector3 movement;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime;
        if (counter >= arrowExistTime) {
            Destroy(gameObject);
            counter = 0;
        }
        transform.position += movement * Time.deltaTime;
    }
}
