using System.Collections;
using UnityEngine;
using System;

public class PlayerDissolve : MonoBehaviour
{
    public Material dissolveMaterial; 
    public float dissolveSpeed = 1.5f; 

    private SpriteRenderer spriteRenderer;
    private float dissolveAmount = 0f;
    private bool isDissolving = false;

    public Action onDissolveComplete; 

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.material = spriteRenderer.sharedMaterial;
        }
    }

    void Update()
    {
        if (isDissolving)
        {
            dissolveAmount += Time.deltaTime * dissolveSpeed;
            spriteRenderer.material.SetFloat("_DissolveAmount", dissolveAmount);

            if (dissolveAmount >= 1)
            {
                onDissolveComplete?.Invoke(); 
                Destroy(gameObject); 
            }
        }
    }

    public void StartDissolve()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material = dissolveMaterial;
            isDissolving = true;
        }
    }

    public void StopDissolve()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.material = spriteRenderer.sharedMaterial;
        }
        isDissolving = false;
        dissolveAmount = 0f;
    }
}
