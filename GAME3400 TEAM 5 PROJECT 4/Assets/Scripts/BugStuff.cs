using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugStuff : MonoBehaviour
{
    Vector3 initialBugPosition;

    public BugManager.Bug bugType;

    public bool inJar;

    private void Awake()
    {
        inJar = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        initialBugPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inJar)
        {
            switch (bugType)
            {
                case BugManager.Bug.None:
                    break;
                case BugManager.Bug.Worm:
                    Debug.Log("Here 1");
                    Debug.Log(inJar);
                    WormMove();
                    break;
                case BugManager.Bug.Butterfly:
                    ButterflyMove();
                    break;
                case BugManager.Bug.Stickbug:
                    ClimbTree();
                    break;
                case BugManager.Bug.Ants:
                    break;
                case BugManager.Bug.Snail:
                    break;
                case BugManager.Bug.Ladybug:
                    ClimbTree();
                    break;
                case BugManager.Bug.Bee:
                    BeeMove();
                    break;
                case BugManager.Bug.Spider:
                    break;
                default:
                    break;
            }
        }
    }

    void ButterflyMove()
    {
        float positionOffset = .3f;
        float speed = 2.0f;
        float t = Mathf.Sin(speed * Time.time) + 1;
        t /= 2;

        float t2 = Mathf.Sin(speed * Time.time * 2) + 1;
        t2 /= 2;

        float t3 = Mathf.Cos(speed * Time.time) + 1;
        t3 /= 2;

        Vector3 top = new Vector3(transform.position.x, initialBugPosition.y - positionOffset * 2, transform.position.z);
        Vector3 bottom = new Vector3(transform.position.x, initialBugPosition.y + positionOffset * 2, transform.position.z);
        transform.position = Vector3.Lerp(top, bottom, t);

        Vector3 left = new Vector3(initialBugPosition.x - positionOffset, transform.position.y, transform.position.z);
        Vector3 right = new Vector3(initialBugPosition.x + positionOffset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(left, right, t2);

        Vector3 front = new Vector3(transform.position.x, transform.position.y, initialBugPosition.z - positionOffset);
        Vector3 back = new Vector3(transform.position.x, transform.position.y, initialBugPosition.z + positionOffset);
        transform.position = Vector3.Lerp(front, back, t3);
    }
    void BeeMove()
    {
        float positionOffset = .3f;
        float speed = 1.5f;
        float t = Mathf.Sin(speed * Time.time) + 1;
        t /= 2;
        float t2 = Mathf.Cos(speed * Time.time) + 1;
        t2 /= 2;
        float t3 = Mathf.Sin(speed * Time.time * 10) + 1;
        t3 /= 2;

        Vector3 top = new Vector3(transform.position.x, initialBugPosition.y - positionOffset, transform.position.z);
        Vector3 bottom = new Vector3(transform.position.x, initialBugPosition.y + positionOffset, transform.position.z);
        transform.position = Vector3.Lerp(top, bottom, t);

        Vector3 left = new Vector3(initialBugPosition.x - positionOffset, transform.position.y, transform.position.z);
        Vector3 right = new Vector3(initialBugPosition.x + positionOffset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(left, right, t2);

        Vector3 front = new Vector3(transform.position.x, transform.position.y, initialBugPosition.z - positionOffset/5);
        Vector3 back = new Vector3(transform.position.x, transform.position.y, initialBugPosition.z + positionOffset/5);
        transform.position = Vector3.Lerp(front, back, t3);
    }
    void ClimbTree()
    {
        float positionOffset = 2f;
        float speed = .05f;
        float t = Mathf.Sin(speed * Time.time) + 1;
        t /= 2;

        Vector3 top = new Vector3(transform.position.x, initialBugPosition.y - positionOffset, transform.position.z);
        Vector3 bottom = new Vector3(transform.position.x, initialBugPosition.y + positionOffset, transform.position.z);
        transform.position = Vector3.Lerp(top, bottom, t);
    }
    void WormMove()
    {
        float positionOffset = .3f;
        float speed = .18f;
        float t = Mathf.Sin(speed * Time.time * 5) + 1;
        t /= 2;
        float t2 = Mathf.Sin(speed * Time.time) + 1;
        t2 /= 2;
        float t3 = Mathf.Cos(speed * Time.time) + 1;
        t3 /= 2;

        Vector3 top = new Vector3(transform.position.x, initialBugPosition.y - positionOffset/4, transform.position.z);
        Vector3 bottom = new Vector3(transform.position.x, initialBugPosition.y + positionOffset/4, transform.position.z);
        transform.position = Vector3.Lerp(top, bottom, t);

        Vector3 left = new Vector3(initialBugPosition.x - positionOffset, transform.position.y, transform.position.z);
        Vector3 right = new Vector3(initialBugPosition.x + positionOffset, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(left, right, t2);

        Vector3 front = new Vector3(transform.position.x, transform.position.y, initialBugPosition.z - positionOffset);
        Vector3 back = new Vector3(transform.position.x, transform.position.y, initialBugPosition.z + positionOffset);
        transform.position = Vector3.Lerp(front, back, t3);
    }

    public void PlacedInJar()
    {
        inJar = true;
    }

    public BugManager.Bug GetBugType()
    {
        return bugType;
    }
}
