using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Variables de vida
    public int vidaMaxima = 100;   // Vida máxima del enemigo
    private int vidaActual;

    // Variables para hacer daño al jugador
    public int daño = 10;          // Daño que hace el enemigo al jugador
    public float tiempoEntreGolpes = 2f; // Tiempo entre cada golpe (en segundos)
    private float proximoGolpe = 0f;     // El tiempo en el que el enemigo puede volver a hacer daño

    // Variables para inflar el enemigo
    public float hincharEscala = 1.2f; // Cuánto crece el enemigo al recibir daño
    private Vector3 escalaOriginal; // Almacenará la escala original del enemigo

    // Referencia al AudioSource y sonido de explosión
    private AudioSource audioSource;
    public AudioClip sonidoExplosion; // Sonido que se reproduce al morir

    // Start se llama antes del primer frame update
    void Start()
    {
        // Inicializamos la vida actual al máximo
        vidaActual = vidaMaxima;
        escalaOriginal = transform.localScale; // Guardamos la escala original

        // Referencia al AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Método para que el enemigo reciba daño
    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo recibió daño. Vida restante: " + vidaActual);

        // Hinchamos el sprite del enemigo
        transform.localScale = escalaOriginal * hincharEscala;

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

        // Reproducir el sonido de explosión
        if (sonidoExplosion != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoExplosion);
        }

        // Destruir el enemigo inmediatamente
        Destroy(gameObject);
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


