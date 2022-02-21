using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManStuff : MonoBehaviour
{
    public float moveSpeed = 0;
    public float gravity = 9.81f;
    public float jumpHeight = 5;
    public float range = 100f;

    public GameObject net;
    public Camera fpsCam;
    

    private bool hasNet;

    private Vector3 moveDirection;
    private Vector3 input;
    private CharacterController _controller;


    

    // Start is called before the first frame update
    void Awake()
    {
        hasNet = false;
        net.SetActive(false);
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("Here3");
            Click();
        }
        PlayerMovement();
    }

    private void PlayerMovement() 
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input *= moveSpeed;


        if (_controller.isGrounded)
        {
            //jump
            moveDirection = input;
            // jump height = .5 * v(0)^2 / g

            if (Input.GetButton("Jump"))
            {
                // jump
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
            else
            {
                // ground the object
                moveDirection.y = 0.0f;
            }
        }
        else
        {
            //mid-air
        }
        moveDirection.y -= gravity * Time.deltaTime;
        _controller.Move(moveDirection * Time.deltaTime);
    }

    private void Click() 
    {
        RaycastHit target;
        Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out target, range);

        if (target.transform.CompareTag("Net")) 
        {
            hasNet = true;
            net.SetActive(true);
            Destroy(target.transform.gameObject);
        }
        else if (target.transform.CompareTag("Bug") && hasNet) 
        {
            
            
        }
    }
}

