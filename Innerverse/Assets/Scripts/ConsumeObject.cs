using UnityEngine;
using System.Collections;

public class ConsumeObject : MonoBehaviour
{
    private GameObject consumableObject; // Stores the object the player is colliding with

    void Update()
    {
        if (consumableObject != null && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ShrinkAndDestroy(consumableObject));
            consumableObject = null; // Clear reference after consuming
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Consumable"))
        {
            consumableObject = collision.gameObject; // Store the object
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == consumableObject)
        {
            consumableObject = null; // Clear reference when leaving
        }
    }

    IEnumerator ShrinkAndDestroy(GameObject obj)
    {
        float shrinkTime = 0.5f;
        Vector3 originalScale = obj.transform.localScale;

        for (float t = 0; t < shrinkTime; t += Time.deltaTime)
        {
            obj.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, t / shrinkTime);
            yield return null;
        }

        Destroy(obj);
    }
}
