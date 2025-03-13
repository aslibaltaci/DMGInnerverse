using UnityEngine;

public class SpriteChangeManager : MonoBehaviour
{
    public int totalSprites;
    private int changedSprites = 0; 
    public BoxCollider2D blocker;
    public AudioSource musicSource; 

    public void NotifySpriteChanged()
    {
        changedSprites++;
        Debug.Log($"Sprite changed: {changedSprites}/{totalSprites}");

        if (changedSprites >= totalSprites)
        {
            UnlockArea();
            PlayMusic();
        }
    }

    void UnlockArea()
    {
        if (blocker != null)
        {
            Debug.Log("All sprites changed! Removing box collider.");
            blocker.enabled = false; 
        }
        else
        {
            Debug.LogError("No BoxCollider assigned to SpriteChangeManager!");
        }
    }

    public void PlayMusic()
    {
        if (musicSource != null && !musicSource.isPlaying)  
        {
            musicSource.Play();
            Debug.Log("Music started playing!");
        }
        else if (musicSource == null)
        {
            Debug.LogError("Music Source is not assigned!");
        }
    }

}
