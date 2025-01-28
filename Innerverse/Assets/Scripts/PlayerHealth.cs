using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 30;
    public int health;
    public Image healthBar;

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
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        if (isDestroyed) return;
        
        health -= damage;
        flashEffect.Flash();

        health = Mathf.Clamp(health, 0, maxHealth);

        UpdateHealthBar();

        if (health <= 0)
        {
            isDestroyed = true;
            Destroy(gameObject);
            
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Divide as float to avoid integer division
            healthBar.fillAmount = (float)health / maxHealth;
        }
        else
        {
            Debug.LogError("HealthBar is not assigned in the Inspector!");
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
