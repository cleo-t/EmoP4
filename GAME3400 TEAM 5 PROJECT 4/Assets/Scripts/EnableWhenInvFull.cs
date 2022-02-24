using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhenInvFull : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToDisable;
    [SerializeField]
    private int requiredJars = 3;

    void Update()
    {
        this.objectToDisable.SetActive(InventoryManager.instance.GetJarCount() >= this.requiredJars && InventoryManager.instance.hasNet);
    }
}
