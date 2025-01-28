using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 20;
    public int health;
    public VignetteController vignetteController;

    public AudioSource audioPlayer;
    public AudioSource audioPlayer1;
    public AudioSource audioPlayer2;
    public AudioSource audioPlayer3;

    [SerializeField] private SimpleFlash flashEffect;

    private bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDestroyed) return;
        health -= damage;
        flashEffect.Flash();

        if (health < 0)
        {
            isDestroyed = true;
            Destroy(gameObject);
            
        }
        if (vignetteController != null)
        {
            vignetteController.TriggerVignette();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyed) return;
        if (collision.gameObject.tag == "Enemy1")
        {
            audioPlayer.Play();
        }
        else if (collision.gameObject.tag == "Enemy2")
        {
            audioPlayer1.Play();
        }
        else if (collision.gameObject.tag == "Enemy3")
        {
            audioPlayer2.Play();
        }
        else if (collision.gameObject.tag == "Enemy4")
        {
            audioPlayer.Play();
        }

    }
}   
