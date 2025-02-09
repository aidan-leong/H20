using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required for health bar integration

public class Health : MonoBehaviour
{
    [Header("Player Health Settings")]
    public int maxHealth = 5;  // Maximum health points
    public int currentHealth; // Current health points

    [Header("Invincibility Settings")]
    public float invincibilityDuration = 1f;  // Duration of invincibility after taking damage
    private bool isInvincible = false;        // Whether the player is invincible

    [Header("Damage Effects")]
    public GameObject deathEffect;            // Optional death effect (e.g., explosion)
    public Renderer playerRenderer;           // Reference to the player's renderer for flash effect
    public Color damageFlashColor = Color.red;
    private Color originalColor;

    [Header("Health Bar Integration")]
    public Slider healthBar;                  // Reference to the Slider UI component

    private void Start()
    {
        // Initialize health and save the player's original color
        currentHealth = maxHealth;

        if (playerRenderer != null)
        {
            originalColor = playerRenderer.material.color;
        }

        // Initialize health bar
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    // Method to take damage
    public void TakeDamage(int damage)
    {
        if (isInvincible || currentHealth <= 0) return;

        currentHealth -= damage;

        // Update health bar if assigned
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Check for player death
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
            if (playerRenderer != null)
            {
                StartCoroutine(FlashEffect());
            }
        }
    }

    // Heal the player
    public void Heal(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

        // Update health bar if assigned
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
    }

    // Invincibility coroutine
    private IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    // Flash effect coroutine to show damage visually
    private IEnumerator FlashEffect()
    {
        playerRenderer.material.color = damageFlashColor;
        yield return new WaitForSeconds(0.1f);
        playerRenderer.material.color = originalColor;
    }

    // Handle player death
    private void Die()
    {
        Debug.Log("Player has died!");

        // Optional: Spawn death effect
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Disable the player object
        gameObject.SetActive(false);

        // Load a Game Over screen or restart level
        SceneManager.LoadScene("Lose");
    }
}
