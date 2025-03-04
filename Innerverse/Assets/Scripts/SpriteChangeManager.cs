using UnityEngine;
using System;

public class SpriteChangeManager : MonoBehaviour
{
    private ChangeSpriteOnTrigger[] allSprites;
    private int changedCount = 0;

    public static event Action OnAllSpritesChanged;

    void Start()
    {
        allSprites = FindObjectsOfType<ChangeSpriteOnTrigger>();
    }

    public void NotifySpriteChanged()
    {
        changedCount++; 
        if (changedCount >= allSprites.Length)
        {
            Debug.Log("✅ All sprites changed! Triggering event.");
            OnAllSpritesChanged?.Invoke(); 
        }
    }
}
