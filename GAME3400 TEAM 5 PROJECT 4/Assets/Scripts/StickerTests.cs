using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerTests : MonoBehaviour
{
    private int index;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            BugManager.instance.AddBugCaught((BugManager.Bug)index);
            index++;
        }
    }
}
