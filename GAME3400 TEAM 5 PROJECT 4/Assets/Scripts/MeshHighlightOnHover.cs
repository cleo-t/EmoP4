using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHighlightOnHover : MonoBehaviour
{
    [SerializeField]
    private List<Renderer> renderers;
    [SerializeField]
    private float lightCoeff = 0.5f;

    private void OnMouseEnter()
    {
        foreach(Renderer renderer in this.renderers)
        {
            renderer.material.color *= lightCoeff;
        }
    }

    private void OnMouseExit()
    {
        foreach (Renderer renderer in this.renderers)
        {
            renderer.material.color /= lightCoeff;
        }
    }
}
