using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Variables de vida
    public int vidaMaxima = 100;   // Vida m�xima del enemigo
    private int vidaActual;

    // Variables para hacer da�o al jugador
    public int da�o = 10;          // Da�o que hace el enemigo al jugador
    public float tiempoEntreGolpes = 2f; // Tiempo entre cada golpe (en segundos)
    private float proximoGolpe = 0f;     // El tiempo en el que el enemigo puede volver a hacer da�o

    // Variables para inflar el enemigo
    public float hincharEscala = 1.2f; // Cu�nto crece el enemigo al recibir da�o
    private Vector3 escalaOriginal; // Almacenar� la escala original del enemigo

    // Referencia al AudioSource y sonido de explosi�n
    private AudioSource audioSource;
    public AudioClip sonidoExplosion; // Sonido que se reproduce al morir

    // Start se llama antes del primer frame update
    void Start()
    {
        // Inicializamos la vida actual al m�ximo
        vidaActual = vidaMaxima;
        escalaOriginal = transform.localScale; // Guardamos la escala original

        // Referencia al AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // M�todo para que el enemigo reciba da�o
    public void RecibirDa�o(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo recibi� da�o. Vida restante: " + vidaActual);

        // Hinchamos el sprite del enemigo
        transform.localScale = escalaOriginal * hincharEscala;

        // Verificamos si la vida lleg� a cero
        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    // M�todo para manejar la muerte del enemigo
    private void Morir()
    {
        Debug.Log("Enemigo ha muerto.");

        // Reproducir el sonido de explosi�n
        if (sonidoExplosion != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoExplosion);
        }

        // Destruir el enemigo inmediatamente
        Destroy(gameObject);
    }

    // M�todo para hacer da�o al jugador con cooldown
    private void OnTriggerStay(Collider other)
    {
        // Comprobamos si el objeto con el que chocamos es el jugador
        if (other.CompareTag("Player"))
        {
            // Si el tiempo actual es mayor al pr�ximo golpe permitido
            if (Time.time >= proximoGolpe)
            {
                // Hacer da�o al jugador
                other.GetComponent<Jugador>().TomarDa�o(da�o);

                // Establecer el pr�ximo tiempo en el que el enemigo puede hacer da�o
                proximoGolpe = Time.time + tiempoEntreGolpes;
            }
        }
    }
}


