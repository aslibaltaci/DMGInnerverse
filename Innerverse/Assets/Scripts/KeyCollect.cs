using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCollect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            other.GetComponent<PlayerInventory>().hasKey = true; 
            Destroy(gameObject); 
            Debug.Log("Key Collected!");
        }
    }
}
