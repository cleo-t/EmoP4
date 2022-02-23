using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [HideInInspector]
    public bool hasNet;

    private int jars;
    private List<BugManager.Bug> bugsOnHand;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        this.bugsOnHand = new List<BugManager.Bug>();
        this.jars = 0;
        this.hasNet = false;
    }

    public void AddJar()
    {
        this.jars++;
    }

    public int GetJarCount()
    {
        return this.jars;
    }

    public void ResetJars()
    {
        this.jars = 0;
    }

    // Returns whether the bug can actually be caught too
    public bool BugCaught(BugManager.Bug bugType)
    {
        if (this.jars <= 0)
        {
            return false;
        }
        else
        {
            this.jars--;
            this.bugsOnHand.Add(bugType);
            return true;
        }
    }

    public List<BugManager.Bug> GetBugs()
    {
        return new List<BugManager.Bug>(this.bugsOnHand);
    }

    public void PopFrontBug()
    {
        if (this.bugsOnHand.Count <= 0)
        {
            return;
        }
        BugManager.Bug bug = this.bugsOnHand[0];
        this.bugsOnHand.RemoveAt(0);
        BugManager.instance.AddBugCaught(bug);
    }

    public bool HasBugs()
    {
        if (bugsOnHand != null && bugsOnHand.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
