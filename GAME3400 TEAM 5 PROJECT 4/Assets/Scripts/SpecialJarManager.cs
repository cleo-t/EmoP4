using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialJarManager : MonoBehaviour
{

    private List<SpecialJarThings> specialJars;

    private void Awake()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("JarSpecial");
        foreach (GameObject jar in temp)
        {
            SpecialJarThings temp2 = jar.GetComponent<SpecialJarThings>();
            specialJars.Add(temp2);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
