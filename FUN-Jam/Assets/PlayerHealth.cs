using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float health;

    [SerializeField]
    private TextMeshProUGUI healthDisplay;

    [SerializeField]
    private ParticleSystem damage;

    private void OnEnable()
    {
       damage.Stop();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        healthDisplay.text = Mathf.Clamp(health, 0, 100).ToString();

        this.damage.Play();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        health = Mathf.Clamp(health, 0, 100);

        healthDisplay.text = Mathf.Clamp(health, 0, 100).ToString();
    }

    private void Die()
    {
        GameManager.instance.ReloadScene(true);
    }
}
