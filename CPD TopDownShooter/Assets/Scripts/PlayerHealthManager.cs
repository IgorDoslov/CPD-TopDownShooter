using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public float flashLength;
    private float flashCounter;

    private Renderer rend;
    private Color storedColor;

    public HealthBar healthBar;

    public GameObject deathEffect;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            FindObjectOfType<GameManager>().EndGame();
            gameObject.SetActive(false);
        }

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    public void HealPlayer(int heal)
    {
        currentHealth += heal;
        healthBar.SetHealth(currentHealth);
    }
}
