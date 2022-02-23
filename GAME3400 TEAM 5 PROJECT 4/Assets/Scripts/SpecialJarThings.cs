using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialJarThings : MonoBehaviour
{

    public float lightOnDistance = 2f;

    public GameObject player;
    public GameObject model;
    public GameObject light;

    public MeshRenderer jarGlassMesh;
    public MeshRenderer jarLidMesh;

    public Material glassMaterial;
    public Material lidMaterial;


    private bool hovering;
    private bool placedDown;

    void Awake() 
    {
        HoveringOff();
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
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= lightOnDistance)
        {
            light.SetActive(true);
        }
        else if (!hovering)
        {
            light.SetActive(false);
        }
        
    }

    public void HoveringOn() 
    {
        model.SetActive(true);
        light.SetActive(true);
        hovering = true;

    }

    public void HoveringOff() 
    {
        model.SetActive(false);
        hovering = false;

    }

    public void Place(BugManager.Bug bugType)
    {
        // Change object material
        jarGlassMesh.material = glassMaterial;
        jarLidMesh.material = lidMaterial;

        // Add Worm
        // make objecter perminet 
    }

    public static implicit operator SpecialJarThings(GameObject v)
    {
        throw new NotImplementedException();
    }
}
