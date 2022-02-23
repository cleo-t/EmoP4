using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialJarThings : MonoBehaviour
{

    public GameObject model;
    public GameObject light;

    void Awake() 
    {
        HoveringOff();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoveringOn() 
    {
        model.SetActive(true);
        light.SetActive(true);

    }

    public void HoveringOff() 
    {
        model.SetActive(false);
        //light.SetActive(false);
        
    }

    public void Placed()
    {
        // Change object material
        // Add Worm
        // make objecter perminet 
    }

    public static implicit operator SpecialJarThings(GameObject v)
    {
        throw new NotImplementedException();
    }
}
