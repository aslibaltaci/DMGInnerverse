using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        if (health <= 0 && !isDestroyed)
        {
            isDestroyed = true;

    
            PlayerDissolve dissolveEffect = GetComponent<PlayerDissolve>();

            if (dissolveEffect != null)
            {
                dissolveEffect.onDissolveComplete = NextLevel;
            }
            else
            {
                Debug.LogError("PlayerDissolve component missing!");
                NextLevel();
            }
        }
    }


    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
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

    private void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No more levels to load. Returning to main menu.");
            SceneManager.LoadScene(0);
        }
    }
}   
