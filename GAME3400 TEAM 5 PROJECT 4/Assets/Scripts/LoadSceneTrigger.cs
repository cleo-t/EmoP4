using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour
{
    [SerializeField]
    private string targetScene;

    private bool triggered;

    private void Start()
    {
        this.triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.LoadScene();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.LoadScene();
        }
    }

    private void LoadScene()
    {
        if (!this.triggered)
        {
            this.triggered = true;
            ManageScenes.instance.SwitchScene(this.targetScene);
        }
    }
}
