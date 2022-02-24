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

    private bool fading;

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
        StartCoroutine(this.InitialFade());
    }

    // Update is called once per frame
    void Update()
    {
        if (this.fading)
        {
            float currentA = this.sceneFader.color.a;
            float dA = (Time.deltaTime / this.fadeTime) * Mathf.Sign(this.targetOpacity - currentA);
            float newA = currentA + dA;
            Mathf.Clamp(newA, 0, 1);
            this.SetFaderOpacity(Mathf.Clamp(newA, 0, 1));
        }
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
        this.fading = true;
        while(this.sceneFader.color.a < 0.999f)
        {
            yield return null;
        }
        this.fading = false;
        yield return new WaitForSecondsRealtime(this.pauseTime);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator InitialFade()
    {
        this.fading = true;
        while(this.sceneFader.color.a > 0.001f)
        {
            yield return null;
        }
        this.SetFaderOpacity(0);
        this.fading = false;
    }
}
