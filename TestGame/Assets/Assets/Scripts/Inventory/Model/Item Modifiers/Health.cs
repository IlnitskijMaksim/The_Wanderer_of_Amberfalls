using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private FloatValueSO currentHealth;

    //[SerializeField] private GameObject bloodParticle;

    //[SerializeField] private Renderer renderer;
    //[SerializeField] private float flashTime = 0.2f;

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth.Value = 1;
        
    }

    

    public void Reduce(int damage, FloatValueSO currentHealth)
    {
        currentHealth.Value -= damage / maxHealth;
        healthBar.SetHealth(currentHealth.Value);
        //CreateHitFeedback();
        if (currentHealth.Value <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int healthBoost)
    {
        int health = Mathf.RoundToInt(currentHealth.Value * maxHealth);
        int value = health + healthBoost;
        currentHealth.Value = (value > maxHealth ? maxHealth : value / maxHealth);
        healthBar.SetHealth(currentHealth.Value);
    }

    /*private void CreateHitFeedback()
    {
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        StartCoroutine(FlashFeedback());
    }

    private IEnumerator FlashFeedback()
    {
        renderer.material.SetInt("_Flash", 1);
        yield return new WaitForSeconds(flashTime);
        renderer.material.SetInt("_Flash", 0);
    }
    */
    private void Die()
    {
        Debug.Log("Died");
        currentHealth.Value = 1;
    }
}

