using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayWhenOutOfJars : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToEnable;
    [SerializeField]
    private Text textRenderer;
    [SerializeField]
    private string text;

    void Update()
    {
        bool outOfJars = InventoryManager.instance.GetJarCount() == 0;
        this.textRenderer.text = outOfJars ? this.text : "";
        if (this.objectToEnable != null)
        {
            this.objectToEnable.SetActive(outOfJars);
        }
    }
}
