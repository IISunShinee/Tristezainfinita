using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Variables de vida
    public int vidaMaxima = 100;   // Vida m�xima del enemigo
    private int vidaActual;

    // Variables para hacer da�o al jugador
    public int da�o = 10;          // Da�o que hace al jugador
    public float tiempoEntreGolpes = 2f; // Tiempo entre cada golpe (en segundos)
    private float proximoGolpe = 0f;     // El tiempo en el que el enemigo puede volver a hacer da�o

    // Referencia al Animator (si tiene animaciones)
    private Animator animator;

    // Start se llama antes del primer frame update
    void Start()
    {
        // Inicializamos la vida actual al m�ximo
        vidaActual = vidaMaxima;

        // Referencia al Animator (opcional)
        animator = GetComponent<Animator>();
    }

    // M�todo para que el enemigo reciba da�o
    public void RecibirDa�o(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("Enemigo recibi� da�o. Vida restante: " + vidaActual);

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

        // Si tiene una animaci�n de muerte, la reproducimos
        if (animator != null)
        {
            animator.SetTrigger("Muerte");
        }

        // Desactivar el objeto o destruirlo despu�s de un tiempo
        Destroy(gameObject, 2f); // Destruye el enemigo despu�s de 2 segundos para dejar ver la animaci�n de muerte
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