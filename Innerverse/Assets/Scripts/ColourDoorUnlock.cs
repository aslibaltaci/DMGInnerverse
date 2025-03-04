using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    void OnEnable()
    {
        SpriteChangeManager.OnAllSpritesChanged += UnlockDoor;
    }

    void OnDisable()
    {
        SpriteChangeManager.OnAllSpritesChanged -= UnlockDoor;
    }

    void UnlockDoor()
    {
        Debug.Log("?? Door is unlocked!");
        gameObject.SetActive(false);
    }
}
