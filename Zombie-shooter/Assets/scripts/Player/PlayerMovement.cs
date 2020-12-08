using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 _movement;
    Animator _anim;
    Rigidbody _playerRb;
    int _floorMask;
    float camReyLength = 100f;
    void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        _anim = GetComponent<Animator>();
        _playerRb = GetComponent<Rigidbody>();
    }

    
    void fixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Move(horizontalInput, verticalInput);
        Turning();
    }
    void Move(float h, float v)
    {
        _movement.Set(h, 0f, v);
        _movement = _movement.normalized * speed * Time.deltaTime;
        _playerRb.MovePosition(transform.position + _movement);
    }
    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        if(Physics.Raycast(camRay, out floorHit, camReyLength, _floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            _playerRb.MoveRotation(newRotation);
        }
    }
}
