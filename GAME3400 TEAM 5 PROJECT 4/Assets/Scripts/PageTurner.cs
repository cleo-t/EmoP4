using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageTurner : MonoBehaviour
{
    [SerializeField]
    private List<Texture> pageTextures;
    [SerializeField]
    private uint startPageIndex = 0;
    [SerializeField]
    private float turnSeconds = 0.75f;
    [SerializeField]
    private MeshRenderer page1;
    [SerializeField]
    private MeshRenderer page2;
    [SerializeField]
    private MeshRenderer turnyPageLeft;
    [SerializeField]
    private MeshRenderer turnyPageRight;
    [SerializeField]
    private GameObject turnyPageObject;

    private bool turning;
    private int index;

    void Start()
    {
        this.turning = false;
        this.index = (int)this.startPageIndex;

        this.RenderNotTurning();
    }

    private void OnMouseDown()
    {
        StartCoroutine(this.TurnPage());
    }

    private void RenderNotTurning()
    {
        this.turnyPageObject.SetActive(false);
        this.SetPageTexture(this.page1, 0);
        this.SetPageTexture(this.page2, 1);
    }

    private void RenderTurning()
    {
        this.turnyPageObject.SetActive(true);
        this.SetPageTexture(this.page1, 0);
        this.SetPageTexture(this.turnyPageLeft, 1);
        this.SetPageTexture(this.turnyPageRight, 2);
        this.SetPageTexture(this.page2, 3);
    }

    private int GetWrappedPageIndex(int index, int offset)
    {
        return (index + offset) % this.pageTextures.Count;
    }

    private void SetPageTexture(MeshRenderer page, int offsetFromCurrentPage)
    {
        page.material.SetTexture(Shader.PropertyToID("_MainTex"), this.pageTextures[this.GetWrappedPageIndex(this.index, offsetFromCurrentPage)]);
    }

    private IEnumerator TurnPage()
    {
        if (!this.turning)
        {
            this.turning = true;
            this.RenderTurning();
            this.turnyPageObject.transform.localRotation = Quaternion.AngleAxis(180, Vector3.forward);

            float dDeg = 180 / this.turnSeconds;
            float angle = 0;
            while(angle < 179.5f)
            {
                float degStep = dDeg * Time.deltaTime;
                angle += degStep;
                this.turnyPageObject.transform.localRotation *= Quaternion.AngleAxis(degStep, Vector3.forward);
                yield return null;
            }

            this.index = this.GetWrappedPageIndex(this.index, 2);
            this.RenderNotTurning();
            this.turning = false;
        }
    }
}
