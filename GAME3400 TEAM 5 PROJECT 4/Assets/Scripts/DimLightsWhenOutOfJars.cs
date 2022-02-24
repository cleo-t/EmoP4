using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimLightsWhenOutOfJars : MonoBehaviour
{
    [SerializeField]
    private GameObject lightToDisable;

    void Update()
    {
        this.lightToDisable.SetActive(InventoryManager.instance.GetJarCount() > 0);
    }
}
