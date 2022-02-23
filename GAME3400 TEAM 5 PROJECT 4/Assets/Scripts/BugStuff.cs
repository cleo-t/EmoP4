using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugStuff : MonoBehaviour
{

    public BugManager.BugPrefabPair bugInfo;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BugManager.Bug GetBugType()
    {
        return bugInfo.bugType;
    }
}
