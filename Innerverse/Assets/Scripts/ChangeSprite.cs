using UnityEngine;
using System.Collections;

public class ChangeSpriteOnTrigger : MonoBehaviour
{
    public Sprite newSprite;
    public float fadeDuration = 1f;

    private SpriteRenderer spriteRenderer;
    private bool hasChanged = false;
    private SpriteChangeManager manager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("Error: SpriteRenderer is missing on " + gameObject.name);
        }

        manager = FindObjectOfType<SpriteChangeManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasChanged)
        {
            StartCoroutine(FadeToNewSprite());
        }
    }

    public IEnumerator FadeToNewSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("Error: SpriteRenderer is missing on " + gameObject.name);
            yield break;
        }

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
