using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Variables de vida
    public int vidaMaxima = 100;   // Vida máxima del enemigo
    private int vidaActual;

    // Variables para hacer daño al jugador
    public int daño = 10;          // Daño que hace al jugador
    public float tiempoEntreGolpes = 2f; // Tiempo entre cada golpe (en segundos)
    private float proximoGolpe = 0f;     // El tiempo en el que el enemigo puede volver a hacer daño

    // Referencia al Animator (si tiene animaciones)
    private Animator animator;

    // Start se llama antes del primer frame update
    void Start()
    {
        // Inicializamos la vida actual al máximo
        vidaActual = vidaMaxima;

        // Referencia al Animator (opcional)
        animator = GetComponent<Animator>();
    }

    // Método para que el enemigo reciba daño
    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo recibió daño. Vida restante: " + vidaActual);

        // Verificamos si la vida llegó a cero
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    // Método para manejar la muerte del enemigo
    private void Morir()
    {
        Debug.Log("Enemigo ha muerto.");

        // Si tiene una animación de muerte, la reproducimos
        if (animator != null)
        {
            animator.SetTrigger("Muerte");
        }

        // Desactivar el objeto o destruirlo después de un tiempo
        Destroy(gameObject, 2f); // Destruye el enemigo después de 2 segundos para dejar ver la animación de muerte
    }

    // Método para hacer daño al jugador con cooldown
    private void OnTriggerStay(Collider other)
    {
        // Comprobamos si el objeto con el que chocamos es el jugador
        if (other.CompareTag("Player"))
        {
            // Si el tiempo actual es mayor al próximo golpe permitido
            if (Time.time >= proximoGolpe)
            {
                // Hacer daño al jugador
                other.GetComponent<Jugador>().TomarDaño(daño);

                // Establecer el próximo tiempo en el que el enemigo puede hacer daño
                proximoGolpe = Time.time + tiempoEntreGolpes;
            }
        }
    }
}