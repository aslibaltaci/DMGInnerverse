using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour
{
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerInventory>();

            if (playerInventory != null && playerInventory.hasKey)
            {
                OpenDoor();
            }
            else
            {
                Debug.Log("You need a key to unlock this door!");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }

    void OpenDoor()
    {
        Debug.Log("Door Unlocked!");
        
    }
}
