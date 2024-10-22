using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;          // Salud máxima del jugador
    public float currentHealth;              // Salud actual del jugador
    public Slider healthBar;                 // Barra de salud UI

    void Start()
    {
        currentHealth = maxHealth;           // Inicializa la salud actual
        UpdateHealthBar();                   // Actualiza la barra de salud al inicio
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;              // Resta el daño a la salud actual
        if (currentHealth < 0)
        {
            currentHealth = 0;                // Asegura que la salud no sea negativa
            Die();                            // Llama al método de muerte si la salud es 0
        }

        UpdateHealthBar();                   // Actualiza la barra de salud después de recibir daño
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
        // Aquí puedes agregar la lógica para la muerte del jugador, como reiniciar el nivel, mostrar un menú de game over, etc.
        Debug.Log("¡El jugador ha muerto!");
        Destroy(gameObject); // Destruye el objeto del jugador (opcional)
    }
}
