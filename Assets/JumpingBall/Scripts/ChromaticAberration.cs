using UnityEngine;
using System.Collections;

/// This class handles the Chromatic Abberation Image Effect
[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour
{
    public float duration = 5f;
    public float maxTime;
    private float minTime = 0f;
    public float speedMulti;
    public float elapsed = 0f;
    public Shader chromeAbbShader;
    public float ChromaticAbberation = 1.0f;
    private Material curMaterial;
    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(chromeAbbShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }

    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture srcText, RenderTexture destText)
    {
        if (chromeAbbShader != null)
        {
            material.SetFloat("_AberrationOffset", ChromaticAbberation);
            Graphics.Blit(srcText, destText, material);
        }
        else
        {
            Graphics.Blit(srcText, destText);
        }
    }

    IEnumerator OverTime(/*float waitTime*/)
    {
        elapsed = minTime;
        duration = maxTime;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime * speedMulti;
            yield return null;
        }
        elapsed = maxTime;
        yield return StartCoroutine("UnderTime");
    }

    IEnumerator UnderTime()
    {
        elapsed = maxTime;
        duration = minTime;
        while (elapsed > duration)
        {
            elapsed -= Time.deltaTime * speedMulti;
            yield return null;
        }
        elapsed = minTime;
        yield return null;
    }

    void Update()
    {
        ChromaticAbberation = elapsed;
    }

    public void StartAbberation()
    {
        StopAllCoroutines();
        StartCoroutine("OverTime");
    }

    void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }
}