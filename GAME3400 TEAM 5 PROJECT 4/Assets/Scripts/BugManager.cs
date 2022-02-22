using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BugManager : MonoBehaviour
{
    public static BugManager instance;

    [Serializable]
    public struct BugPrefabPair
    {
        public Bug bugType;
        public GameObject prefab;
    }

    [SerializeField]
    private List<BugPrefabPair> bugPrefabs;

    private HashSet<Bug> bugTypesCaught;
    private List<Bug> bugsCaught;
    private Dictionary<Bug, GameObject> bugPrefabMap;

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

    public enum Bug
    {
        None = -1,
        Worm,
        Butterfly,
        Stickbug,
        Ants,
        Snail,
        Ladybug,
        Bee
    }

    void Start()
    {
        this.bugTypesCaught = new HashSet<Bug>();
        this.bugPrefabMap = new Dictionary<Bug, GameObject>();
        foreach(BugPrefabPair pair in this.bugPrefabs)
        {
            this.bugPrefabMap.Add(pair.bugType, pair.prefab);
        }
    }

    public void AddBugCaught(Bug bug)
    {
        this.bugTypesCaught.Add(bug);
        this.bugsCaught.Add(bug);
    }

    public List<Bug> GetBugTypesCaught()
    {
        return new List<Bug>(this.bugTypesCaught);
    }

    public List<Bug> GetBugsCaught()
    {
        return new List<Bug>(this.bugsCaught);
    }

    public GameObject GetBugPrefab(Bug bug)
    {
        if (this.bugPrefabMap.TryGetValue(bug, out GameObject prefab))
        {
            return prefab;
        } else
        {
            Debug.Log("Invalid bug: " + bug.ToString());
            return null;
        }
    }
}
