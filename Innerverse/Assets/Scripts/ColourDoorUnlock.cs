using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public Sprite unlockedSprite; // Assign the colored door sprite in Inspector

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Make sure the door starts with its original sprite
        if (spriteRenderer.sprite == null)
        {
            Debug.LogError("?? Door's SpriteRenderer has no sprite assigned! Set the greyscale/outline sprite.");
        }

        SpriteChangeManager.OnAllSpritesChanged += UnlockDoor;
    }

    void OnDestroy()
    {
        SpriteChangeManager.OnAllSpritesChanged -= UnlockDoor;
    }

    void UnlockDoor()
    {
        Debug.Log("?? Door unlocked! Changing sprite...");

        if (spriteRenderer != null && unlockedSprite != null)
        {
            spriteRenderer.sprite = unlockedSprite; // Swap to the colored sprite
        }
        else
        {
            Debug.LogError("?? Missing SpriteRenderer or unlockedSprite!");
        }
    }
}
