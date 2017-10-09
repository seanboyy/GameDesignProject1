using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for defining the movement of the player
/// </summary>
public class PlayerMove : MonoBehaviour{
    /// <summary>
    /// Source of Footfall sounds
    /// </summary>
    private AudioSource footfall;
    /// <summary>
    /// boolean that determines whether a sound should play
    /// </summary>
    private bool shouldPlaySound = true;
    /// <summary>
    /// list of different footstep sounds
    /// </summary>
    public AudioClip[] footfallsounds = new AudioClip[3];
    /// <summary>
    /// player movement speed modifier
    /// </summary>
    public float speed = 3.0F;
    /// <summary>
    /// gravity modifier
    /// </summary>
    public float gravity = 0.98F;
    /// <summary>
    /// Character controller for moving and collisions
    /// </summary>
    private CharacterController characterController;
    /// <summary>
    /// Sprite renderer for character
    /// </summary>
    private SpriteRenderer characterRenderer;
    /// <summary>
    /// Sprite renderer for shadow
    /// </summary>
    private SpriteRenderer shadowRenderer;
    /// <summary>
    /// Sprite representing standing looking left
    /// </summary>
    public Sprite stillLeft;
    /// <summary>
    /// Sprite representing walking looking left
    /// </summary>
    public Sprite walkingLeft;
    /// <summary>
    /// Sprite representing standing looking right
    /// </summary>
    public Sprite stillRight;
    /// <summary>
    /// Sprite representing walking looking right
    /// </summary>
    public Sprite walkingRight;
    /// <summary>
    /// jump height modifier
    /// </summary>
    public float jumpHeight = 5.0F;
    /// <summary>
    /// how fast should the sprite switch between walking and standing
    /// </summary>
    private float spriteToggle = 0.2F;
    /// <summary>
    /// How much does sprinting change the speed
    /// </summary>
    public float sprintMultiplier = 2;
    /// <summary>
    /// frame time counter
    /// </summary>
    private float counter = 0;
    /// <summary>
    /// movement vector 3 dimension
    /// </summary>
    private Vector3 movement = new Vector3(0, 0, 0);
    /// <summary>
    /// movement vector 2 dimension
    /// </summary>
    private Vector2 move2 = new Vector2(0, 0);
    /// <summary>
    /// boolean for if the player is touching the floor
    /// </summary>
    public bool isOnFloor;
    /// <summary>
    /// boolean for if the player has initiated a jump and it is not yet complete
    /// </summary>
    private bool jumping;
    /// <summary>
    /// boolean for if the player has begun to fall
    /// </summary>
    private bool falling;

    /// <summary>
    /// Method called upon object creation
    /// </summary>
    void Start(){
        //find the footfall source component
        footfall = this.GetComponent<AudioSource>();
        //grab the character controller
        characterController = this.GetComponent<CharacterController>();
        //grab the character and shadow renderers from children
        characterRenderer = GetComponentsInChildren<SpriteRenderer>()[1];
        shadowRenderer = GetComponentsInChildren<SpriteRenderer>()[0];
        //set the sprite to standing looking right
        characterRenderer.sprite = stillRight;
    }

    /// <summary>
    /// Method called every frame
    /// </summary>
    void Update(){
        //add the frame time to the counter
        counter += Time.deltaTime;
        //create two floats
        float deltaX, deltaY;
        //check if sprinting
        if (!Input.GetKey(KeyCode.LeftShift)){
            //not sprinting: grab relevant movement based on keys pressed
            deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        }
        else{
            //sprinting: grab relevant movement based on keys pressed and scale with sprint
            deltaX = Input.GetAxis("Horizontal") * speed * sprintMultiplier * Time.deltaTime;
            deltaY = Input.GetAxis("Vertical") * speed * sprintMultiplier * Time.deltaTime;
        }
        //assign move2
        move2 = new Vector2(deltaX, deltaY);
        //clamp to speed
        //move2 = Vector2.ClampMagnitude(movement, speed);
        //assign movement
        movement = new Vector3(move2.x, move2.y, 0);
        //check if jump key is pressed and if on floor. if so, jump
        if (Input.GetKeyDown("space") && isOnFloor && !jumping && !falling){
            //move the sprite up
            characterRenderer.gameObject.transform.position = new Vector3(characterRenderer.gameObject.transform.position.x, characterRenderer.gameObject.transform.position.y + 1F, characterRenderer.gameObject.transform.position.z);
            movement.z = -jumpHeight;
            jumping = true;
        }
        //check if not on floor. if so, apply gravity continuously
        else if (!isOnFloor){
            falling = true;
            //gradually move the sprite down
            characterRenderer.gameObject.transform.position = new Vector3(characterRenderer.gameObject.transform.position.x, characterRenderer.gameObject.transform.position.y - 0.02F, characterRenderer.gameObject.transform.position.z);
            movement.z += gravity;
        }
        //check if on floor. if so, set movement z to be 0
        else if (isOnFloor && jumping && falling){
            //make sure the sprite is on the floor (relative to where the shadow is)
            if (characterRenderer.gameObject.transform.position.y != shadowRenderer.gameObject.transform.position.y) characterRenderer.gameObject.transform.position = new Vector3(characterRenderer.gameObject.transform.position.x, shadowRenderer.gameObject.transform.position.y + 1.6424F, characterRenderer.gameObject.transform.position.z);
            movement.z = 0;
            jumping = false;
            falling = false;
        }

        //move the controller
        characterController.Move(movement);
        //animate movement
        AnimateWalk(Input.GetKey(KeyCode.LeftShift) ? spriteToggle / sprintMultiplier : spriteToggle);
    }

    /// <summary>
    /// Animation of movement
    /// toggles between two frames
    /// </summary>
    /// <param name="spriteToggle">Speed at which sprite changes</param>
    private void AnimateWalk(float spriteToggle){
        //check if sprite should toggle
        if (counter >= spriteToggle){
            //check if a movement key is pressed
            if (Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") || Input.GetKey("w")){
                //toggle between if sound should play or not
                if (shouldPlaySound){
                    //call sound play
                    PlayFootStepSound();
                    shouldPlaySound = !shouldPlaySound;
                }
                else{
                    shouldPlaySound = !shouldPlaySound;
                }
                if (Input.GetKey("w") || Input.GetKey("s")){
                    if (Input.GetKey("a")){
                        if (characterRenderer.sprite == stillRight) characterRenderer.sprite = stillLeft;
                        else if (characterRenderer.sprite == walkingRight) characterRenderer.sprite = walkingLeft;
                        if (characterRenderer.sprite == stillLeft){
                            characterRenderer.sprite = walkingLeft;
                        }
                        else if (characterRenderer.sprite == walkingLeft){
                            characterRenderer.sprite = stillLeft;
                        }
                    }
                    if (Input.GetKey("d")){
                        if (characterRenderer.sprite == stillLeft) characterRenderer.sprite = stillRight;
                        if (characterRenderer.sprite == walkingLeft) characterRenderer.sprite = walkingRight;
                        if (characterRenderer.sprite == stillRight){
                            characterRenderer.sprite = walkingRight;
                        }
                        else if (characterRenderer.sprite == walkingRight){
                            characterRenderer.sprite = stillRight;
                        }
                    }
                    if (characterRenderer.sprite == stillLeft){
                        characterRenderer.sprite = walkingLeft;
                    }
                    else if (characterRenderer.sprite == walkingLeft){
                        characterRenderer.sprite = stillLeft;
                    }
                    if (characterRenderer.sprite == stillRight){
                        characterRenderer.sprite = walkingRight;
                    }
                    else if (characterRenderer.sprite == walkingRight){
                        characterRenderer.sprite = stillRight;
                    }
                }
                if (Input.GetKey("a"))
                {
                    if (characterRenderer.sprite == stillRight) characterRenderer.sprite = stillLeft;
                    else if (characterRenderer.sprite == walkingRight) characterRenderer.sprite = walkingLeft;
                    if (characterRenderer.sprite == stillLeft){
                        characterRenderer.sprite = walkingLeft;
                    }
                    else if (characterRenderer.sprite == walkingLeft){
                        characterRenderer.sprite = stillLeft;
                    }
                }
                if (Input.GetKey("d"))
                {
                    if (characterRenderer.sprite == stillLeft) characterRenderer.sprite = stillRight;
                    if (characterRenderer.sprite == walkingLeft) characterRenderer.sprite = walkingRight;
                    if (characterRenderer.sprite == stillRight){
                        characterRenderer.sprite = walkingRight;
                    }
                    else if (characterRenderer.sprite == walkingRight){
                        characterRenderer.sprite = stillRight;
                    }
                }
            }
            else{
                if (characterRenderer.sprite == walkingRight) characterRenderer.sprite = stillRight;
                if (characterRenderer.sprite == walkingLeft) characterRenderer.sprite = stillLeft;
            }
            counter = 0;
        }
    }

    /// <summary>
    /// Play sound on footfall
    /// </summary>
    private void PlayFootStepSound(){
        footfall.clip = footfallsounds[Random.Range(0, 2)];
        if (!footfall.isPlaying) footfall.Play();
    }
    
    #region Triggers
    public void OnTriggerEnter(Collider c){
        GameObject o = c.gameObject as GameObject;
        string tag = o.tag;
        if (tag == "Floor") isOnFloor = true;
    }

    public void OnTriggerStay(Collider c){
        GameObject o = c.gameObject as GameObject;
        string tag = o.tag;
        if (tag == "Floor") isOnFloor = true;
    }

    public void OnTriggerExit(Collider c){
        GameObject o = c.gameObject as GameObject;
        string tag = o.tag;
        if (tag == "Floor") isOnFloor = false;
    }
    #endregion
}
