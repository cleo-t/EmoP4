using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BugSpawner : MonoBehaviour
{
    [Serializable]
    public struct BugSpawnEntry
    {
        public BugManager.Bug bugType;
        // Non-negative value; doesn't matter the range, these are defined relative to the other entries
        public float relativeOdds;
    }

    [SerializeField]
    private List<BugSpawnEntry> bugSpawnOdds;
    // Time after a bug is caught before the spawn area is refilled
    [SerializeField]
    private float spawnCooldownSeconds;

    private List<BugSpawnEntry> realBugSpawnOdds;
    private GameObject spawnedBug;
    private bool waitingToSpawnBug;
    private float spawnTimer;

    void Start()
    {
        this.spawnedBug = null;
        this.spawnTimer = 0;
        this.waitingToSpawnBug = false;

        float oddsSum = 0;
        foreach(BugSpawnEntry entry in this.bugSpawnOdds)
        {
            oddsSum += entry.relativeOdds;
        }
        this.realBugSpawnOdds = new List<BugSpawnEntry>();
        float currentOddsSum = 0;
        foreach(BugSpawnEntry entry in this.bugSpawnOdds)
        {
            currentOddsSum += entry.relativeOdds / oddsSum;
            BugSpawnEntry newEntry;
            newEntry.bugType = entry.bugType;
            newEntry.relativeOdds = currentOddsSum;
            this.realBugSpawnOdds.Add(newEntry);
        }

        this.SpawnBug();
    }

    private BugManager.Bug PickRandomBug()
    {
        float rand = UnityEngine.Random.value;
        foreach(BugSpawnEntry entry in this.realBugSpawnOdds)
        {
            if (rand < entry.relativeOdds)
            {
                return entry.bugType;
            }
        }
        return (BugManager.Bug)(-1);
    }

    void Update()
    {
        if (this.spawnedBug == null || !this.spawnedBug.activeInHierarchy || !this.spawnedBug.activeSelf)
        {
            if (this.spawnedBug != null)
            {
                Destroy(this.spawnedBug);
                this.spawnedBug = null;
            }
            if (!this.waitingToSpawnBug)
            {
                this.spawnTimer = this.spawnCooldownSeconds;
                this.waitingToSpawnBug = true;
            } else
            {
                this.spawnTimer -= Time.deltaTime;
                if (this.spawnTimer <= 0)
                {
                    this.SpawnBug();
                    this.waitingToSpawnBug = false;
                }
            }
        }
    }

    private void SpawnBug()
    {
        BugManager.Bug bugToSpawn = this.PickRandomBug();
        GameObject bugPrefab = BugManager.instance.GetBugPrefab(bugToSpawn);
        if (bugPrefab != null)
        {
            this.spawnedBug = Instantiate(bugPrefab, this.transform.position, Quaternion.identity, this.transform);
            this.OnSpawn(bugToSpawn, spawnedBug);
        }
    }

    private void OnSpawn(BugManager.Bug bug, GameObject obj)
    {
        switch (bug)
        {
            case BugManager.Bug.None:
                break;
            case BugManager.Bug.Worm:
                break;
            case BugManager.Bug.Butterfly:
                break;
            case BugManager.Bug.Stickbug:
                break;
            case BugManager.Bug.Ants:
                obj.transform.Rotate(0, 44.145f, 0);
                break;
            case BugManager.Bug.Snail:
                break;
            case BugManager.Bug.Ladybug:
                break;
            case BugManager.Bug.Bee:
                break;
            case BugManager.Bug.Spider:
                break;
            default:
                break;
        }
    }
}
