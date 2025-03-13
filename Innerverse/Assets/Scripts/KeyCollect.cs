using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public GameObject DoorBlocker; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInventory>().hasKey = true;
            Debug.Log("Key Collected!");

            if (DoorBlocker != null)
            {
                BoxCollider2D collider = DoorBlocker.GetComponent<BoxCollider2D>();

                if (collider != null)
                {
                    Debug.Log("Disabling Collider on: " + DoorBlocker.name);
                    collider.enabled = false; 
                }
                else
                {
                    Debug.LogError("No BoxCollider2D found on " + DoorBlocker.name);
                }
            }
            else
            {
                Debug.LogError("DoorBlocker is NOT assigned!");
            }

            Destroy(gameObject);
        }
    }
}
