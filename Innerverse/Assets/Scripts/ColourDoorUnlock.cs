using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public Sprite unlockedSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.sprite == null)
        {
            Debug.LogError("?? Door's SpriteRenderer has no sprite assigned! Set the greyscale/outline sprite.");
        }

        FindObjectOfType<SpriteChangeManager>().NotifySpriteChanged();
    }

    void OnDestroy()
    {
        //SpriteChangeManager.OnAllSpritesChanged -= UnlockDoor;
    }

    public void UnlockDoor()
    {
        Debug.Log("?? Door unlocked! Changing sprite...");

        if (spriteRenderer != null && unlockedSprite != null)
        {
            spriteRenderer.sprite = unlockedSprite;
        }
        else
        {
            Debug.LogError("?? Missing SpriteRenderer or unlockedSprite!");
        }
    }
}
