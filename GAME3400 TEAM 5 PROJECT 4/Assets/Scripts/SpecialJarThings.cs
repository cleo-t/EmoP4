using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialJarThings : MonoBehaviour
{

    public float lightOnDistance = 2f;
    public float bugHeightControl = 2f;

    public GameObject player;
    public GameObject model;
    public GameObject glowLight;
    public GameObject spotLight;

    public MeshRenderer jarGlassMesh;
    public MeshRenderer jarLidMesh;

    public Material glassMaterial;
    public Material lidMaterial;

    public AudioClip placeClip;


    private bool hovering;
    private bool placedDown;

    void Awake() 
    {
        HoveringOff();
        spotLight.SetActive(false);
        glowLight.SetActive(false);
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!placedDown)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= lightOnDistance)
            {
                spotLight.SetActive(true);
            }
            else if (!hovering)
            {
                spotLight.SetActive(false);
            }
        }
        
    }

    public bool isPlaced()
    {
        return placedDown;
    }

    public void HoveringOn() 
    {
        if (!placedDown)
        {
            spotLight.SetActive(false);
            model.SetActive(true);
            glowLight.SetActive(true);
            hovering = true;
        }
    }

    public void HoveringOff() 
    {
        if (!placedDown)
        {
            model.SetActive(false);
            glowLight.SetActive(false);
            hovering = false;
        }
    }

    public void Place(BugManager.Bug bugType, bool doneByManager = false)
    {
        // Change object material
        
        jarGlassMesh.material = glassMaterial;
        jarLidMesh.material = lidMaterial;

        // Add Worm
        GameObject bug = Instantiate(BugManager.instance.GetBugPrefab(bugType), transform.position, transform.rotation);

        Vector3 scaleChange = new Vector3(.25f, .25f, .25f);
        Vector3 posChange = new Vector3(transform.position.x, transform.position.y - bugHeightControl, transform.position.z);

        bug.transform.localScale = scaleChange;
        bug.transform.position = posChange;

        
        bug.GetComponent<BugStuff>().PlacedInJar();

        AudioSource.PlayClipAtPoint(placeClip, Camera.main.transform.position);


        // make objecter perminet
        model.SetActive(true);
        glowLight.SetActive(false);
        spotLight.SetActive(false);

        placedDown = true;

        if (!doneByManager)
        {
            SpecialJarManager.instance.JarPlaced(this.name, bugType);
        }
    }

    public static implicit operator SpecialJarThings(GameObject v)
    {
        throw new NotImplementedException();
    }
}
