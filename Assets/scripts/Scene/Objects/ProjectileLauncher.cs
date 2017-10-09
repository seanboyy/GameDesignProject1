using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour {

    public float speed = 0.05F;
    public float arrowExistenceTime = 100.0F;
    private float counter;
    public Arrow arrow;
    public string direction;
    public Quaternion orientation;
    public bool arrowOut = false;
    public Vector3 movement = new Vector3(0, 0, 0);

	void Start () {
        if (direction == "left") orientation.eulerAngles = new Vector3(0, 0, 0);
        if (direction == "right") orientation.eulerAngles = new Vector3(0, 0, 180);
        if (direction == "up") orientation.eulerAngles = new Vector3(0, 0, 270);
        if (direction == "down") orientation.eulerAngles = new Vector3(0, 0, 90);
	}
	
	void Update () {
        counter += Time.deltaTime;
        if(counter >= arrowExistenceTime) {
            counter = 0;
        }
        if (direction == "left") movement = new Vector3(-speed, 0, 0);
        if (direction == "right") movement = new Vector3(speed, 0, 0);
        if (direction == "up") movement = new Vector3(0, speed, 0);
        if (direction == "down") movement = new Vector3(0, -speed, 0);
    }

    public void CreateArrow() {
        arrow.transform.rotation = orientation;
        arrow.transform.position = this.transform.position;
        arrow.movement = this.movement;
        arrow.arrowExistTime = this.arrowExistenceTime;
        GameObject.Instantiate(arrow);
    }
}
