using UnityEngine;
using System.Collections;

public class ChangeSpriteOnTrigger : MonoBehaviour
{
    public Sprite newSprite; // Assign in Inspector
    public float fadeDuration = 1f; // Adjust fade speed

    private SpriteRenderer spriteRenderer;
    private bool hasChanged = false; // Prevents multiple triggers
    private SpriteChangeManager manager; // Reference to the manager

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        manager = FindObjectOfType<SpriteChangeManager>(); // Find manager
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasChanged)
        {
            StartCoroutine(FadeToNewSprite());
        }
    }

    IEnumerator FadeToNewSprite()
    {
        hasChanged = true; 
        float elapsedTime = 0f;
        Color color = spriteRenderer.color;

        while (elapsedTime < fadeDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, elapsedTime / (fadeDuration / 2));
            spriteRenderer.color = color;
            yield return null;
        }

        spriteRenderer.sprite = newSprite;
        elapsedTime = 0f;

        while (elapsedTime < fadeDuration / 2)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, elapsedTime / (fadeDuration / 2));
            spriteRenderer.color = color;
            yield return null;
        }

        color.a = 1f;
        spriteRenderer.color = color;

        manager.NotifySpriteChanged(); 
    }
}
