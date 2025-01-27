using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class VignetteController : MonoBehaviour
{
    public Material vignetteMaterial;
    public float vignetteDuration = 0.5f;
    public float maxVignetteIntensity = 0.8f;

    private bool isFading = false;

    public void TriggerVignette()
    {
        if (!isFading)
        {
            StartCoroutine(VignetteEffect());
        }
    }

    private IEnumerator VignetteEffect()
    {
        isFading = true;

        float elapsedTime = 0f;

        while (elapsedTime < vignetteDuration)
        {
            elapsedTime += Time.deltaTime;
            float intensity = Mathf.Lerp(maxVignetteIntensity, 0f, elapsedTime / vignetteDuration);

            vignetteMaterial.SetFloat("_VignetteIntensity", intensity);

            yield return null; 
        }

        vignetteMaterial.SetFloat("_VignetteIntensity", 0f); 
        isFading = false;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        
        if (vignetteMaterial != null)
        {
            Graphics.Blit(src, dest, vignetteMaterial);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
    }
}
