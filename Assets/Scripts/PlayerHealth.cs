using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;          // Salud m�xima del jugador
    public float currentHealth;              // Salud actual del jugador
    public Slider healthBar;                 // Barra de salud UI

    void Start()
    {
        currentHealth = maxHealth;           // Inicializa la salud actual
        UpdateHealthBar();                   // Actualiza la barra de salud al inicio
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;              // Resta el da�o a la salud actual
        if (currentHealth < 0)
        {
            currentHealth = 0;                // Asegura que la salud no sea negativa
            Die();                            // Llama al m�todo de muerte si la salud es 0
        }

        UpdateHealthBar();                   // Actualiza la barra de salud despu�s de recibir da�o
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth; // Actualiza la barra de salud en UI
        }
    }

    void Die()
    {
        // Aqu� puedes agregar la l�gica para la muerte del jugador, como reiniciar el nivel, mostrar un men� de game over, etc.
        Debug.Log("�El jugador ha muerto!");
        Destroy(gameObject); // Destruye el objeto del jugador (opcional)
    }
}
