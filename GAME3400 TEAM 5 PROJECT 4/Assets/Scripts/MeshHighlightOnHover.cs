using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHighlightOnHover : MonoBehaviour
{
    [SerializeField]
    private Renderer glow;

    void Start()
    {
        this.glow.enabled = false;
    }

    private void OnMouseEnter()
    {
        this.glow.enabled = true;
    }

    private void OnMouseExit()
    {
        this.glow.enabled = false;
    }
}
