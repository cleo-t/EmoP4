using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SpecialJarManager : MonoBehaviour
{
    public static SpecialJarManager instance;

    private Dictionary<string, BugManager.Bug> jarStates;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            this.jarStates = new Dictionary<string, BugManager.Bug>();
            GameObject[] temp = GameObject.FindGameObjectsWithTag("JarSpecial");
            Array.Sort(temp, this.CompareJarNames);
            foreach (GameObject jar in temp)
            {
                this.jarStates.Add(jar.name, BugManager.Bug.None);
            }

            SceneManager.sceneLoaded += this.OnSceneLoad;
        } else
        {
            Destroy(this);
        }
    }

    private int CompareJarNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    public void JarPlaced(string name, BugManager.Bug bugType)
    {
        if (this.jarStates.ContainsKey(name))
        {
            this.jarStates.Remove(name);
        }
        this.jarStates.Add(name, bugType);
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        foreach(KeyValuePair<string, BugManager.Bug> entry in this.jarStates)
        {
            if (entry.Value != BugManager.Bug.None)
            {
                GameObject jar = GameObject.Find(entry.Key);
                SpecialJarThings jarComp = jar.GetComponent<SpecialJarThings>();
                jarComp.Place(entry.Value, true);
            }
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
