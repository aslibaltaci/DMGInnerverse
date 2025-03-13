using UnityEngine;
using System.Collections;

public class ConsumeObject : MonoBehaviour
{
    private GameObject consumableObject;
    public ConsumptionBar consumptionBar;
    private int collectedObjects = 0;
    public int maxObjects = 20; // Now set to 20
    public BoxCollider2D doorBlocker; // Assign in Inspector
    public PlayerInventory playerInventory; // Reference to track the key

    void Start()
    {
        if (consumptionBar == null)
        {
            Debug.LogError("ConsumptionBar script is not assigned in the Inspector!");
            return;
        }

        consumptionBar.UpdateBar(collectedObjects, maxObjects);
    }

    void Update()
    {
        if (consumableObject != null && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ShrinkAndDestroy(consumableObject));
            consumableObject = null;

            collectedObjects++;
            consumptionBar.UpdateBar(collectedObjects, maxObjects);

            // Check if both key and maxObjects are collected
            CheckUnlockConditions();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Consumable"))
        {
            consumableObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == consumableObject)
        {
            consumableObject = null;
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

    void CheckUnlockConditions()
    {
        if (playerInventory != null && playerInventory.hasKey && collectedObjects >= maxObjects)
        {
            if (doorBlocker != null)
            {
                doorBlocker.enabled = false; // Disable the blocking collider
                Debug.Log("Door Blocker Removed!");
            }
            else
            {
                Debug.LogError("Door Blocker is NOT assigned in the Inspector!");
            }
        }
    }
}
