using System.Collections;
using UnityEngine;
using System;

public class PlayerDissolve : MonoBehaviour
{
    public Material dissolveMaterial; // The dissolve material
    public float dissolveSpeed = 1.5f; // Speed of dissolving

    private SpriteRenderer spriteRenderer;
    private float dissolveAmount = 0f;
    private bool isDissolving = false;

    public Action onDissolveComplete; // Callback when dissolve is done

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Store the regular material (we need to use it when not dissolving)
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
                onDissolveComplete?.Invoke(); // Call the event when dissolve finishes
                Destroy(gameObject); // Destroy player after dissolve
            }
        }
    }

    public void StartDissolve()
    {
        // Change to dissolve material and start dissolving
        if (spriteRenderer != null)
        {
            spriteRenderer.material = dissolveMaterial;  // Apply dissolve material
            isDissolving = true;
        }
    }

    public void StopDissolve()
    {
        // Restore the regular material if you want to cancel dissolve for some reason
        if (spriteRenderer != null)
        {
            spriteRenderer.material = spriteRenderer.sharedMaterial;
        }
        isDissolving = false;
        dissolveAmount = 0f;
    }
}
