using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;// the speed that player will move at;
    Vector3 _movement;//the vector to store the direction of players movement;
    Animator _anim;// Reference to the animator component;
    Rigidbody _playerRb; //Reference to the rigidbody component;
    int _floorMask;//a layer mask so that the ray can be cast just at gameobjects (floor) on the floor layer;
    float camRayLength = 100f;//the lentgh of the ray from the camera to the screen
    void Awake()
    {
        //create a layer mask for the floor layer
        _floorMask = LayerMask.GetMask("Floor");
        //set up references
        _anim = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody>();
    }

    
    void FixedUpdate()
    {
        //store the input axes      
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        //move a player around the scene
        Move(horizontalInput, verticalInput);
        //turn the player around the scene
        Turning();
        Animating(horizontalInput, verticalInput);
    }
    void Move(float h, float v)
    {
        //set the movement vector based on the axis input
        _movement.Set(h, 0f, v);
        //normalise movement vector and make it proportional to the speed per seconds
        _movement = _movement.normalized * speed * Time.deltaTime;
        //move player to its current position plus the movement
        _playerRb.MovePosition(transform.position + _movement);
    }
    void Turning()
    {
        //create a ray from the mouse cursor on screen in the direction of the camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //create aRaycastHit variable to store information about what was hit by the ray
        RaycastHit floorHit;
        //perfprm the raycast and if it hits something on the floor layer...
        if(Physics.Raycast(camRay, out floorHit, camRayLength, _floorMask))
        {
            //create a vector from the player to the point on the floor the raycast from the mouse hit
            Vector3 playerToMouse = floorHit.point - transform.position;
            //ensure the vector is entirely along the floor plane
            playerToMouse.y = 0f;
            //create a quaternion based on looking down the vector from the player to the mouse
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            //set the players rotation to the new rotation
            _playerRb.MoveRotation(newRotation);
        }
    }
    void Animating(float h, float v)
    {
        //create a boolean that is true if either of input axis is not zero
        bool walking = h != 0 || v != 0;
        //tell animator whether or not player is walking
        _anim.SetBool("isWalking", walking);
    }
}
