using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManStuff : MonoBehaviour
{
    public float moveSpeed = 0;
    public float gravity = 9.81f;
    public float jumpHeight = 5;
    public float range = 100f;
    public float blinkTime = .1f;


    public GameObject net;
    public GameObject bedroom;
    public GameObject crossHair;

    public Camera fpsCam;

    public AudioClip swingSound;
    public AudioClip jarSound;
    public AudioClip outOfJars;

    private bool ohNo = false;
    private float nextTime = 0f;

    private Vector3 moveDirection;
    private Vector3 input;
    private CharacterController _controller;
    private GameObject lastLookTarget;
    

    

    // Start is called before the first frame update
    void Awake()
    {
        ohNo = false;
        net.SetActive(InventoryManager.instance.hasNet);
        crossHair.SetActive(true);
        _controller = GetComponent<CharacterController>();

        if (bedroom != null)
        {
            ohNo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Click();
        }
        if (ohNo && Time.time >=  nextTime) {
            nextTime = Time.time + blinkTime;
            Looking();

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

        if (target.collider != null)
        {
            if (target.transform.CompareTag("Net"))
            {
                InventoryManager.instance.hasNet = true;
                net.SetActive(true);
                Destroy(target.transform.gameObject);
            }
            else if (target.transform.CompareTag("Bug") && InventoryManager.instance.hasNet)
            {
                BugStuff bug = target.transform.gameObject.GetComponent<BugStuff>();
                BugManager.Bug bugType = bug.GetBugType();

                AudioSource.PlayClipAtPoint(swingSound, Camera.main.transform.position);


                if (InventoryManager.instance.BugCaught(bugType))
                {
                    PlaySoundAfterSeconds(.5f);

                    Destroy(target.transform.gameObject);
                    if (InventoryManager.instance.GetJarCount() == 0)
                    {
                        AudioSource.PlayClipAtPoint(outOfJars, Camera.main.transform.position);
                    }
                }

            }
            else if (target.transform.CompareTag("Jar"))
            {
                InventoryManager.instance.AddJar();
                
                Destroy(target.transform.gameObject);
            }
            else if (target.transform.CompareTag("JarSpecial") && InventoryManager.instance.HasBugs())
            {
                SpecialJarThings jar = target.transform.gameObject.GetComponent<SpecialJarThings>();
                jar.Place(InventoryManager.instance.PopFrontBug());
            }
            else
            {

            }
        }
    }

    private IEnumerable PlaySoundAfterSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        AudioSource.PlayClipAtPoint(jarSound, Camera.main.transform.position);
    }

    private void Looking()
    {
        RaycastHit hit;
        Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);

        if (hit.collider != null)
        {
            GameObject target = hit.transform.gameObject;

            if (target.transform.CompareTag("JarSpecial") && lastLookTarget != null && lastLookTarget != target)
            {
                SpecialJarThings jar = target.GetComponent<SpecialJarThings>();
                jar.HoveringOn();
            }

            if (lastLookTarget != null && lastLookTarget.transform.CompareTag("JarSpecial") && lastLookTarget != target)
            {
                SpecialJarThings jar = lastLookTarget.GetComponent<SpecialJarThings>();
                jar.HoveringOff();
            }


            lastLookTarget = target;
        }

        if(hit.collider == null && lastLookTarget != null && lastLookTarget.transform.CompareTag("JarSpecial"))
        {
            SpecialJarThings jar = lastLookTarget.GetComponent<SpecialJarThings>();
            jar.HoveringOff();
            lastLookTarget = null;
        }
        
    }
}

