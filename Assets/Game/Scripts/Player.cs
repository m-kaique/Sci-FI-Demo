using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private float _speed = 3.5f;
    private float _gravity = 9.81f;
    [SerializeField]
    private Camera _MainCamera;

    public float range = 10;

    // Start is called before the first frame update
    void Start()
    {
        
        _controller = GetComponent<CharacterController>();
        Cursor.visible= false;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            Ray theRay = _MainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
            Debug.DrawRay(theRay.origin, theRay.direction * 20f, Color.red, 5f);


            if (Physics.Raycast(theRay, out RaycastHit hit, range))
            {
                if (hit.collider.tag == "environment")
                {
                    Debug.Log("Its the Environment");
                }
                else if (hit.collider.tag == "enemy")
                {
                    Debug.Log("Its the enemy");
                }
                else
                {
                    Debug.Log("Hit Something in the VOID");
                }
            }
        }

        CalculateMovement();

    }
 

    private void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        // apply gravity
        velocity.y -= _gravity;

        velocity = transform.transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }
}
