using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{
    [SerializeField]
    private string sceneFaderTag = "SceneFade";
    [SerializeField]
    private float fadeTime = 0.75f;
    [SerializeField]
    private float pauseTime = 0.25f;

    private Image sceneFader;
    private float targetOpacity;

    public static ManageScenes instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        GameObject sceneFaderObject = GameObject.FindGameObjectWithTag(this.sceneFaderTag);
        if (sceneFaderObject != null)
        {
            this.sceneFader = sceneFaderObject.GetComponent<Image>();
        }

        if (this.sceneFader != null)
        {
            this.SetFaderOpacity(1);
        }
        this.targetOpacity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float currentA = this.sceneFader.color.a;
        float dA = (Time.deltaTime / this.fadeTime) * Mathf.Sign(this.targetOpacity - currentA);
        float newA = currentA + dA;
        Mathf.Clamp(newA, 0, 1);
        this.SetFaderOpacity(Mathf.Clamp(newA, 0, 1));
    }

    private void SetFaderOpacity(float a)
    {
        Color t = this.sceneFader.color;
        t.a = a;
        this.sceneFader.color = t;
    }

    public void SwitchScene(string sceneName)
    {
        this.targetOpacity = 1;
        StartCoroutine(this.WaitForFadeThenSwitch(sceneName));
    }
    
    private IEnumerator WaitForFadeThenSwitch(string sceneName)
    {
        while(this.sceneFader.color.a < 0.999f)
        {
            yield return null;
        }
        yield return new WaitForSecondsRealtime(this.pauseTime);
        SceneManager.LoadScene(sceneName);
    }
}
